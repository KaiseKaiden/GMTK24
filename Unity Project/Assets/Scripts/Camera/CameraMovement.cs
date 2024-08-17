using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform myPlayer;
    public Transform myNest;

    [SerializeField] private float myCameraSmooting;
    [SerializeField] private float myDeciredZDistance = 10.0f;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //myNest = GameObject.FindGameObjectWithTag("Nest").transform;
    }

    private void Update()
    {
        myDeciredZDistance = (transform.position.y / 2) + 10;
        Vector3 myDirection = myPlayer.position - myNest.position;
        Vector3 myDeciredPosition = myNest.position + myDirection;
        myDeciredPosition.z = -myDeciredZDistance;

        transform.position = Vector3.Lerp(transform.position, myDeciredPosition, myCameraSmooting * Time.deltaTime);
    }
}