using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform myPickupPoint;
    Transform myPlayer;
    PlayerLevel myPlayerLevel;
    Rigidbody myRigidbody;

    [SerializeField] int myNestCapacity;
    [SerializeField] int myLevelRequred;
    [SerializeField] float myPickupDistance = 2.0f;

    bool myIsDragging;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        myPlayer = player.transform;
        myPlayerLevel = player.GetComponent<PlayerLevel>();

        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Vector3.Distance(transform.position, myPlayer.position) < myPickupDistance && myPlayerLevel.GetCurrentLevel() >= myLevelRequred)
        {
            myIsDragging = true;
            myRigidbody.isKinematic = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            myIsDragging = false;
        }

        if (myIsDragging)
        {
            Vector3 myDirection = myPlayer.position - transform.position;
            myRigidbody.AddForceAtPosition(myDirection, myPickupPoint.position);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    public int GetCapacity()
    {
        return myNestCapacity;
    }
}
