using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeeserFeed : MonoBehaviour
{
    [SerializeField] ParticleSystem myFoodParticle;

    public void Feed()
    {
        myFoodParticle.Play();
    }
}
