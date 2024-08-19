using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    GameObject myPauseMenu;
    GameObject mySettingsMenu;

    private void Start()
    {
        if (transform.Find("PauseMenu")) 
        {
            myPauseMenu = transform.Find("PauseMenu").gameObject;     
        }
        if (transform.Find("SettingsMenu"))
        {
            mySettingsMenu = transform.Find("SettingsMenu").gameObject;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainLevel")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;

                if (!myPauseMenu.activeSelf) 
                {
                    myPauseMenu.SetActive(true);
                }
                else
                {
                    if (!mySettingsMenu.activeSelf)
                    {
                        Continue();
                    }
                    else
                    {
                        SettingsPauseMenuToggle();
                    }
                }
            }
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SettingsPauseMenuToggle()
    {
        mySettingsMenu.SetActive(!mySettingsMenu.activeSelf);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Continue()
    {
        myPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
