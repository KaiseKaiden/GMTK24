using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform myPickupPoint;
    Transform myPlayer;
    Rigidbody myRigidbody;

    bool myIsDragging;

    [SerializeField] int myNestCapacity;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 myDirection = myPlayer.position - transform.position;
            myRigidbody.AddForceAtPosition(myDirection, myPickupPoint.position);
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            myRigidbody.centerOfMass = -myPickupPoint.localPosition;
            myIsDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            myRigidbody.ResetCenterOfMass();
            myIsDragging = false;
        }

        if (myIsDragging)
        {
            Vector3 myDirection = myPlayer.position - transform.position;
            myRigidbody.velocity = myDirection; /*myDirection - myPickupPoint.position
        }*/

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    public int GetCapacity()
    {
        return myNestCapacity;
    }
}
