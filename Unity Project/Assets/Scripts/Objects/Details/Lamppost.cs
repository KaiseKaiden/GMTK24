using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamppost : MonoBehaviour
{
    public enum State
    {
        IdleStable,
        IdleFlicker,
        IdleBroken,
        Broken
    };

    [SerializeField] Material myUnlitMat;
    [SerializeField] Material myLitMat;

    public State myState = State.IdleStable;

    void Update()
    {
        switch(myState)
        {
            case State.IdleStable:
                {
                    IdleStable();
                    break;
                }
            case State.IdleFlicker:
                {
                    IdleStable();
                    break;
                }
            case State.IdleBroken:
                {
                    IdleFlicker();
                    break;
                }
            case State.Broken:
                {
                    Broken();
                    break;
                }
        }
    }

    public void Break()
    {
        myState = State.Broken;
    }

    void IdleStable()
    {

    }

    void IdleFlicker()
    {

    }

    void IdleBroken()
    {

    }

    void Broken()
    {

    }
}