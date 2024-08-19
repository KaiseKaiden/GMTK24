using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlSpawner : MonoBehaviour
{
    public GameObject myWindSwirlObject; 
    public GameObject myWindSwirlObject2;

    float myWindSwirlTimer = 0;
    float myWindSwirlInterval = 2;

    // Update is called once per frame
    void Update()
    {
        myWindSwirlTimer += Time.deltaTime;

        if (myWindSwirlTimer > myWindSwirlInterval)
        {
            int rand = Random.Range(0, 2);

            Vector3 pos = new Vector3(Random.Range(-27, 28), Camera.main.transform.position.y, -1);

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
