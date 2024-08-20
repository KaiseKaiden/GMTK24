using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject myPauseMenu;
    public GameObject myCreditsMenu;
    public GameObject mySettingsMenu;
    public GameObject myLeaderboardMenu;

    private void Start()
    {
        if (myPauseMenu.activeSelf)
        {
            myPauseMenu.SetActive(false);
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

    public void Credits()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.CreditButtonEvent);
        myCreditsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.BirdWingFlapEvent);
        myPauseMenu.SetActive(false);
    }

    public void CloseMenu(string aMenuName)
    {
        if (aMenuName == "Credits")
        {
            myCreditsMenu.SetActive(false);
        }
        else if (aMenuName == "Leaderboard")
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
