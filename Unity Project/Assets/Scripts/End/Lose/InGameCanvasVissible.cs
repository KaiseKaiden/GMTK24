using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasVissible : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.GetIsGameOver() || GameManager.Instance.GetIsMoonCollected())
        {
            gameObject.SetActive(false);
        }
    }
}
