using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    float myFillAmount = 1.0f;
    [SerializeField]
    GameObject myBreadCrumbPart;
    [SerializeField]
    GameObject myFoodBalloonPrefab;

    PlayerLevel myPlayerLevel;
    Rigidbody myRigidbody;

    Transform myPlayer;
    Material myOutlineMaterial;

    void SetOutlineActive()
    {
        if (myOutlineMaterial != null)
            myOutlineMaterial.SetFloat("_Active", 1.0f);
    }
    void SetOutlineInactive()
    {
        if (myOutlineMaterial != null)
            myOutlineMaterial.SetFloat("_Active", 0.0f);
    }

    Vector3 myDeciredScale;
    float myScaleTimer = 0.0f;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        myPlayerLevel = myPlayer.GetComponent<PlayerLevel>();
        myRigidbody = GetComponent<Rigidbody>();

        List<Material> materials = new List<Material>();
        var renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.GetMaterials(materials);
            if (materials.Count != 0)
                myOutlineMaterial = materials.Last();
        }

        SetWorldState();

        myDeciredScale = transform.localScale;
    }

    private void Update()
    {
        var position = transform.position;
        position.z = GameManager.Instance.GetZFromY(transform.position.y);
        transform.position = position;

        if (Vector3.Distance(myPlayer.position, position) < 2.0f)
        {
            SetOutlineActive();
        }
        else
        {
            SetOutlineInactive();
        }

        // Scale Up
        myScaleTimer += Time.deltaTime;
        transform.localScale = myDeciredScale * EaseOutElastic(Mathf.Clamp01(myScaleTimer));
    }

    public void Eat()
    {
        myPlayerLevel.AddXp(myFillAmount * transform.localScale.x);
        Destroy(gameObject);

        Instantiate(myBreadCrumbPart, transform.position, Quaternion.identity);
        AudioManager.instance.PlayOneshot(FMODEvents.instance.GetFoodEvent, transform.position);
    }

    void SetWorldState()
    {
        if (transform.position.y < 45.0f)
        {
            // Ground
            myRigidbody.useGravity = true;
        }
        else if (transform.position.y < 65.0f)
        {
            // First & Second Sky
            myRigidbody.useGravity = false;
            myRigidbody.isKinematic = true;

            // Create Balloon
            GameObject balloon = Instantiate(myFoodBalloonPrefab, transform.position, Quaternion.identity);
            balloon.transform.localScale = transform.localScale;
            balloon.transform.SetParent(transform);
            balloon.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }
        else
        {
            // Space
            myRigidbody.useGravity = false;
            myRigidbody.AddTorque(
                new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)));
        }
    }

    float EaseOutElastic(float aValue)
    {
        const float c4 = (2.0f * Mathf.PI) / 3.0f;

        return aValue == 0.0f ? 0.0f : aValue == 1.0f ? 1.0f : Mathf.Pow(2.0f, -10.0f * aValue) * Mathf.Sin((aValue * 10.0f - 0.75f) * c4) + 1.0f;
    }
}
