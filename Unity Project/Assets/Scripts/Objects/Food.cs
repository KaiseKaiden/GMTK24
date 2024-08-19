using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float myFillAmount = 1.0f;
    [SerializeField] GameObject myBreadCrumbPart;
    [SerializeField] GameObject myFoodBalloonPrefab;

    PlayerLevel myPlayerLevel;

    Rigidbody myRigidbody;

    private void Start()
    {
        myPlayerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
        myRigidbody = GetComponent<Rigidbody>();

        SetWorldState();
    }

    private void Update()
    {
        var position = transform.position;
        position.z = GameManager.Instance.GetZFromY(transform.position.y);
        transform.position = position;
    }

    public void Eat()
    {
        myPlayerLevel.AddXp(myFillAmount);
        Destroy(gameObject);

        Instantiate(myBreadCrumbPart, transform.position, Quaternion.identity);
        AudioManager.instance.PlayOneshot(FMODEvents.instance.GetFoodEvent, transform.position);
    }

    void SetWorldState()
    {
        if (transform.position.y < 45.0f)
        {
            // Ground
            myRigidbody.useGravity = true;
        }
        else if (transform.position.y < 65.0f)
        {
            // First & Second Sky
            myRigidbody.useGravity = false;
            myRigidbody.isKinematic = true;

            // Create Balloon
            GameObject balloon = Instantiate(myFoodBalloonPrefab, transform.position, Quaternion.identity);
            balloon.transform.SetParent(transform);
        }
        else
        {
            // Space
            myRigidbody.useGravity = false;
            myRigidbody.AddTorque(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)));
        }
    }
}
