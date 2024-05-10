using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyState
{
    Chasing,
    Damaged
}

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 200f;
    public float attackRange = 0.1f;
    public Transform player;
    public GameObject playerBody;

    public float maxHealth = 10; // Maximum health of the enemy
    public float currentHealth = 10; // Current health of the enemy
    public float damage = 1; // Damage dealt by the enemy

    //game director reference
    public GameObject GameDirector;

    private EnemyState currentState = EnemyState.Chasing;

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Chasing:
                ChasePlayer();
                break;

            case EnemyState.Damaged:
                Damaged();
                break;
        }
        

    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                // Move towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                // Rotate towards the player
                Vector2 direction = (player.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        }
    }

    //collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if collision is with player body do damage
            if (collision.gameObject == playerBody)
            {
                player.GetComponent<PlayerController>().TakeDamage(damage);
                //set state to damaged
            }
        }
    }


    private void Damaged()
    {
       StartCoroutine(DamagedState());
    }

    //coroutine for damaged state
    private IEnumerator DamagedState()
    {
        //set sprite color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //wait for a few seconds
        yield return new WaitForSeconds(0.2f);
        //reset sprite color
        GetComponent<SpriteRenderer>().color = Color.white;
        //set state back to idle
        currentState = EnemyState.Chasing;
    }


    public void TakeDamage(float damage)
    {
        //if not in damaged state, take damage
        if (currentState != EnemyState.Damaged)
        {
            currentHealth -= damage; // Reduce current health by the damage amount

            if (currentHealth <= 0)
            {
                Die(); // If health drops to or below 0, call the Die function
            }

            //set state to damaged
            currentState = EnemyState.Damaged;
        }
    }

    private void Die()
    {
        // Perform any death-related actions here, such as playing death animations, dropping items, etc.
        GameDirector.GetComponent<GameDirectorBeta>().EnemyDeath();
        Destroy(gameObject); // Destroy the enemy GameObject
    }

}
