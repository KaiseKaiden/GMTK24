using System.Collections.Generic;
using UnityEngine;

public class NestMaterialSpawner : MonoBehaviour
{
    float mySky1Level = 0f;
    float mySky2Level = 0f;
    float mySpaceLevel = 0f;

    [SerializeField] float mySpawningInterval = 3f;
    [SerializeField] float myMinSpawningDistance = 10f;
    [SerializeField] float myMaxSpawningDistance = 30f;
    float mySpawningTimer = 0f;


    [Header("Ground Objects")]
    public List<GameObject> mySmallGroundLevelPrefabs;
    public List<GameObject> myMediumGroundLevelPrefabs;
    public List<GameObject> myLargeGroundLevelPrefabs;

    [Header("Sky1 Objects")]
    public List<GameObject> mySmallSky1LevelPrefabs;
    public List<GameObject> myMediumSky1LevelPrefabs;
    public List<GameObject> myLargeSky1LevelPrefabs;

    [Header("Sky2 Objects")]
    public List<GameObject> mySmallSky2LevelPrefabs;
    public List<GameObject> myMediumSky2LevelPrefabs;
    public List<GameObject> myLargeSky2LevelPrefabs;

    [Header("Space Objects")]
    public List<GameObject> mySmallSpaceLevelPrefabs;
    public List<GameObject> myMediumSpaceLevelPrefabs;
    public List<GameObject> myLargeSpaceLevelPrefabs;

    public Transform myPlayerTransform;
    public Material mySkyBoxMaterial; 

    private void Awake()
    {
        mySky1Level = mySkyBoxMaterial.GetFloat("_HigherupSkyPosition");
        mySky2Level = mySkyBoxMaterial.GetFloat("_SpacePosition");
        mySpaceLevel = mySkyBoxMaterial.GetFloat("_DeepSpacePosition");
    }

    private void Update()
    {
        mySpawningTimer += Time.deltaTime;

        if (mySpawningTimer >= mySpawningInterval)
        {
            if (myPlayerTransform.position.y <= mySky1Level)
            {
                SpawnGroundLevelMaterials();

            }
            else if (myPlayerTransform.position.y <= mySky2Level)
            {
                SpawnSkyLevel1Materials();
                Debug.Log("Sky1 Level");
            }
            else if (myPlayerTransform.position.y <= mySpaceLevel)
            {
                SpawnSkyLevel2Materials();
                Debug.Log("Sky2 Level");
            }
            else if (myPlayerTransform.position.y > mySpaceLevel)
            {
                SpawnSpaceLevelMaterials();
                Debug.Log("Space Level");
            }

            mySpawningTimer = 0;
        }
    }

    void SpawnGroundLevelMaterials()
    {
        //Does nothing rn
    }

    void SpawnSkyLevel1Materials()
    {
        Vector3 worldRndPos = GetRndPosOutsideCamera();

        int random = Random.Range(0, 3);
        int randomObj = Random.Range(0, 5);

        if (random == 0) //Smol
        {
            Instantiate(mySmallSky1LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Small Sky1 Level");
        }
        else if(random == 1) //Normal
        {
            Instantiate(myMediumSky1LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Medium Sky1 Level");
        }
        else if (random == 2) //Big
        {
            Instantiate(myLargeSky1LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Large Sky1 Level");
        }
    }

    void SpawnSkyLevel2Materials()
    {
        Vector3 worldRndPos = GetRndPosOutsideCamera();

        int random = Random.Range(0, 3);
        int randomObj = Random.Range(0, 5);

        if (random == 0) //Smol
        {
            Instantiate(mySmallSky2LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Small Sky2 Level");
        }
        else if (random == 1) //Normal
        {
            Instantiate(myMediumSky2LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Medium Sky2 Level");
        }
        else if (random == 2) //Big
        {
            Instantiate(myLargeSky2LevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Large Sky2 Level");
        }
    }

    void SpawnSpaceLevelMaterials()
    {
        Vector3 worldRndPos = GetRndPosOutsideCamera();

        int random = Random.Range(0, 3);
        int randomObj = Random.Range(0, 5);

        if (random == 0) //Smol
        {
            Instantiate(mySmallSpaceLevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Small Space Level");
        }
        else if (random == 1) //Normal
        {
            Instantiate(myMediumSpaceLevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Medium Space Level");
        }
        else if (random == 2) //Big
        {
            Instantiate(myLargeSpaceLevelPrefabs[randomObj], worldRndPos, Quaternion.identity);
            Debug.Log("Large Space Level");
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
