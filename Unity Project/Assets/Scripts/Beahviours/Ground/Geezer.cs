using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geezer : Behaviour
{
    public void SetVelocity(Vector2 velocity)
    {
        myVelocity = velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        myKillTime = 45f;
        myMoveSpeed = 0.35f;
    }

    public override void Move()
    {
        base.Move();

        if (!myIsBeingPicked && transform.position.y > 0)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        if (myIsBeingPicked && transform.position.y <= 0.5f)
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
