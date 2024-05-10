using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject gameDirector;

   //on collision with player, destroy loot object and call LootPickup function from GameDirectorBeta
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameDirector.GetComponent<GameDirectorBeta>().LootPickup();
        }
    }
}
