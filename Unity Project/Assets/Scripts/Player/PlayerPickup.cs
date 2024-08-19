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

            Collider[] colliders = Physics.OverlapSphere(transform.position, myPickupRadius * transform.localScale.x, myPickupLayer);
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

                // if (distance < closestDistance)
                //{
                //     if (c.tag == "Pickup" && myPlayerLevel.GetCurrentLevel() >= pickup.GetLevelRequired())
                //     {
                //         closestDistance = distance;
                //         myHeldPickup = pickup;

                //        isFood = false;
                //    }
                //    else
                //    {
                //        food = c.GetComponent<Food>();

                //        isFood = true;
                //    }

                //    foundSomething = true;
                //}
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

                    if (myHeldPickup.gameObject.tag == "Moon")
                    {
                        myHeldPickup.GetComponent<SphereCollider>().enabled = false;
                        myPlayerMovement.DeactivateMovement();
                        myPlayerLevel.enabled = false;
                        this.enabled = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (myHeldPickup != null)
            {
                myHeldPickup.Drop();

                myHeldPickup.transform.SetParent(null);
                myHeldPickup = null;
            }
        }
    }
}
