using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject mySettingsMenu;
    public GameObject myCreditsMenu;
    public GameObject myLeaderboardMenu;

    [SerializeField] GameObject myFadePrefab;

    public void StartGame()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.StartMainButtonEvent);

        Instantiate(myFadePrefab);
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

    public void QuitGame()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.QuitButtonEvent);
        Application.Quit();
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

    public void Credits()
    {
        AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.CreditButtonEvent);
        myCreditsMenu.SetActive(true);
    }
}
