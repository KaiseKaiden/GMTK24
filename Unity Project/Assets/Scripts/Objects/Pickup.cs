using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    Transform myPickupPoint;
    PlayerMovement myPlayerMovement;
    Rigidbody myRigidbody;

    [SerializeField]
    int myNestCapacity;
    [SerializeField]
    int myLevelRequred;

    bool myIsDragging;
    float myDraggingLerpTime;

    float myPerlinX;
    float myPerlinY;

    Vector3 myStartPosition;
    Quaternion myStartRotation;

    Behaviour myBehaviour;

    Vector3 myDeciredScale;
    float myScaleTimer = 0.0f;
    bool myHasScaled = false;

    private void Start()
    {
        myPlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        myRigidbody = GetComponent<Rigidbody>();
        myBehaviour = GetComponent<Behaviour>();

        myDeciredScale = transform.localScale;
    }
    public IEnumerator MoveTowardPoint(Vector3 point)
    {
        myRigidbody.useGravity = false;
        myIsDragging = false;
        myRigidbody.isKinematic = true;
        GetComponent<Pickup>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        float maxTime = Time.time + 10;
        while ((point - transform.position).sqrMagnitude > 1.0f && maxTime > Time.time)
        {
            transform.Translate(Time.deltaTime * (point - transform.position).normalized * 10.0f);
            yield return null;
        }
        NestCreator.myNestCreator.Increment(gameObject, myNestCapacity);
    }
    private void Update()
    {
        if (myIsDragging)
        {
            if (tag != "Moon")
            {
                myPerlinX += Time.deltaTime;
                myPerlinY += Time.deltaTime;

                transform.eulerAngles = new Vector3(Mathf.PerlinNoise(myPerlinX, 0.0f) * 50.0f,
                                                    Mathf.PerlinNoise(0.0f, myPerlinY) * 50.0f, 0.0f);
                transform.eulerAngles =
                    transform.eulerAngles + new Vector3(-myPlayerMovement.GetVelocity().x, 0.0f, 0.0f);
            }

            Vector3 diffrence = (myPlayerMovement.GetRightLeg().position - myPickupPoint.position);

            myDraggingLerpTime += Time.deltaTime;
            transform.position = Vector3.Lerp(myStartPosition, transform.position + diffrence,
                                              EaseOutCirc(Mathf.Clamp01(myDraggingLerpTime)));

            transform.rotation =
                Quaternion.Lerp(myStartRotation, transform.rotation, EaseOutCirc(Mathf.Clamp01(myDraggingLerpTime)));
        }
        else
        {
            if (myRigidbody.velocity.sqrMagnitude > .1f)
            {
                Ray ray = new Ray(transform.position, Vector3.forward * 100.0f);
                var arg = Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Nest"));
                if (arg)
                {

                    {
                        StartCoroutine(MoveTowardPoint(hit.point));
                    }
                }
            }

            var position = transform.position;
            position.z = GameManager.Instance.GetZFromY(transform.position.y);
            transform.position = Vector3.Lerp(transform.position, position, 3.5f * Time.deltaTime);
        }

        // Scale Up
        if (!myHasScaled)
        {
            myScaleTimer += Time.deltaTime;
            transform.localScale = myDeciredScale * EaseOutElastic(Mathf.Clamp01(myScaleTimer));

            if (myScaleTimer >= 1.0f)
            {
                myHasScaled = true;
            }
        }
    }

    public void Pick()
    {
        myIsDragging = true;

        if (myRigidbody.isKinematic == true)
        {
            Lamppost lamppost = GetComponent<Lamppost>();
            if (lamppost != null)
            {
                lamppost.Break();
            }
        }

        myRigidbody.isKinematic = true;
        myRigidbody.useGravity = false;

        myStartPosition = transform.position;
        myStartRotation = transform.rotation;

        myDraggingLerpTime = 0.0f;

        if (myBehaviour != null)
        {
            myBehaviour.Picked();
        }
        AudioManager.instance.PlayOneshot(FMODEvents.instance.GetNestMaterialEvent, transform.position);
    }

    public void Drop()
    {
        myIsDragging = false;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = true;
        CharacterController CC;
        if (myPlayerMovement.TryGetComponent(out CC))
        {
            myRigidbody.velocity = CC.velocity;
        }

        if (myBehaviour != null)
        {
            myBehaviour.Dropped();
        }
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

    float EaseOutElastic(float aValue)
    {
        const float c4 = (2.0f * Mathf.PI) / 3.0f;

        return aValue == 0.0f ? 0.0f : aValue == 1.0f ? 1.0f : Mathf.Pow(2.0f, -10.0f * aValue) * Mathf.Sin((aValue * 10.0f - 0.75f) * c4) + 1.0f;
    }
}
