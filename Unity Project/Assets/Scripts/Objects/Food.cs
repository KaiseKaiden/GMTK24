using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float myFillAmount = 1.0f;
    [SerializeField] GameObject myBreadCrumbPart;

    PlayerLevel myPlayerLevel;

    private void Start()
    {
        myPlayerLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
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
}
