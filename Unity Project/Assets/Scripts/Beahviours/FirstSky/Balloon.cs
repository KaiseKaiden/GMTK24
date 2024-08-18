using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    [SerializeField] float mySwayLength;

    Rigidbody myRigidbody;

    private void Start()
    {
        myStartPosition = transform.position;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0.0f, 0.25f, 0.0f);
    }

    public override void Move()
    {
        myTime += Time.deltaTime;

        Vector3 position = transform.position;
        position.x = myStartPosition.x + (Mathf.Sin(myTime) * mySwayLength);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Sin(myTime) * mySwayLength * 10.0f);
    }

    public override void Picked()
    {
        myRigidbody.velocity = Vector3.one;
    }

    public override void Dropped()
    {
        myStartPosition = transform.position;

        myRigidbody.velocity = new Vector3(0.0f, 0.25f, 0.0f);
        myRigidbody.useGravity = false;
    }
}
