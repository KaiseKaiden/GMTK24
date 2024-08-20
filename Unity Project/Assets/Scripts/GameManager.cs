using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float myBottomLimit;
    [SerializeField] float myLimitIncrease;

    bool myMoonCollected;
    float myCurrentDistance;

    bool myIsGameOver = false;

    void Start()
    {
        GameManager.Instance = this;
    }

    public float GetZFromY(float aY)
    {
        return -aY * 0.5f;
    }

    public float GetXLimitFromY(float aY)
    {
        return myBottomLimit + aY * myLimitIncrease;
    }

    public float GetCamDistanceFromY(float aY)
    {
        if (myMoonCollected)
        {
            return myCurrentDistance;
        }

        myCurrentDistance = aY * myLimitIncrease * 0.5f;
        return myCurrentDistance;
    }

    public float GetFoodScaleFromY(float aY)
    {
        return 1.0f * (aY / 300.0f) * 10.0f;
    }

    public void MoonCollected()
    {
        myMoonCollected = true;
    }

    public void GameOver()
    {
        myIsGameOver = true;
    }

    public bool GetIsGameOver()
    {
        return myIsGameOver;
    }

    public bool GetIsMoonCollected()
    {
        return myMoonCollected;
    }
}
