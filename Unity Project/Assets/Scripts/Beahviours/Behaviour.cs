using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    float myTimeAlive = 0f;
    protected float myKillTime = 6f;
    protected float myMoveSpeed = 0f;

    bool myIsBeingPicked = false;
    bool myUseDeathShader = false;

    protected Vector2 myVelocity = Vector2.zero;

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


            if (myKillTime - myTimeAlive < 5f)
            {
                if (!myUseDeathShader) 
                {
                    if(GetComponent<MeshRenderer>())
                    {
                        GetComponent<MeshRenderer>().material = new Material(Resources.Load<Material>("PickUp"));
                        GetComponent<MeshRenderer>().material.EnableKeyword("_ISABOUTTODIE");
                    }
                    else if (GetComponent<SkinnedMeshRenderer>()) 
                    {
                        GetComponent<SkinnedMeshRenderer>().material = new Material(Resources.Load<Material>("PickUp"));
                        GetComponent<SkinnedMeshRenderer>().material.EnableKeyword("_ISABOUTTODIE");
                    }
                    myUseDeathShader = true;
                }

                float range01 = 1 - ((myKillTime - myTimeAlive) / 5);

                Debug.Log(range01);

                GetComponent<MeshRenderer>().material.SetFloat("_TimeUntilDeath", range01);
            }

            if (myTimeAlive >= myKillTime)
            {
                Destroy(gameObject);
            }

            Move();
        }
    }
}
