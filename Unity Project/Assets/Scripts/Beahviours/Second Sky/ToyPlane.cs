using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPlane : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    Vector3 myLastPosition;

    [SerializeField] float myLoopRadius;
    [SerializeField] TrailRenderer[] myTrails;
    Rigidbody myRigidbody;

    void Start()
    {
        myStartPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        myTime += Time.deltaTime;

        Vector3 position = myStartPosition + new Vector3(Mathf.Cos(myTime) * Mathf.Clamp01(myTime) * myLoopRadius, Mathf.Sin(myTime) * Mathf.Clamp01(myTime) * myLoopRadius, 0);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        Vector3 direction = (position - myLastPosition);
        direction.z = 0.0f;
        direction.Normalize();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 5.0f * Time.deltaTime);

        myLastPosition = position;
    }

    public override void Picked()
    {
        myIsBeingPicked = true;

        myStartPosition = transform.position;
        myLastPosition = transform.position;

        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = false;
        }
    }

    public override void Dropped()
    {
        myIsBeingPicked = false;

        myStartPosition = transform.position;
        myLastPosition = transform.position;

        myTime = 0.0f;
        myRigidbody.useGravity = false;

        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = true;
        }
    }
}
