using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] float mySpinnSpeed;
    bool myHasDestroyedEarth = false;

    [SerializeField] Material myEffectMaterial;
    [SerializeField] GameObject myEffectObject;
    float myIntensity = 0.0f;

    CameraMovement myCameraMovement;

    [SerializeField] GameObject myFadeCanvas;

    void Start()
    {
        myCameraMovement = Camera.main.GetComponent<CameraMovement>();

        myEffectMaterial.SetFloat("_Intensity", 0.0f);
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(0.0f, mySpinnSpeed, 0.0f) * Time.deltaTime;

        if (transform.position.y < 200.0f)
        {
            myCameraMovement.SetShakeIntencity(1.5f);

            myIntensity = Mathf.Lerp(myIntensity, 1.0f, Time.deltaTime * 2.2f);
            myEffectMaterial.SetFloat("_Intensity", myIntensity);

            AudioManager.instance.SetWorldParameter("music.on_off", 0.0f);

            myEffectObject.SetActive(true);
        }

        if (transform.position.y < 40.0f && !myHasDestroyedEarth)
        {
            Instantiate(myFadeCanvas);
            AudioManager.instance.PlayOneshotNoLocation(FMODEvents.instance.WorldEndEvent);
            myHasDestroyedEarth = true;
        }
    }
}
