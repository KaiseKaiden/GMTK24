using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Behaviour
{
    [SerializeField] TrailRenderer[] myTrails;

    float myTime;
    Vector3 myRotation;
    Quaternion myStartRotation;

    [SerializeField] float myRotationSpeed;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = Vector3.up * 2.5f;

        myStartRotation = transform.rotation;
    }

    public override void Move()
    {
        myTime += Time.deltaTime;

        Vector3 position = transform.position;
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        myRotation += new Vector3(0.0f, myRotationSpeed, 0.0f) * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(myRotation), Mathf.Clamp01(myTime));
    }

    public override void Picked()
    {
        myIsBeingPicked = true;

        myRigidbody.velocity = Vector3.zero;

        foreach(TrailRenderer t in myTrails)
        {
            t.emitting = false;
        }
    }

    public override void Dropped()
    {
        myIsBeingPicked = false;

        myStartRotation = transform.rotation;

        myTime = 0.0f;
        myRigidbody.velocity = Vector3.up * 2.5f;
        myRigidbody.useGravity = false;

        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = true;
        }
    }
}
