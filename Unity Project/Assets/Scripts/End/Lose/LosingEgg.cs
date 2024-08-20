using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingEgg : MonoBehaviour
{
    [SerializeField] GameObject myCrackedEggPrefab;
    [SerializeField] GameObject myEggCrackParticlePrefab;
    [SerializeField] GameObject myTransition;

    private void Start()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f, 0.0f, -5.0f));
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.z = GameManager.Instance.GetZFromY(position.y);

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        Instantiate(myEggCrackParticlePrefab, collision.GetContact(0).point, Quaternion.identity);
        Instantiate(myTransition, Vector3.zero, Quaternion.identity);

        Camera.main.GetComponent<CameraMovement>().SetTargetOtherTransform(Instantiate(myCrackedEggPrefab, collision.GetContact(0).point, Quaternion.identity).transform);

        // Call GameOver State
    }
}
