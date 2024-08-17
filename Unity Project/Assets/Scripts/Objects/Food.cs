using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float myFillAmount = 1.0f;

    Transform myPlayer;
    PlayerLevel myPlayerLevel;

    [SerializeField] float myPickupDistance = 2.0f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        myPlayer = player.transform;
        myPlayerLevel = player.GetComponent<PlayerLevel>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Vector3.Distance(transform.position, myPlayer.position) < myPickupDistance)
        {
            myPlayerLevel.AddXp(myFillAmount);
            Destroy(gameObject);
        }

        var position = transform.position;
        position.z = GameManager.Instance.GetZFromY(transform.position.y);
        transform.position = position;
    }
}