using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform myPickupPoint;
    Transform myPlayer;
    Rigidbody myRigidbody;

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

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }
}
