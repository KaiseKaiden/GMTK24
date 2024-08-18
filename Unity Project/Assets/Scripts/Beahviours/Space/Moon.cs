using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float mySpinnSpeed;

    void Update()
    {
        transform.eulerAngles += new Vector3(0.0f, mySpinnSpeed, 0.0f) * Time.deltaTime;
    }
}
