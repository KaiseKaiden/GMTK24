using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawnManager : MonoBehaviour
{
    public float mySpawnInterval = 1.0f;
    public float mySpawnArea = 30.0f;
    public int mySpawnAmount = 10; //Debugging
    public int mymaxSpawnedObjects = 1000;
    public List<SpawnObj> myMaterialObjects;
    public List<SpawnObj> myFoodObjects;

    private float myCurrentSpawnTime = 0.0f;
    private float myCurrentSpawnedObjectCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mySpawnAmount; i++)
        {
            TrySpawnMaterial();
        }
    }

    // Update is called once per frame
    void Update()
    {
        myCurrentSpawnTime -= Time.deltaTime;

        if(myCurrentSpawnTime <= 0.0f)
        {
            myCurrentSpawnTime = mySpawnInterval;

            TrySpawnFood();
            TrySpawnMaterial();
        }
    }

    void TrySpawnMaterial()
    {
        if (myCurrentSpawnedObjectCount > mymaxSpawnedObjects)
            return;

        for (int i = 0; i < mySpawnAmount; i++)
        {
            for (int j = 0; j < myMaterialObjects.Count; j++)
            {
                if (Random.Range(0.0f, 1.0f) <= myMaterialObjects[j].probability)
                {
                    Vector3 rndPos = Random.insideUnitSphere * mySpawnArea;
                    rndPos.z = 0;
                    Instantiate(myMaterialObjects[j].Prefab, rndPos, Quaternion.identity);
                    myCurrentSpawnedObjectCount++;
                    return;
                }
            }
        }
    }

    void TrySpawnFood()
    {
        if (myCurrentSpawnedObjectCount > mymaxSpawnedObjects)
            return;

        for (int j = 0; j < myFoodObjects.Count; j++)
        {
            if (Random.Range(0.0f, 1.0f) <= myMaterialObjects[j].probability)
            {
                Vector3 rndPos = Random.insideUnitSphere * mySpawnArea;
                rndPos.z = 0;
                Instantiate(myMaterialObjects[j].Prefab, rndPos, Quaternion.identity);
                myCurrentSpawnedObjectCount++;
                return;
            }
        }
    }

}
