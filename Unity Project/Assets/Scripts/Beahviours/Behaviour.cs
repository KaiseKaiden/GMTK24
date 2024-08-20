using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    float myTimeAlive = 0f;
    protected float myKillTime = 15f;
    protected float myMoveSpeed = 5f;

    protected bool myIsBeingPicked = false;

    protected Vector2 myVelocity = Vector2.zero;
    protected Vector3 myLastPosition = Vector3.zero;

    Rigidbody myRigidBody;

    MeshRenderer[] myMeshRenderers;
    float myBlinkTime;

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

        myMeshRenderers = GetComponentsInChildren<MeshRenderer>();
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
                float totalBlinkTime = (myTimeAlive - myKillTime) * 4.0f;

                foreach(MeshRenderer m in myMeshRenderers)
                {
                    if (((int)totalBlinkTime % 2) == 0)
                    {
                        m.enabled = false;
                    }
                    else
                    {
                        m.enabled = true;
                    }

                }

                if (totalBlinkTime > 7.0f)
                {
                    Destroy(gameObject);
                }
            }

            Move();
        }

        myLastPosition = transform.position;
    }
}
