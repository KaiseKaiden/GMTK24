using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float myFillAmount = 1.0f;

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
    }
}
