using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nest : MonoBehaviour
{
    [SerializeField]
    public int myEggCapacity = 2;
    private int myEggCount = 0;
    [SerializeField]
    private float myEggInterval = 15f;
    private float myEggIntervalMultiplier = 1f;
    private float myEggTimer = 0f;
    private Transform myNestCentre;
    private SpiralGenerator mySpiralGenerator = new();
    public Transform myDoveTransform;

    private List<Vector3> myEggPoints = new();
    public GameObject myEggPrefab;
    public GameObject myDovePrefab;

    public TextMeshProUGUI myEggCountText;

    private Vector3[] myOffsets = { new(0, 0, 0),     new(-1, 0, 0),    new(1, 0, 0),    new(-2, 0, 0),
                                    new(2, 0, 0),     new(-0.5f, 1, 0), new(0.5f, 1, 0), new(-1.5f, 1, 0),
                                    new(1.5f, 1, 0),  new(0, 2, 0),     new(-1, 2, 0),   new(1, 2, 0),
                                    new(-0.5f, 3, 0), new(0.5f, 3, 0),  new(0, 4, 0) };

    private void ChangeText(string text)
    {
        if (myEggCountText != null)
            myEggCountText.text = text;
    }

    private void Start()
    {
        ChangeText(myEggCount.ToString() + "/" + myEggCapacity.ToString());
        int numOfEggs = 50;
        float spiralParameter = 0.1f;
        float distBetweenEggs = 0.65f;
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3();
            pos.y += 0.65f * i;

            myEggPoints.AddRange(mySpiralGenerator.GetSpiralPoints(pos, spiralParameter, distBetweenEggs, numOfEggs));

            spiralParameter -= 0.025f;
            distBetweenEggs -= 0.05f;
            numOfEggs = (int)(numOfEggs * 0.75f);
        }

        // if (Camera.main.GetComponent<CameraMovement>())
        //{
        //     Camera.main.GetComponent<CameraMovement>().SetNest(transform);
        // }
        var doveMom = Instantiate(myDovePrefab, transform);
        myDoveTransform = doveMom.transform;
        myDoveTransform.position = transform.position;
    }

    private void Update()
    {
        myEggTimer += Time.deltaTime;

        if (myEggTimer >= myEggInterval * myEggIntervalMultiplier)
        {
            SpawnEgg();

            myEggCount++;
            myEggTimer = 0f;

            if (myEggCount > myEggCapacity)
            {
                Debug.Log("You Lost");
            }
        }

        if (IsObjectOutsideCameraView())
        {
            Debug.Log("GAME OVER");
        }

        ChangeText(myEggCount.ToString() + "/" + myEggCapacity.ToString());
    }

    private void SpawnEgg()
    {
        if (myEggCount >= myEggPoints.Count)
        {
            return;
        }

        Vector3 nextEggPos = GetEggPosition(myEggCount);

        GameObject egg = Instantiate(myEggPrefab, transform);
        egg.transform.position = nextEggPos + transform.position;
        myDoveTransform.position = egg.transform.position;
        AudioManager.instance.PlayOneshot(FMODEvents.instance.SpawnEggEvent, transform.position);
    }

    private bool IsObjectOutsideCameraView()
    {
        Camera mainCamera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Renderer renderer = GetComponent<Renderer>();

        return !GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public Vector3 GetEggPosition(int anEggCount)
    {
        return myEggPoints[anEggCount];
    }

    public void SetCurrentEggCount(int countToReach)
    {
        while (myEggCount < countToReach)
        {
            SpawnEgg();
            myEggCount++;
            myEggTimer = 0f;
        }
    }
    public int GetEggCount()
    {
        return myEggCount;
    }

    public int GetEggCapacity()
    {
        return myEggCapacity;
    }

    public float GetNextEggProgress()
    {
        return myEggTimer / (myEggInterval * myEggIntervalMultiplier);
    }

    public void AddEggCapacity(int aCapacity)
    {
        myEggCapacity += aCapacity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            Pickup pickup = other.GetComponent<Pickup>();

            AddEggCapacity(pickup.GetCapacity());
            Destroy(other.gameObject);
        }
    }
}
