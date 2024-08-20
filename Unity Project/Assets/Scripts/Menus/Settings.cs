using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public void SetSoundLevel(GameObject aSoundSlider)
    {
        float value = aSoundSlider.GetComponent<Slider>().value * 100f;
        AudioManager.instance.SetVolume(value);
    }

    public void SetMusicLevel(GameObject aMusicSlider)
    {
        float value = aMusicSlider.GetComponent<Slider>().value * 100f;
        AudioManager.instance.SetMusicVolume(value);
    }

    public void SetFullscreen(GameObject aFullscreenToggle)
    {
        bool value = aFullscreenToggle.GetComponent<Toggle>().isOn;
        Screen.fullScreen = value;
    }
}
