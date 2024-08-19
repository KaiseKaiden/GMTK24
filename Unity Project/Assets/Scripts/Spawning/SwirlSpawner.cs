using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlSpawner : MonoBehaviour
{
    public GameObject myWindSwirlObject; 
    public GameObject myWindSwirlObject2;

    float myWindSwirlTimer = 0;
    float myWindSwirlInterval = 2;

    float myMaxSpawnHeight = 0;
    public Material mySkyMaterial;

    private void Start()
    {
        myMaxSpawnHeight = mySkyMaterial.GetFloat("_DeepSpacePosition");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y < myMaxSpawnHeight)
        {
            myWindSwirlTimer += Time.deltaTime;

            if (myWindSwirlTimer > myWindSwirlInterval)
            {
                int rand = Random.Range(0, 2);

                Vector3 pos = new Vector3(Random.Range(-60, 60), Camera.main.transform.position.y, -1);

                if (rand == 0)
                {
                    Instantiate(myWindSwirlObject, pos, Quaternion.identity);
                }
                else if (rand == 1)
                {
                    Instantiate(myWindSwirlObject2, pos, Quaternion.identity);
                }

                myWindSwirlTimer = 0;
            }
        }
    }
}
