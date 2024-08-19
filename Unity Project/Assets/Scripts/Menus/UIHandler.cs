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

        if (true)
        {

        }
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.MusicEvent);
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
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);
        SceneManager.LoadScene("MainLevel");
    }

    public void Settings()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.SettingsButtonEvent);
        SceneManager.LoadScene("Settings");
    }

    public void SettingsPauseMenuToggle()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.SettingsButtonEvent);
        mySettingsMenu.SetActive(!mySettingsMenu.activeSelf);
    }

    public void Leaderboard()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.LeaderBoardButtonEvent);
        SceneManager.LoadScene("Leaderboard");
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.QuitButtonEvent);
        Application.Quit();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.CreditButtonEvent);
        SceneManager.LoadScene("Credits");
    }

    public void Continue()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);
        myPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
