using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeezerSpawner : MonoBehaviour
{
    public GameObject myGeezerObject;

    float myWaitTimer = 0f;
    float myTimeBetweenGeezers = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Geezer>() == null)
        {
            myWaitTimer += Time.deltaTime;
        }

        if (myWaitTimer >= myTimeBetweenGeezers)
        {
            Vector3 pos = Camera.main.transform.position;

            Vector2 dir = Vector3.zero;
            float rotation = 0;
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                pos.x += 10f;
                dir.x = -1;
                rotation = -90;
            }
            else if (rand == 1)
            {
                pos.x -= 10f;
                dir.x = 1;
                rotation = 90;
            }

            pos.y = 0f;
            pos.z = -1f;

            GameObject geezer = Instantiate(myGeezerObject, pos, Quaternion.identity);

            if (geezer.GetComponent<Geezer>() != null)
            {
                geezer.GetComponent<Geezer>().SetVelocity(dir);
            }

            geezer.transform.localScale = Vector3.one * 2;
            geezer.transform.rotation = Quaternion.Euler(0, rotation, 0);

            myWaitTimer = 0f;
        }   
    }
}
