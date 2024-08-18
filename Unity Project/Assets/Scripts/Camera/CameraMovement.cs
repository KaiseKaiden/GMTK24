using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform myPlayer;
    public Transform myNest;

    [SerializeField] private float myCameraSmooting;
    [SerializeField] private float myDeciredZDistance = 10.0f;
    [SerializeField] private float myXPositionLimit = 29.0f;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //myNest = GameObject.FindGameObjectWithTag("Nest").transform;
    }

    private void Update()
    {
        Vector3 myDirection = myPlayer.position - myNest.position;
        Vector3 myDeciredPosition = myNest.position + myDirection;
        myDeciredPosition.z = GameManager.Instance.GetZFromY(transform.position.y) - myDeciredZDistance; ;

        myDeciredPosition.x = Mathf.Clamp(myDeciredPosition.x, -myXPositionLimit, myXPositionLimit);

        transform.position = Vector3.Lerp(transform.position, myDeciredPosition, myCameraSmooting * Time.deltaTime);
    }
}