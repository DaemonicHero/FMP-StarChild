 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
   public GameObject gameDirector;
   public GameObject player;
   public GameObject upgradeMenu;
   public GameObject blacksmith;


    // Update is called once per frame
    void Update()
    {
        //if game not running and player is within range of blacksmith, show upgrade menu
        if(gameDirector.GetComponent<GameDirectorBeta>().runInProgress == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.transform.position, blacksmith.transform.position) < 2)
            {
                upgradeMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
