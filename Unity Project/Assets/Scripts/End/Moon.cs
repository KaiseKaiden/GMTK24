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

        myIntensity = Mathf.Lerp(myIntensity, 1.0f, Time.deltaTime * 0.6f);
        myEffectMaterial.SetFloat("_Intensity", myIntensity);
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(0.0f, mySpinnSpeed, 0.0f) * Time.deltaTime;

        if (transform.position.y < 100.0f)
        {
            myCameraMovement.SetShakeIntencity(1.5f);

            myEffectMaterial.SetFloat("_Intensity", 1.0f);
            myEffectObject.SetActive(true);
        }

        if (transform.position.y < 40.0f && !myHasDestroyedEarth)
        {
            Instantiate(myFadeCanvas);
            myHasDestroyedEarth = true;
        }
    }
}
