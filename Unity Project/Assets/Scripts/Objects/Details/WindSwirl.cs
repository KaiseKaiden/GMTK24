using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WindSwirl : MonoBehaviour
{
    float myTimeTillDeath = 10f;
    float myDeathTimer = 0f;

    void Update()
    {
        myDeathTimer += Time.deltaTime;

        if (myDeathTimer >= myTimeTillDeath)
        {
            Destroy(gameObject);
        }
    }
}
