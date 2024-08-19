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
        AudioManager.instance.PlayOneshot(FMODEvents.instance.MusicEvent, transform.position);
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
        AudioManager.instance.PlayOneshot(FMODEvents.instance.StartMainButtonEvent, transform.position);
        SceneManager.LoadScene("MainLevel");
    }

    public void Settings()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.SettingsButtonEvent, transform.position);
        SceneManager.LoadScene("Settings");
    }

    public void SettingsPauseMenuToggle()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.SettingsButtonEvent, transform.position);
        mySettingsMenu.SetActive(!mySettingsMenu.activeSelf);
    }

    public void Leaderboard()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.LeaderBoardButtonEvent, transform.position);
        SceneManager.LoadScene("Leaderboard");
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.QuitButtonEvent, transform.position);
        Application.Quit();
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.StartMainButtonEvent, transform.position);
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.CreditButtonEvent, transform.position);
        SceneManager.LoadScene("Credits");
    }

    public void Continue()
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.StartMainButtonEvent, transform.position);
        myPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
