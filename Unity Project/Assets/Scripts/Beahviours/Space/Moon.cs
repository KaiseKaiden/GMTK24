using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float mySpinnSpeed;
    bool myHasDestroyedEarth = false;

    void Update()
    {
        transform.eulerAngles += new Vector3(0.0f, mySpinnSpeed, 0.0f) * Time.deltaTime;

        if (transform.position.y < 50.0f && !myHasDestroyedEarth)
        {
            Debug.Log("End Game");
            myHasDestroyedEarth = true;
        }
    }
}
