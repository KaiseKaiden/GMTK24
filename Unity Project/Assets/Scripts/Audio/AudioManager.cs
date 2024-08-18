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
    }

    private void Start()
    {
        musicEventInstance = CreateEventInstance(FMODEvents.instance.MusicEvent);
        musicEventInstance.start();

        ambienceEventInstance = CreateEventInstance(FMODEvents.instance.AmbienceEvent);
        ambienceEventInstance.start();

    }

    public void PlayOneshot(EventReference aEventref, Vector3 worldPos)
    {
        if (aEventref.IsNull)
            return;

        RuntimeManager.PlayOneShot(aEventref,worldPos);
    }

    public EventInstance CreateEventInstance(EventReference aEventref)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(aEventref);
        myEventInstances.Add(eventInstance);

        return eventInstance;
    }

    void CleanUp()
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
