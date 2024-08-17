using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public EventReference testReference;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("more than one Audiomanager found");
        }
        instance = this;
    }

    private void Start()
    {
        PlayOneshot(testReference, transform.position);
    }

    void PlayOneshot(EventReference aEventref, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(aEventref,worldPos);
    }
}
