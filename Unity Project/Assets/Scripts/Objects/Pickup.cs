using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform myPickupPoint;
    PlayerMovement myPlayerMovement;
    Rigidbody myRigidbody;

    [SerializeField] int myNestCapacity;
    [SerializeField] int myLevelRequred;

    bool myIsDragging;
    float myDraggingLerpTime;

    float myPerlinX;
    float myPerlinY;

    Vector3 myStartPosition;
    Quaternion myStartRotation;

    private void Start()
    {
        myPlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (myIsDragging)
        {
            myPerlinX += Time.deltaTime;
            myPerlinY += Time.deltaTime;

            transform.eulerAngles = new Vector3(Mathf.PerlinNoise(myPerlinX, 0.0f) * 50.0f, Mathf.PerlinNoise(0.0f, myPerlinY) * 50.0f, 0.0f);
            transform.eulerAngles = transform.eulerAngles + new Vector3(-myPlayerMovement.GetVelocity().x, 0.0f, 0.0f);

            Vector3 diffrence = (myPlayerMovement.GetRightLeg().position - myPickupPoint.position);


            myDraggingLerpTime += Time.deltaTime;
            transform.position = Vector3.Lerp(myStartPosition, transform.position + diffrence, EaseOutCirc(Mathf.Clamp01(myDraggingLerpTime)));
            transform.rotation = Quaternion.Lerp(myStartRotation, transform.rotation, EaseOutCirc(Mathf.Clamp01(myDraggingLerpTime)));
        }
        else
        {
            var position = transform.position;
            position.z = GameManager.Instance.GetZFromY(transform.position.y);
            transform.position = Vector3.Lerp(transform.position, position, 3.5f * Time.deltaTime);
        }
    }

    public void Pick()
    {
        myIsDragging = true;
        myRigidbody.isKinematic = true;
        myRigidbody.useGravity = false;

        myStartPosition = transform.position;
        myStartRotation = transform.rotation;

        myDraggingLerpTime = 0.0f;
    }

    public void Drop()
    {
        myIsDragging = false;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = true;
    }

    public int GetCapacity()
    {
        return myNestCapacity;
    }

    public int GetLevelRequired()
    {
        return myLevelRequred;
    }

    float EaseOutCirc(float aValue)
    {
        return Mathf.Sqrt(1.0f - Mathf.Pow(aValue - 1.0f, 2.0f));
    }
}
