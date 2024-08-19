using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satelite : Behaviour
{
    float myTime;

    float myNoiseX;
    float myNoiseY;
    [SerializeField] float myFloatingOffset;

    Vector3 myRotationDir;

    Vector3 myRotation;

    Vector3 myStartPosition;
    Quaternion myStartRotation;

    [SerializeField] float myRotationSpeed;
    [SerializeField] Transform myMeshTransform;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));

        myStartPosition = transform.position;
        myStartRotation = transform.rotation;

        myRotationDir = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f)).normalized;
    }

    public override void Move()
    {
        myTime += Time.deltaTime;
        myNoiseX += Time.deltaTime * 0.2f;
        myNoiseY += Time.deltaTime * 0.2f;

        Vector3 position = myStartPosition + (new Vector3(Mathf.PerlinNoise(myNoiseX, 0.0f) * myFloatingOffset, Mathf.PerlinNoise(myNoiseY, 0.0f) * myFloatingOffset, 0.0f) * Mathf.Clamp01(myTime));
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        myRotation += myRotationDir * myRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(myStartRotation, Quaternion.Euler(myRotation), Mathf.Clamp01(myTime));

        myMeshTransform.localEulerAngles += new Vector3(0.0f, myRotationSpeed, 0.0f) * Time.deltaTime;
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
        myNoiseX = 0.0f;
        myNoiseY = 0.0f;

        myRigidbody.useGravity = false;
    }
}
