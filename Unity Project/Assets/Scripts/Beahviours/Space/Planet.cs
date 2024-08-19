using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Behaviour
{
    float myTime;

    float myNoiseX;
    float myNoiseY;
    Vector3 myRotation;
    Quaternion myStartRotation;

    [SerializeField] float myRotationSpeed;
    [SerializeField] float myFloatOffset;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);
        myStartRotation = transform.rotation;
    }

    public override void Move()
    {
        myTime += Time.deltaTime;
        myNoiseX += Time.deltaTime;
        myNoiseY += Time.deltaTime;

        Vector3 position = transform.position;
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        myRotation = new Vector3(Mathf.PerlinNoise(myNoiseX, 0.0f) * myFloatOffset, myRotationSpeed * myTime, Mathf.PerlinNoise(0.0f, myNoiseY) * myFloatOffset);
        transform.rotation = Quaternion.Lerp(myStartRotation, Quaternion.Euler(myRotation), Mathf.Clamp01(myTime));
    }

    public override void Picked()
    {
        myIsBeingPicked = true;
    }

    public override void Dropped()
    {
        myIsBeingPicked = false;

        myStartRotation = transform.rotation;

        myTime = 0.0f;
        myRigidbody.useGravity = false;
    }
}
