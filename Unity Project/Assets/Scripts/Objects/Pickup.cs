using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    bool haveBeenPicked = false;
    Transform myPlayer;
    Material myOutlineMaterial;
    Vector3 myDeciredScale;
    float myScaleTimer = 0.0f;
    bool myHasScaled = false;

    private void Start()
    {

        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        myPlayerMovement = myPlayer.GetComponent<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody>();
        myBehaviour = GetComponent<Behaviour>();

        myDeciredScale = transform.localScale;

        List<Material> materials = new List<Material>();
        var renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.GetMaterials(materials);
            if (materials.Count != 0)
                myOutlineMaterial = materials.Last();
        }
    }
    public IEnumerator MoveTowardPoint(Vector3 point)
    {
        float maxTime = Time.time + 10;
        while ((point - transform.position).sqrMagnitude > 1.0f && maxTime > Time.time)
        {
            transform.Translate(Time.deltaTime * (point - transform.position).normalized * 10.0f);
            yield return null;
        }
        NestCreator.myNestCreator.Increment(gameObject, myNestCapacity);
    }

    public void SetOutlineActive()
    {
        if (myOutlineMaterial != null && enabled)
            myOutlineMaterial.SetFloat("_Active", 1.0f);
    }
    public void SetOutlineInactive()
    {
        if (myOutlineMaterial != null)
            myOutlineMaterial.SetFloat("_Active", 0.0f);
    }

    public void OnDisable()
    {
        SetOutlineInactive();
    }
    private void Update()
    {
        if (myIsDragging)
        {
            haveBeenPicked = true;
            SetOutlineInactive();
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
            if (haveBeenPicked)
            {
                Ray ray = new Ray(transform.position, Vector3.forward * 100.0f);
                var arg = Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Nest"));
                if (arg)
                {
                    SetOutlineInactive();

                    myRigidbody.useGravity = false;
                    myIsDragging = false;
                    myRigidbody.isKinematic = true;
                    var components = gameObject.GetComponents(typeof(UnityEngine.Component));
                    foreach (Component comp in components)
                    {
                        if (comp is not Transform && comp is not MeshFilter && comp is not MeshRenderer &&
                            comp is not Pickup)
                        {
                            Destroy(comp);
                        }
                    }

                    enabled = false;
                    StartCoroutine(MoveTowardPoint(hit.point));
                    return;
                }
            }

            var position = transform.position;
            position.z = GameManager.Instance.GetZFromY(transform.position.y);
            transform.position = Vector3.Lerp(transform.position, position, 3.5f * Time.deltaTime);

            // if (Vector3.Distance(myPlayer.position, position) < 2.0f &&
            //     myPlayer.GetComponent<PlayerLevel>().GetCurrentLevel() >= GetLevelRequired())
            //{
            //     SetOutlineActive();
            // }
            // else
            //{
            //     SetOutlineInactive();
            // }
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

        return aValue == 0.0f ? 0.0f
               : aValue == 1.0f ? 1.0f
                                : Mathf.Pow(2.0f, -10.0f * aValue) * Mathf.Sin((aValue * 10.0f - 0.75f) * c4) + 1.0f;
    }
}
