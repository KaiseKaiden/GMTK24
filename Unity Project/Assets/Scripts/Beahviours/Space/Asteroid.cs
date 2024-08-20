using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Behaviour
{
    [SerializeField] TrailRenderer mySwooshTrail;

    Vector3 myRotationDir;
    Vector3 myMoveDir;
    [SerializeField] float myRotationSpeed;
    [SerializeField] float mySpeed;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
        myRotationDir = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f)).normalized;

        myMoveDir = (new Vector3(0.0f, transform.position.y, transform.position.z) - transform.position);
        myMoveDir.y = Random.Range(-10.0f, 10.0f);

        myMoveDir.Normalize();

        mySwooshTrail.emitting = true;
    }

    public override void Move()
    {
        Vector3 position = transform.position + (myMoveDir * mySpeed * Time.deltaTime);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        transform.eulerAngles += myRotationDir * myRotationSpeed * Time.deltaTime;
    }

    public override void Picked()
    {
        myIsBeingPicked = true;

        mySwooshTrail.emitting = false;
    }

    public override void Dropped()
    {
        myIsBeingPicked = false;

        myRigidbody.useGravity = false;

        mySwooshTrail.emitting = true;

        myMoveDir = (transform.position - myLastPosition);
        myMoveDir.z = 0.0f;

        mySpeed = myMoveDir.magnitude;

        myMoveDir.Normalize();
    }
}
