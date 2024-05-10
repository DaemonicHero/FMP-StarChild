using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player stats
    public float baseHealth = 10;
    public float health;
    public float baseMaxHealth = 10;
    public float maxHealth;
    public float baseArmor = 1;
    public float armor;
    public float baseDamage = 10;
    public float damage;
    public float baseDamagePenetration = 1;
    public float damagePenetration;
    public float baseSpeed = 7;
    public float speed;
    public float maxSpeed = 7;
    public float minSpeed = 3.5f;
    public float dodgeLength;

    //Pllayer Parts
    public GameObject sword;
    public GameObject shield;
    public GameObject playerModel;
    public GameObject pivot;

    //Mouse and Player Position
    public Vector3 mousePosition;
    public Vector3 playerPosition;

    //Sprite Selection
    public SpriteRenderer spriteRenderer;
    public Sprite north, south, east, west, northEast, northWest, southEast, southWest;

    //Attack and Defend Triggers
    public bool attacking;
    public bool defending;
    public bool dodging;

    public GameObject gameDirector;



    void Start()
    {
        //set base stats
        health = baseHealth;
        maxHealth = baseMaxHealth;
        armor = baseArmor;
        damage = baseDamage;
        damagePenetration = baseDamagePenetration;
        speed = baseSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        //if run in progress enable defend, attack, and dodge
        if(gameDirector.GetComponent<GameDirectorBeta>().runInProgress == true)
        {
            if (defending)
            {
                speed = minSpeed;
            }
            else
            {
                speed = maxSpeed;
            }

            if (!attacking)
            {
                Attack();
            }

            Defend();

            if (!dodging)
            {
                Dodge();
            }
        }

        //if speed = 0, reset speed 
        if (speed == 0)
        {
            speed = baseSpeed;
        }

        Move();

        //if not attacking or defending, track mouse angle
        if (!attacking && !defending)
        { 
            AngleTrack();
        }
       
    }

    void Move()
    {
        Vector3 moveDirection = Vector3.zero;

        //Handle input
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        // Normalize move direction to ensure consistent speed regardless of diagonal movement
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        // Determine the angle of movement
        float angle = Vector3.SignedAngle(Vector3.up, moveDirection, Vector3.forward);

        // Assign sprite based on angle
        if (angle > -22.5f && angle <= 22.5f)
        {
            spriteRenderer.sprite = north;
        }
        else if (angle > 22.5f && angle <= 67.5f)
        {
            spriteRenderer.sprite = northWest;
        }
        else if (angle > 67.5f && angle <= 112.5f)
        {
            spriteRenderer.sprite = west;
        }
        else if (angle > 112.5f && angle <= 157.5f)
        {
            spriteRenderer.sprite = southWest;
        }
        else if (angle > 157.5f || angle <= -157.5f)
        {
            spriteRenderer.sprite = south;
        }
        else if (angle > -157.5f && angle <= -112.5f)
        {
            spriteRenderer.sprite = southEast;
        }
        else if (angle > -112.5f && angle <= -67.5f)
        {
            spriteRenderer.sprite = east;
        }
        else if (angle > -67.5f && angle <= -22.5f)
        {
            spriteRenderer.sprite = northEast;
        }

    }

    void AngleTrack()
    {
        // Compare mouse position to player position
        mousePosition = Input.mousePosition;
        playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Calculate differences in x and y coordinates
        float mouseXFromPlayer = mousePosition.x - playerPosition.x;
        float mouseYFromPlayer = mousePosition.y - playerPosition.y;

        // Calculate angle in degrees
        float angle = Mathf.Atan2(mouseYFromPlayer, mouseXFromPlayer) * Mathf.Rad2Deg;

        // Snap to the closest 45-degree angle
        pivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Round(angle / 45f) * 45f - 90));
    }

    void Attack()
    {
       //if attacking is false and left mouse button is pressed
       if (Input.GetMouseButtonDown(0) && !attacking)
        {
              StartCoroutine(AttackThrust());
         }
       else
        {
            return;
        }
    }

    IEnumerator AttackThrust()
    {
        // Attack with sword
        attacking = true;
        sword.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        sword.SetActive(false);
        attacking = false;
    }

    public void TakeDamage(float damage)
    {
       if (!defending)
        {
            health -= damage;
        }
        else
        {
            health -= damage * armor;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameDirector.GetComponent<GameDirectorBeta>().runInProgress = false;
        //Player death
        armor = baseArmor;
        damage = baseDamage;
        damagePenetration = baseDamagePenetration;
        health = baseHealth;
        maxHealth = baseMaxHealth;
        speed = baseSpeed;
        defending = false;
        shield.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        //reset
        gameDirector.GetComponent<GameDirectorBeta>().Reset();
    }

    public void Heal(float healing)
    {
        //heal player
        health += healing;
    }

    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dodging = true;
            StartCoroutine(DodgeRoll());
        }
    }

    IEnumerator DodgeRoll()
    {
            float dodgeLength = 0.5f; // Adjust the duration of the dodge roll
            float elapsedTime = 0f;

            // Perform the dodge roll
            while (elapsedTime < dodgeLength)
            {
                dodging = true;

                //move in the angle of the pivot
                transform.position += pivot.transform.up * speed * Time.deltaTime;

                // Check for collision with enemies or walls
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f); // Adjust the radius as needed
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Enemy") || collider.CompareTag("Walls")) // Adjust the tags as needed
                    {
                        // If collision detected, stop dodging
                        dodging = false;
                        yield break;
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            dodging = false;
    }

    void Defend()
    {
        //set shield active
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shield.SetActive(true);
            defending = true;
        }
        else
        {
            shield.SetActive(false);
            defending = false;
        }
    }

}
