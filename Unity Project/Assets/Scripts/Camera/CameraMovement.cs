using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform myPlayer;
    private PlayerLevel myPlayerLevel;
    public Transform myNest;

    [SerializeField] private float myCameraSmooting;
    [SerializeField] private float myDeciredZDistance = 10.0f;
    [SerializeField] private float myXPositionLimit = 29.0f;
    [SerializeField] private float myYPositionLimit = 150.0f;

    private void Start()
    {
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
        Vector3 myDirection = myPlayer.position - myNest.position;
        Vector3 myDeciredPosition = myNest.position + myDirection;
        myDeciredPosition.z = GameManager.Instance.GetZFromY(transform.position.y) - (myDeciredZDistance + myPlayerLevel.GetCurrentLevel());

        myDeciredPosition.x = Mathf.Clamp(myDeciredPosition.x, -myXPositionLimit, myXPositionLimit);
        myDeciredPosition.y = Mathf.Clamp(myDeciredPosition.y, 0.0f, myYPositionLimit);

        transform.position = Vector3.Lerp(transform.position, myDeciredPosition, myCameraSmooting * Time.deltaTime);
    }
}