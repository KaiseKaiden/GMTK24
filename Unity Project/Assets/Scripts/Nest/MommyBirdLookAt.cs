using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MommyBirdLookAt : MonoBehaviour
{
    Transform myPlayer;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(myPlayer);
    }
}
