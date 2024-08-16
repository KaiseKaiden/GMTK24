using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    void Update()
    {
        if (IsObjectOutsideCameraView())
        {
            Debug.Log("GAME OVER");
        }
    }

    bool IsObjectOutsideCameraView()
    {
        Camera mainCamera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Renderer renderer = GetComponent<Renderer>();

        return !GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
