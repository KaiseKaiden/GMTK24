using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Start()
    {
        GameManager.Instance = this;
    }

    public float GetZFromY(float aY)
    {
        return -aY * 0.5f;
    }
}
