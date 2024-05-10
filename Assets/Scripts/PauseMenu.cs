using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    public GameObject director;
    public GameObject player;

    public TMP_Text currentHealth;
    public TMP_Text maxHealth;
    public TMP_Text currentArmor;
    public TMP_Text currentDamage;
    public TMP_Text currentSpeed;
    public TMP_Text currentDamagePenetration;
    public TMP_Text currentRoomClear;



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        //set currenthealth text to player health
        currentHealth.text = player.GetComponent<PlayerController>().health.ToString();
        //set currentarmor text to player armor
        currentArmor.text = player.GetComponent<PlayerController>().armor.ToString();
        //set currentdamage text to player damage
        currentDamage.text = player.GetComponent<PlayerController>().damage.ToString();
        //set currentspeed text to player speed
        currentSpeed.text = player.GetComponent<PlayerController>().speed.ToString();

        maxHealth.text = player.GetComponent<PlayerController>().maxHealth.ToString();
        currentDamagePenetration.text = player.GetComponent<PlayerController>().damagePenetration.ToString();
        currentRoomClear.text = director.GetComponent<GameDirectorBeta>().roomCount.ToString();

        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
