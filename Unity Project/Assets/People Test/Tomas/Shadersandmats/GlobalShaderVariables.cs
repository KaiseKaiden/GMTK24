using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShaderVariables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Shader.SetGlobalFloat("_AFloat", 1.0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
