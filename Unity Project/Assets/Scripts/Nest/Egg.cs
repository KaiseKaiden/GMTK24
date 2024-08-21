using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    float myTime = 0.0f;

    void Update()
    {
        myTime += Time.deltaTime;
        transform.localScale = Vector3.one * EaseOutElastic(Mathf.Clamp01(myTime));
    }

    float EaseOutElastic(float aValue)
    {
        const float c4 = (2.0f * Mathf.PI) / 3.0f;

        return aValue == 0.0f ? 0.0f
               : aValue == 1.0f ? 1.0f
                                : Mathf.Pow(2.0f, -10.0f * aValue) * Mathf.Sin((aValue * 10.0f - 0.75f) * c4) + 1.0f;
    }
}
