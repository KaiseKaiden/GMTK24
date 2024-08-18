using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersRigidbody : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidbodyHead;
    [SerializeField] Rigidbody myRigidbodyWindL;
    [SerializeField] Rigidbody myRigidbodyWindR;
    [SerializeField] float myFlapForce;

    private void Update()
    {
        // Head
        Vector3 headVelocity = myRigidbodyHead.velocity;
        headVelocity.y = 10.0f;
        myRigidbodyHead.velocity = headVelocity;
    }

    public void Flapp()
    {
        int dir = Random.Range(0, 1);
        if (dir == 0) dir = -1;

        dir = -1;

        myRigidbodyWindL.AddForceAtPosition(Vector3.up * myFlapForce * (float)dir, transform.position);
        myRigidbodyWindR.AddForceAtPosition(Vector3.up * myFlapForce * (float)dir, transform.position);
    }
}
