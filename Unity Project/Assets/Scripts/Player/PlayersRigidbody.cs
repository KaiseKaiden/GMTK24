using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersRigidbody : MonoBehaviour
{
    [SerializeField] Rigidbody myRigidbodyHead;

    private void Update()
    {
        // Head
        Vector3 headVelocity = myRigidbodyHead.velocity;
        headVelocity.y = 10.0f;
        myRigidbodyHead.velocity = headVelocity;
    }
}
