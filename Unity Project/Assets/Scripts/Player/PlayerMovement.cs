using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float myForce;
    [SerializeField] private float myGravity;

    [SerializeField] private float myMaxFallSpeed;
    [SerializeField] private float myMaxGlideSpeed;
    [SerializeField] private float myRotationSmooting;
    private Vector3 myVelocity;
    private CharacterController myController;

    [SerializeField] private TrailRenderer[] myTrails;
    private Vector3 myLookDirection;

    [SerializeField] Transform myRightLeg;

    [SerializeField] float myXPositionLimit = 29.0f;
    [SerializeField] float myYPositionLimit = 150.0f;

    [SerializeField] ParticleSystem myWingFlapPart;

    private void Start()
    {
        myLookDirection = transform.forward;

        myController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Flap
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            direction.z = 0.0f;
            direction.Normalize();

            myVelocity = direction * myForce * transform.localScale.x;

            myWingFlapPart.Play();

            AudioManager.instance.PlayOneshot(FMODEvents.instance.BirdWingFlapEvent,transform.position);

            GetComponent<PlayersRigidbody>().Flapp();
        }

        // Gravity
        myVelocity.y -= myGravity * Time.deltaTime;
        if (myVelocity.y < -myMaxFallSpeed)
        {
            myVelocity.y = -myMaxFallSpeed;
        }

        // Glide
        if (Input.GetMouseButton(0))
        {
            if (myVelocity.y < -myMaxGlideSpeed)
            {
                myVelocity.y = -myMaxGlideSpeed;

                // Add Glide Speed
                float directionX = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
                if (directionX < 0) directionX = -1.0f;
                if (directionX > 0) directionX = 1.0f;

                myVelocity.x += directionX * myForce * transform.localScale.x * 2.0f * Time.deltaTime;
                myVelocity.x = Mathf.Clamp(myVelocity.x, -myForce * transform.localScale.x, myForce * transform.localScale.x);

                foreach(TrailRenderer t in myTrails)
                {
                    t.emitting = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (TrailRenderer t in myTrails)
            {
                t.emitting = false;
            }
        }

        if (myController.isGrounded)
        {
            if (myVelocity.y < 0.0f)
            {
                myVelocity = Vector3.Lerp(myVelocity, Vector3.zero, 5.0f * Time.deltaTime);
                myVelocity.y = -0.5f;
            }

            myLookDirection = new Vector3(0.0f, 0.0f, -1.0f);
        }
        else
        {
            myLookDirection = myVelocity.normalized;
        }

        myController.Move(myVelocity * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(myLookDirection), myRotationSmooting * Time.deltaTime);
        var position = transform.position;
        position.x = Mathf.Clamp(position.x, -myXPositionLimit, myXPositionLimit);
        position.y = Mathf.Clamp(position.y, 0.0f, myYPositionLimit);

        if (position.y >= myYPositionLimit)
        {
            myVelocity.y = -0.5f;
        }

        position.z = GameManager.Instance.GetZFromY(transform.position.y);
        transform.position = position;


        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    transform.localScale *= 1.25f;
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    transform.localScale *= .75f;
        //}

        // Change Skybox Material
        RenderSettings.skybox.SetFloat("_TestHeight", transform.position.y);
    }

    public Transform GetRightLeg()
    {
        return myRightLeg;
    }

    public Vector3 GetVelocity()
    {
        return myVelocity;
    }
}