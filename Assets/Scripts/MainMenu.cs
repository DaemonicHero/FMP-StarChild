using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;

    void start()
    {
        //music
    }

    public void ControlsButton()
    {
        main.SetActive(false);
        controls.SetActive(true);
    }

    public void BackButton()
    {
        main.SetActive(true);
        controls.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        //load game scene
        SceneManager.LoadScene("main");
    }
}
