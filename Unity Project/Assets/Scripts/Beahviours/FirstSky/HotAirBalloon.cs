using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloon : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    [SerializeField] float mySwayLength;
    [SerializeField] ParticleSystem myFireParticle;
    bool myPartIsActive = true;

    Rigidbody myRigidbody;

    private void Start()
    {
        myStartPosition = transform.position;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0.25f, 0.25f, 0.0f);
    }

    public override void Move()
    {
        myTime += Time.deltaTime * 0.25f;

        Vector3 position = transform.position;
        position.y = myStartPosition.y + (Mathf.Sin(myTime) * mySwayLength);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        if (Mathf.Sin(myTime) > 0.8f)
        {
            myPartIsActive = false;
        }
        else if (Mathf.Sin(myTime) < -0.8f)
        {
            myPartIsActive = true;
        }

        var emission = myFireParticle.emission;
        emission.enabled = myPartIsActive;
    }

    public override void Picked()
    {
        myRigidbody.velocity = Vector3.one;
    }

    public override void Dropped()
    {
        myTime = 0.0f;
        myPartIsActive = true;
        myStartPosition = transform.position;

        myRigidbody.velocity = new Vector3(0.25f, 0.0f, 0.0f);
        myRigidbody.useGravity = false;
    }
}
