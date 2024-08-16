using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObj
{
    [Range(0,1)]
    public float probability = 0.1f;
    
    public GameObject Prefab;
}
