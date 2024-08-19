using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private float mySky1Level = 0f;
    private float mySky2Level = 0f;
    private float mySpaceLevel = 0f;
    private Transform myPlayerTransform;

    private float mySpawningTimer = 0f;


    [SerializeField, Range(0.01f, 10.0f)] float mySpawningInterval = 3f;
    [SerializeField] float myMinSpawningDistance = 10f;
    [SerializeField] float myMaxSpawningDistance = 30f;

    public Material mySkyBoxMaterial;
    [Header("Food")]
    public List<GameObject> mySmallPrefabs;
    public List<GameObject> myMediumPrefabs;
    public List<GameObject> myLargePrefabs;


    private void Awake()
    {
        mySky1Level = mySkyBoxMaterial.GetFloat("_HigherupSkyPosition");
        mySky2Level = mySkyBoxMaterial.GetFloat("_SpacePosition");
        mySpaceLevel = mySkyBoxMaterial.GetFloat("_DeepSpacePosition");
    }

    private void Start()
    {
        myPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        mySpawningTimer += Time.deltaTime;

        if (mySpawningTimer >= mySpawningInterval)
        {
            if (myPlayerTransform.position.y <= mySky1Level)
            {
                SpawnFood(1);
            }
            else if (myPlayerTransform.position.y <= mySky2Level)
            {
                SpawnFood(2);
            }
            else if (myPlayerTransform.position.y <= mySpaceLevel)
            {
                SpawnFood(3);

            }
            else if (myPlayerTransform.position.y > mySpaceLevel)
            {
                SpawnFood(4);
            }
            mySpawningTimer = 0;
        }
    }

    void SpawnFood(float someNewScale)
    {
        Vector3 worldRndPos = GetRndPosOutsideCamera();
        int random = Random.Range(0, 3);

        if (random == 0) //Smol
        {
            int randomObj = Random.Range(0, mySmallPrefabs.Count);
            GameObject newObject = Instantiate(mySmallPrefabs[randomObj], worldRndPos, Quaternion.identity);
            newObject.transform.localScale = new Vector3(someNewScale, someNewScale, someNewScale);
        }
        else if (random == 1) //Normal
        {
            int randomObj = Random.Range(0, myMediumPrefabs.Count);
            GameObject newObject = Instantiate(myMediumPrefabs[randomObj], worldRndPos, Quaternion.identity);
            newObject.transform.localScale = new Vector3(someNewScale, someNewScale, someNewScale);
        }
        else if (random == 2) //Big
        {
            int randomObj = Random.Range(0, myLargePrefabs.Count);
            GameObject newObject = Instantiate(myLargePrefabs[randomObj], worldRndPos, Quaternion.identity);
            newObject.transform.localScale = new Vector3(someNewScale, someNewScale, someNewScale);
        }
    }

    Vector3 GetRndPosOutsideCamera()
    {
        float zPos = myPlayerTransform.position.z;
        Vector2 CirclePos = Random.insideUnitCircle.normalized * Random.Range(myMinSpawningDistance, myMaxSpawningDistance);
        Vector3 Rndpos = new Vector3(CirclePos.x, CirclePos.y, zPos) + myPlayerTransform.position;
        Vector3 worldRndPos = Camera.main.ViewportToWorldPoint(Rndpos);
        return Rndpos;
    }
}
