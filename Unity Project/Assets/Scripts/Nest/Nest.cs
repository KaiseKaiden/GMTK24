using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Nest : MonoBehaviour
{
    [SerializeField] private int myEggCapacity = 2;
    private int myEggCount = 0;
    [SerializeField] private float myEggInterval = 15f;
    private float myEggIntervalMultiplier = 1f;
    private float myEggTimer = 0f;
    private Transform myNestCentre;

    SpiralGenerator mySpiralGenerator = new SpiralGenerator();

    private List<Vector3> myEggPoints = new List<Vector3>();
    public GameObject myEggPrefab;
    private GameObject myEggHolder;

    public TextMeshProUGUI myEggCountText;

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
        myEggCountText.text = myEggCount.ToString() + "/" + myEggCapacity.ToString();
        myEggHolder = new GameObject("EggHolder");
        myEggHolder.transform.position = transform.position;
        myNestCentre = myEggHolder.transform;

        int numOfEggs = 50;
        float spiralParameter = 0.1f;
        float distBetweenEggs = 0.65f;
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = myEggHolder.transform.position;
            pos.y += 0.65f * i;

            myEggPoints.AddRange(mySpiralGenerator.GetSpiralPoints(pos, spiralParameter, distBetweenEggs, numOfEggs));

            spiralParameter -= 0.025f;
            distBetweenEggs -= 0.05f;
            numOfEggs = (int)(numOfEggs * 0.75f);
        }
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

        myEggCountText.text = myEggCount.ToString() + "/" + myEggCapacity.ToString();
    }

    void SpawnEgg()
    {
        if (myEggCount >= myEggPoints.Count) 
        {
            return;
        }

        Vector3 nextEggPos = GetEggPosition(myEggCount);

        GameObject egg = Instantiate(myEggPrefab, myEggHolder.transform);
        egg.transform.position = nextEggPos;
    }

    bool IsObjectOutsideCameraView()
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
