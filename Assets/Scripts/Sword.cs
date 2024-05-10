using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage; // Damage amount the player deals
    //reference to damage stat in player controller
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyHealth script component from the collided enemy
            EnemyAI enemyHealth = collision.gameObject.GetComponent<EnemyAI>();
            //get the damage stat from the player controller
            damage = playerController.damage * playerController.damagePenetration;

            //if enemy has any null references, destroy enemy
            if (enemyHealth == null)
            {
                Destroy(collision.gameObject);
            }

            // If the enemy has a health script
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damage);
                //heal player for every hit for enemy damage value
                playerController.Heal(enemyHealth.damage);
               
            }
            else
            {
                Debug.LogWarning("Enemy is missing EnemyHealth script.");
            }
        }
    }
}
