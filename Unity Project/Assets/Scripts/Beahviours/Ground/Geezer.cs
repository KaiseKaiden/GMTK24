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
}
