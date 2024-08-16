using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform myPlayer;
    Transform myNest;

    [SerializeField] float myCameraSmooting;
    [SerializeField] float myDeciredZDistance = 10.0f;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        myNest = GameObject.FindGameObjectWithTag("Nest").transform;
    }

    void Update()
    {
        Vector3 myDirection = myPlayer.position - myNest.position;
        Vector3 myDeciredPosition = myNest.position + myDirection;
        myDeciredPosition.z = -myDeciredZDistance;

        transform.position = Vector3.Lerp(transform.position, myDeciredPosition, myCameraSmooting * Time.deltaTime);
    }
}