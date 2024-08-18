using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    Vector3 myLastPosition;

    [SerializeField] float myMapWidth;
    [SerializeField] float myAngularTilt;
    [SerializeField] TrailRenderer[] myTrails;
    Rigidbody myRigidbody;

    void Start()
    {
        myStartPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        myTime += Time.deltaTime * 0.25f;

        float dir = Mathf.Sin(myTime);

        Vector3 position = new Vector3(dir * Mathf.Clamp01(myTime) * myMapWidth, myStartPosition.y, 0.0f);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        Vector3 direction = (position - myLastPosition);
        direction.z = 0.0f;
        direction.Normalize();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 5.0f * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Sin(myTime * 10.0f) * myAngularTilt);

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
