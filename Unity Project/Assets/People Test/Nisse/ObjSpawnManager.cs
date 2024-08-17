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
        //for (int i = 0; i < mySpawnAmount; i++)
        //{
        //    TrySpawnMaterial();
        //}
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

        for (int j = 0; j < myMaterialObjects.Count; j++)
        {
            if (Random.Range(0.0f, 1.0f) <= myMaterialObjects[j].probability)
            {
                Vector3 worldRndPos = GetRndPosOutsideCamera();
                Instantiate(myMaterialObjects[j].Prefab, worldRndPos, Quaternion.identity);
                myCurrentSpawnedObjectCount++;
                return;
            }
        }
    }

    void TrySpawnFood()
    {
        if (myCurrentSpawnedObjectCount > mymaxSpawnedObjects)
            return;

        for (int j = 0; j < myFoodObjects.Count; j++)
        {
            if (Random.Range(0.0f, 1.0f) <= myFoodObjects[j].probability)
            {
                Vector3 worldRndPos = GetRndPosOutsideCamera();
                Instantiate(myFoodObjects[j].Prefab, worldRndPos, Quaternion.identity);
                myCurrentSpawnedObjectCount++;
                return;
            }
        }
    }

    Vector3 GetRndPosOutsideCamera()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        if (x >= 0) x += 1;
        if (y >= 0) y += 1;
        Vector3 Rndpos = new Vector3(x, y, Camera.main.nearClipPlane + 11);

        Vector3 worldRndPos = Camera.main.ViewportToWorldPoint(Rndpos);
        //worldRndPos *= mySpawnArea;
        return worldRndPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mySpawnArea);
    }
}
