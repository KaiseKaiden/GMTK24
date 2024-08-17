using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform myPickupPoint;
    Transform myPlayer;
    PlayerLevel myPlayerLevel;
    PlayerMovement myPlayerMovement;
    Rigidbody myRigidbody;
    Collider myCollider;

    [SerializeField] int myNestCapacity;
    [SerializeField] int myLevelRequred;
    [SerializeField] float myPickupDistance = 2.0f;

    bool myIsDragging;
    float myDraggingLerpTime;

    float myPerlinX;
    float myPerlinY;

    Vector3 myStartPosition;
    Quaternion myStartRotation;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        myPlayer = player.transform;
        myPlayerLevel = player.GetComponent<PlayerLevel>();
        myPlayerMovement = player.GetComponent<PlayerMovement>();

        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Vector3.Distance(transform.position, myPlayer.position) < myPickupDistance && myPlayerLevel.GetCurrentLevel() >= myLevelRequred)
        {
            myIsDragging = true;
            myRigidbody.isKinematic = true;
            myRigidbody.useGravity = false;
            //myCollider.enabled = false;

            myStartPosition = transform.position;
            myStartRotation = transform.rotation;

            transform.SetParent(myPlayerMovement.GetRightLeg());

            myDraggingLerpTime = 0.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            myIsDragging = false;
            myRigidbody.isKinematic = false;
            myRigidbody.useGravity = true;
            //myCollider.enabled = true;

            transform.SetParent(null);
        }

        if (myIsDragging)
        {
            myPerlinX += Time.deltaTime;
            myPerlinY += Time.deltaTime;

            transform.eulerAngles = new Vector3(Mathf.PerlinNoise(myPerlinX, 0.0f) * 50.0f, Mathf.PerlinNoise(0.0f, myPerlinY) * 50.0f, 0.0f);
            transform.eulerAngles = transform.eulerAngles + new Vector3(-myPlayerMovement.GetVelocity().x, 0.0f, 0.0f);

            Vector3 diffrence = (myPlayerMovement.GetRightLeg().position - myPickupPoint.position);


            myDraggingLerpTime += Time.deltaTime * 0.5f;
            transform.position = Vector3.Lerp(myStartPosition, transform.position + diffrence, Mathf.Clamp01(myDraggingLerpTime));
            transform.rotation = Quaternion.Lerp(myStartRotation, transform.rotation, Mathf.Clamp01(myDraggingLerpTime));
        }
        else
        {
            var position = transform.position;
            position.z = GameManager.Instance.GetZFromY(transform.position.y);
            transform.position = position;
        }
    }

    public int GetCapacity()
    {
        return myNestCapacity;
    }
}
