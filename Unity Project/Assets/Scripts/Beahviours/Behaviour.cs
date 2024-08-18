using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    float myTimeAlive = 0f;
    float myKillTime = 15f;
    float myMoveSpeed = 5f;

    bool myIsBeingPicked = false;

    Vector2 myVelocity = Vector2.zero;

    Rigidbody myRigidBody;

    private void Awake()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();

        if (myRigidBody == null)
        {
            myRigidBody = gameObject.AddComponent<Rigidbody>();
        }

        myRigidBody.useGravity = false;

        if (transform.position.x <= 0)
        {
            myVelocity.x = 1f;
        }
        else if (transform.position.x > 0)
        {
            myVelocity.x = -1f;
        }
    }

    public virtual void Picked()
    {
        myIsBeingPicked = true;
    }

    public virtual void Dropped()
    {
        myIsBeingPicked = false;
    }

    public virtual void Move()
    {
        Vector3 pos = transform.position;

        pos.x += myVelocity.x * myMoveSpeed * Time.deltaTime;

        transform.position = pos;
    }

    private void Update()
    {
        if (!myIsBeingPicked)
        {
            myTimeAlive += Time.deltaTime;

            if (myTimeAlive >= myKillTime)
            {
                Destroy(gameObject);
            }

            Move();
        }
    }
}
