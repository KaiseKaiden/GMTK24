using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool myCanMove = true;

    [SerializeField] LayerMask myWhatIsMoon;

    [SerializeField] private float myForce;
    [SerializeField] private float myGravity;
    float myWingTrailTime = 3.0f;

    [SerializeField] private float myMaxFallSpeed;
    [SerializeField] private float myMaxGlideSpeed;
    [SerializeField] private float myRotationSmooting;
    private Vector3 myVelocity;
    private CharacterController myController;

    [SerializeField] private TrailRenderer[] myTrails;
    private Vector3 myLookDirection;

    [SerializeField] Transform myRightLeg;

    [SerializeField] float myXPositionLimit = 29.0f;
    [SerializeField] float myYPositionLimit = 300.0f;

    [SerializeField] ParticleSystem myWingFlapPart;

    private void Start()
    {
        myLookDirection = transform.forward;

        myController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!GameManager.Instance.GetIsGameOver())
        {
            // Flap
            if (myCanMove)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                    direction.z = 0.0f;
                    direction.Normalize();

                    myVelocity = direction * myForce * transform.localScale.x;

                    myWingFlapPart.Play();

                    AudioManager.instance.PlayOneshot(FMODEvents.instance.BirdWingFlapEvent, transform.position);

                    GetComponent<PlayersRigidbody>().Flapp();

                    foreach (TrailRenderer t in myTrails)
                    {
                        t.emitting = true;
                    }
                    myWingTrailTime = 0.0f;
                }
            }

            // Wind Trail
            if (myWingTrailTime > 0.2f)
            {
                foreach (TrailRenderer t in myTrails)
                {
                    t.emitting = false;
                }
            }
            else
            {
                myWingTrailTime += Time.deltaTime;
            }

            // Gravity
            myVelocity.y -= myGravity * Time.deltaTime;
            if (myVelocity.y < -myMaxFallSpeed)
            {
                myVelocity.y = -myMaxFallSpeed;
            }

            // Glide
            //if (myCanMove)
            //{
            //    if (Input.GetMouseButton(0))
            //    {
            //        if (myVelocity.y < -myMaxGlideSpeed)
            //        {
            //            myVelocity.y = -myMaxGlideSpeed;

            //            // Add Glide Speed
            //            float directionX = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
            //            if (directionX < 0) directionX = -1.0f;
            //            if (directionX > 0) directionX = 1.0f;

            //            myVelocity.x += directionX * myForce * transform.localScale.x * 2.0f * Time.deltaTime;
            //            myVelocity.x = Mathf.Clamp(myVelocity.x, -myForce * transform.localScale.x, myForce * transform.localScale.x);

            //            foreach (TrailRenderer t in myTrails)
            //            {
            //                t.emitting = true;
            //            }
            //        }
            //    }
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    foreach (TrailRenderer t in myTrails)
            //    {
            //        t.emitting = false;
            //    }
            //}

            // Slow Down When Grounded
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

            // Restrict Player Position
            float widthLimit = GameManager.Instance.GetXLimitFromY(position.y);
            position.x = Mathf.Clamp(position.x, -widthLimit, widthLimit);

            position.y = Mathf.Clamp(position.y, 0.0f, myYPositionLimit);

            // Stop Head Bopping Into Objects
            RaycastHit hit;
            if (position.y >= myYPositionLimit || (myVelocity.y > 0.0f && Physics.SphereCast(transform.position, transform.localScale.x * 0.5f, Vector3.up, out hit, 1.0f, myWhatIsMoon)))
            {
                myVelocity.y = -0.5f;
            }

            position.z = GameManager.Instance.GetZFromY(transform.position.y);
            transform.position = position;
        }
    }

    public Transform GetRightLeg()
    {
        return myRightLeg;
    }

    public Vector3 GetVelocity()
    {
        return myVelocity;
    }

    public void DeactivateMovement()
    {
        myCanMove = false;

        myVelocity.x = 0.0f;
        myVelocity.y = 0.0f;
    }
}