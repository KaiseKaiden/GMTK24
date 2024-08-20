using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform myPlayer;
    private PlayerLevel myPlayerLevel;
    public Transform myNest;
    Transform myOtherTarget;

    Vector3 myPosition;
    float myShakeIntencity;

    [SerializeField] private float myCameraSmooting;
    [SerializeField] private float myDeciredZDistance = 10.0f;
    [SerializeField] private float myXPositionLimit = 29.0f;
    [SerializeField] private float myYPositionLimit = 150.0f;


    private void Start()
    {
        myPosition = transform.position;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        myPlayer = player.transform;
        myPlayerLevel = player.GetComponent<PlayerLevel>();
    }

    public void SetNest(Transform aNestTransform)
    {
        myNest = aNestTransform;
    }

    private void Update()
    {
        Vector3 myDeciredPosition = myPlayer.position;
        if (GameManager.Instance.GetIsGameOver())
        {
            if (myOtherTarget != null)
            {
                myDeciredPosition = myOtherTarget.position;
            }
            else
            {
                myDeciredPosition = transform.position;
            }
        }

        myDeciredPosition.z = GameManager.Instance.GetZFromY(myPosition.y) - (GameManager.Instance.GetCamDistanceFromY(myDeciredPosition.y) + myDeciredZDistance + myPlayerLevel.GetCurrentLevel());

        float widthLimit = GameManager.Instance.GetXLimitFromY(myDeciredPosition.y);
        myDeciredPosition.x = Mathf.Clamp(myDeciredPosition.x, -widthLimit, widthLimit);
        myDeciredPosition.y = Mathf.Clamp(myDeciredPosition.y, 1.0f, myYPositionLimit);

        myPosition = Vector3.Lerp(myPosition, myDeciredPosition, myCameraSmooting * Time.deltaTime);

        transform.position = myPosition + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized * Random.Range(-myShakeIntencity, myShakeIntencity));
        myShakeIntencity = Mathf.Lerp(myShakeIntencity, 0.0f, Time.deltaTime);

        // Change Skybox Material
        RenderSettings.skybox.SetFloat("_TestHeight", transform.position.y);
    }

    public void SetShakeIntencity(float aValue)
    {
        myShakeIntencity = aValue;
    }

    public void SetTargetOtherTransform(Transform aTransform)
    {
        myOtherTarget = aTransform;
    }
}