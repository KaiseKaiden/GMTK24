using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    float myNoiseTime;
    [SerializeField] float mySwayOffset;
    [SerializeField] float mySwaySpeed;

    Vector3 myStartRotation;

    private void Start()
    {
        myStartRotation = transform.eulerAngles;
    }

    void Update()
    {
        myNoiseTime += mySwaySpeed * Time.deltaTime;

        transform.eulerAngles = myStartRotation + new Vector3(Mathf.PerlinNoise(myNoiseTime, 0.0f) * mySwayOffset, Mathf.PerlinNoise(0.0f, myNoiseTime) * mySwayOffset, 0.0f);
    }
}
