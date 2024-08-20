using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> myEventInstances;

    private EventInstance musicEventInstance;
    private EventInstance ambienceEventInstance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("more than one Audiomanager found");
        }
        instance = this;

        myEventInstances = new List<EventInstance>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitLoopAudio();
    }

    private void Update()
    {
        float audioParameterValue = (transform.position.y / 300.0f) * 10.0f; //TODO change to corrently adjust so the adui is right at the right height
        SetWorldParameter("air_level", audioParameterValue);
    }

    public void PlayOneshot(EventReference aEventref, Vector3 worldPos)
    {
        if (aEventref.IsNull)
            return;

        RuntimeManager.PlayOneShot(aEventref,worldPos);
    }

    public void PlayOneshotNoLocation(EventReference aEventref)
    {
        if (aEventref.IsNull)
            return;

        RuntimeManager.PlayOneShot(aEventref, transform.position);
    }


    public EventInstance CreateEventInstance(EventReference aEventref)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(aEventref);
        myEventInstances.Add(eventInstance);

        return eventInstance;
    }

    public void SetWorldParameter(string aParameterName, float aParameterValue)
    {
        RuntimeManager.StudioSystem.setParameterByName(aParameterName, aParameterValue);
    }

    private void InitLoopAudio()
    {
        musicEventInstance = CreateEventInstance(FMODEvents.instance.MusicEvent);
        musicEventInstance.start();

        ambienceEventInstance = CreateEventInstance(FMODEvents.instance.AmbienceEvent);
        ambienceEventInstance.start();
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in myEventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
