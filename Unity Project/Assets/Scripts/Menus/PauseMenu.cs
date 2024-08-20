using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject myPauseMenu;
    public GameObject mySettingsMenu;
    public GameObject myLeaderboardMenu;

    private void Start()
    {
        if (myPauseMenu.activeSelf)
        {
            myPauseMenu.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            myPauseMenu.SetActive(true);
        }
    }

    public void Settings()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.SettingsButtonEvent);
        mySettingsMenu.SetActive(true);
    }

    public void Leaderboard()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.LeaderBoardButtonEvent);
        myLeaderboardMenu.SetActive(true);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.BirdWingFlapEvent);
        myPauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CloseMenu(string aMenuName)
    {
        if (aMenuName == "Leaderboard")
        {
            myLeaderboardMenu.SetActive(false);
        }
        else if (aMenuName == "Settings")
        {
            mySettingsMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Invalid Menu Name");
        }
    }

}
