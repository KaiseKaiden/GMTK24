using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : Behaviour
{
    float myTime;
    Vector3 myStartPosition;
    Vector3 myLastPosition;

    float myNoiseX;
    float myNoiseY;

    [SerializeField] float myNoiseOffset;
    [SerializeField] TrailRenderer[] myTrails;

    void Start()
    {
        myStartPosition = transform.position;
    }

    void Update()
    {
        myTime += Time.deltaTime;

        myNoiseX += Time.deltaTime;
        myNoiseY += Time.deltaTime;

        Vector3 position = myStartPosition + new Vector3(Mathf.Cos(myTime * 0.5f) + (Mathf.PerlinNoise(myNoiseX, 0.0f) * myNoiseOffset * Mathf.Clamp01(myTime)), (Mathf.Sin(myTime) * 2.0f) + Mathf.PerlinNoise(0.0f, myNoiseY) * myNoiseOffset * Mathf.Clamp01(myTime), 0);
        position.z = GameManager.Instance.GetZFromY(position.y);
        transform.position = position;

        Vector3 direction = (position - myLastPosition).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        myLastPosition = position;
    }

    public override void Picked()
    {
        myStartPosition = transform.position;
        myLastPosition = transform.position;

        foreach(TrailRenderer t in myTrails)
        {
            t.emitting = false;
        }
    }

    public override void Dropped()
    {
        myStartPosition = transform.position;
        myLastPosition = transform.position;

        myTime = 0.0f;

        foreach (TrailRenderer t in myTrails)
        {
            t.emitting = true;
        }
    }
}
