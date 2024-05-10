using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterDoor : MonoBehaviour
{
   public GameObject gameDirector;
   public GameObject player;

    //on collision with player trigger run start
    private void OnCollisionEnter2D(Collision2D collision)
    {


            gameDirector.GetComponent<GameDirectorBeta>().StartRun();
        
    }
}
