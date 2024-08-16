using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nest : MonoBehaviour
{
    [SerializeField] private int myEggCapacity = 2;
    private int myEggCount = 0;
    private int myPyramidCount = 1;
    [SerializeField] private float myEggInterval = 15f;
    private float myEggIntervalMultiplier = 1f;
    private float myEggTimer = 0f;
    [SerializeField] private float myEggDistance = 1f;
    [SerializeField] private float myPyramidDistance = 15f;
    private Vector3 myNestCentre;

    public GameObject myEggPrefab;

    private Vector3[] myOffsets =
    {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(-2, 0, 0), new Vector3(2, 0, 0),
        new Vector3(-0.5f, 1, 0), new Vector3(0.5f, 1, 0), new Vector3(-1.5f, 1, 0), new Vector3(1.5f, 1, 0),
        new Vector3(0, 2, 0), new Vector3(-1, 2, 0), new Vector3(1, 2, 0),
        new Vector3(-0.5f, 3, 0), new Vector3(0.5f, 3, 0),
        new Vector3(0, 4, 0)
    };

    private void Start()
    {
        myNestCentre = transform.position;
    }

    void Update()
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
    }

    void SpawnEgg()
    {
        Debug.Log("Spawned an egg");
        Vector3 nextEggPos = GetEggPosition(myEggCount, myNestCentre);
        Instantiate(myEggPrefab, nextEggPos, Quaternion.identity);
    }

    bool IsObjectOutsideCameraView()
    {
        Camera mainCamera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Renderer renderer = GetComponent<Renderer>();

        return !GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public Vector3 GetEggPosition(int anEggCount, Vector3 aCenterPos)
    {
        if (anEggCount >= 15)
        {
            anEggCount %= 15;

            if (anEggCount == 0)
            {
                myPyramidCount++;
                myNestCentre = GetPyramidPosition(myPyramidCount, aCenterPos);
                aCenterPos = myNestCentre;
            }
        }

        Vector3 eggPosition = new Vector3();

        Vector3 eggOffset = myOffsets[anEggCount];

        eggPosition.x = aCenterPos.x + (eggOffset.x * myEggDistance);
        eggPosition.y = aCenterPos.y + (eggOffset.y * myEggDistance);

        return eggPosition;
    }

    public Vector3 GetPyramidPosition(int aPyramidCount, Vector3 aCenterPos)
    {
        Vector3 eggPosition = new Vector3();

        Vector3 eggOffset = myOffsets[aPyramidCount];

        eggPosition.x = aCenterPos.x + (eggOffset.x * myPyramidDistance);
        eggPosition.y = aCenterPos.y + (eggOffset.y * myPyramidDistance);

        return eggPosition;
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
}
