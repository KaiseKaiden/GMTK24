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
    [field: SerializeField] public EventReference SpawnEggEvent { get; private set; }
    [field: SerializeField] public EventReference MoonCrashEvent { get; private set; }
    [field: SerializeField] public EventReference WorldEndEvent { get; private set; }


    [field:Header("UI")]
    [field: SerializeField] public EventReference CreditButtonEvent { get; private set; }
    [field: SerializeField] public EventReference LeaderBoardButtonEvent { get; private set; }
    [field: SerializeField] public EventReference QuitButtonEvent { get; private set; }
    [field: SerializeField] public EventReference SettingsButtonEvent { get; private set; }
    [field: SerializeField] public EventReference StartMainButtonEvent { get; private set; }

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
