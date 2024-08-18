using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Behaviour
{
    float myRotation;
    float myNoiseX;
    float myNoiseY;

    [SerializeField] float myNoiseOffset;
    [SerializeField] TrailRenderer[] myTrails;

    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0.0f, -0.25f, 0.0f);
    }

    public override void Move()
    {
        myRotation += Time.deltaTime * 60.0f;

        myNoiseX += Time.deltaTime;
        myNoiseY += Time.deltaTime;

        Vector3 position = transform.position;
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        transform.eulerAngles = new Vector3(Mathf.PerlinNoise(myNoiseX, 0.0f) * myNoiseOffset, myRotation, Mathf.PerlinNoise(0.0f, myNoiseY) * myNoiseOffset);
    }

    public override void Picked()
    {
        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = false;
        }

        myRigidbody.velocity = Vector3.one;
    }

    public override void Dropped()
    {
        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = true;
        }

        myRigidbody.velocity = new Vector3(0.0f, -0.25f, 0.0f);
        myRigidbody.useGravity = false;
    }
}
