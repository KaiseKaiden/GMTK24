using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpinn : MonoBehaviour
{
    const float mySpeed = 500.0f;

    void Update()
    {
        transform.localEulerAngles += new Vector3(0.0f, 0.0f, mySpeed * Time.deltaTime);
    }
}
