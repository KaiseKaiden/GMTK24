using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference BirdWingFlapEvent { get; private set; }
    [field: SerializeField] public EventReference MusicEvent { get; private set; }
    [field: SerializeField] public EventReference AmbienceEvent { get; private set; }
    [field: SerializeField] public EventReference NestGrowEvent { get; private set; }
    [field: SerializeField] public EventReference GetNestMaterialEvent { get; private set; }
    [field: SerializeField] public EventReference GetFoodEvent { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one FMODEvents found");
        }
        instance = this;
    }
}
