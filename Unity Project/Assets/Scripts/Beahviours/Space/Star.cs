using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    Vector3 myRotation;
    Quaternion myStartRotation;

    [SerializeField] float myRotationSpeed;
    [SerializeField] float myFloatOffset;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);
        myStartPosition = transform.position;
        myStartRotation = transform.rotation;
    }

    public override void Move()
    {
        myTime += Time.deltaTime;

        Vector3 position = myStartPosition + new Vector3(0.0f, Mathf.Sin(myTime) * myFloatOffset, 0.0f);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        myRotation += new Vector3(0.0f, myRotationSpeed, 0.0f) * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(myStartRotation, Quaternion.Euler(myRotation), Mathf.Clamp01(myTime));
    }

    public override void Picked()
    {
        myIsBeingPicked = true;
    }

    public override void Dropped()
    {
        myIsBeingPicked = false;

        myStartPosition = transform.position;
        myStartRotation = transform.rotation;

        myTime = 0.0f;
        myRigidbody.useGravity = false;
    }
}
