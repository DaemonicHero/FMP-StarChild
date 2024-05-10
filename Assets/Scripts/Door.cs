using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorType;
    public GameObject gameDirector;

   //on collision with player, set matching loot type and call RoomClear function from GameDirectorBeta
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (doorType == "Health")
            {
                gameDirector.GetComponent<GameDirectorBeta>().lootType = GameDirectorBeta.LootType.Health;
            }
            else if (doorType == "Armor")
            {
                gameDirector.GetComponent<GameDirectorBeta>().lootType = GameDirectorBeta.LootType.Armor;
            }
            else if (doorType == "Damage")
            {
                gameDirector.GetComponent<GameDirectorBeta>().lootType = GameDirectorBeta.LootType.Damage;
            }
            else if (doorType == "Speed")
            {
                gameDirector.GetComponent<GameDirectorBeta>().lootType = GameDirectorBeta.LootType.Speed;
            }
            else if (doorType == "DamagePenetration")
            {
                gameDirector.GetComponent<GameDirectorBeta>().lootType = GameDirectorBeta.LootType.DamagePenetration;
            }
            gameDirector.GetComponent<GameDirectorBeta>().DoorHide();
        }
    }

    public void DoorSet()
    {
        //random door type
        int DoorTyping = Random.Range(0, 5);
        if (DoorTyping == 0)
        {
            doorType = "Health";
        }
        else if (DoorTyping == 1)
        {
            doorType = "Armor";
        }
        else if (DoorTyping == 2)
        {
            doorType = "Damage";
        }
        else if (DoorTyping == 3)
        {
            doorType = "Speed";
        }
        else if (DoorTyping == 4)
        {
            doorType = "DamagePenetration";
        }
    }
}
