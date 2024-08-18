using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField]
    LayerMask myPickupLayer;

    [SerializeField]
    float myPickupRadius;

    Pickup myHeldPickup;

    PlayerMovement myPlayerMovement;
    PlayerLevel myPlayerLevel;

    void Start()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerLevel = GetComponent<PlayerLevel>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            float closestDistance = Mathf.Infinity;
            Food food = null;
            bool isFood = false;
            bool foundSomething = false;

            Collider[] colliders = Physics.OverlapSphere(transform.position, myPickupRadius, myPickupLayer);
            foreach (Collider c in colliders)
            {
                float distance = (c.transform.position - transform.position).magnitude;
                Pickup pickup = c.GetComponent<Pickup>();

                if (pickup != null && myPlayerLevel.GetCurrentLevel() >= pickup.GetLevelRequired())
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        myHeldPickup = pickup;

                        isFood = false;
                        foundSomething = true;
                    }
                }
                else
                {
                    if (distance < closestDistance && c.tag == "Food")
                    {
                        food = c.GetComponent<Food>();

                        isFood = true;
                        foundSomething = true;
                    }
                }
            }

            if (foundSomething)
            {
                if (isFood)
                {
                    food.Eat();
                }
                else
                {
                    myHeldPickup.Pick();

                    myHeldPickup.transform.SetParent(myPlayerMovement.GetRightLeg());
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (myHeldPickup != null)
            {
                myHeldPickup.Drop();

                Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(myHeldPickup.transform.position));

                var arg = Physics.RaycastAll(ray, 1000.0f);
                foreach (var hit in arg)
                {
                    if (hit.transform.CompareTag("NestDropPoint"))
                    {
                        Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red, 100.0f, true);
                        myHeldPickup.StartCoroutine(myHeldPickup.MoveTowardPoint(hit.point));
                        break;
                    }
                }

                myHeldPickup.transform.SetParent(null);
                myHeldPickup = null;
            }
        }
    }
}
