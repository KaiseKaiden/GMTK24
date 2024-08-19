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
        Broken,

        Null
    };

    [SerializeField] Animator myAnimator;

    public State myState = State.IdleStable;
    State myPreviousState = State.Null;



    void Update()
    {
        switch (myState)
        {
            case State.IdleStable:
                {
                    IdleStable();
                    break;
                }
            case State.IdleFlicker:
                {
                    IdleFlicker();
                    break;
                }
            case State.IdleBroken:
                {
                    IdleBroken();
                    break;
                }
            case State.Broken:
                {
                    Broken();
                    break;
                }
        }
    }

    private void LateUpdate()
    {
        myPreviousState = myState;
    }

    public void Break()
    {
        myState = State.Broken;
    }

    bool IdleStable()
    {
        if (myPreviousState != myState)
        {
            myAnimator.SetTrigger("IdleStable");
            return true;
        }

        return false;
    }

    bool IdleFlicker()
    {
        if (myPreviousState != myState)
        {
            myAnimator.SetTrigger("IdleFlicker");
            return true;
        }

        return false;
    }

    bool IdleBroken()
    {
        if (myPreviousState != myState)
        {
            myAnimator.SetTrigger("IdleBroken");
            return true;
        }

        return false;
    }

    bool Broken()
    {
        if (myPreviousState != myState)
        {
            myAnimator.SetTrigger("Broken");
            return true;
        }

        return false;
    }
}