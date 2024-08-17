using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class PileControllerTest : MonoBehaviour
{
    private GameObject parentController;
    public GameObject nestPrefab;
    public GameObject[] listOfPileAssets;
    public GameObject[] listOfStickAssets;

    public int maxTier = 25;
    public float bredthMdf = 10;
    public float obPerTier = 10;
    public float aspectRatio = 1 / 10;
    public int startTier = 0;
    public float globalScale = 1.0f;

    private int previousEggCount = 0;
    private Nest nestBase;


    [SerializeField]
    private int obCount = 0;

    private GameObject GetRandomAsset()
    {
        return listOfPileAssets[Random.Range(0, listOfPileAssets.Length)];
    }
    private GameObject GetRandomStickAsset()
    {
        return listOfStickAssets[Random.Range(0, listOfStickAssets.Length)];
    }

    private void OnValidate()
    {
        EditorApplication.update += DelayedBuildObject;
    }

    private void DelayedBuildObject()
    {
        EditorApplication.update -= DelayedBuildObject;
        BuildObject();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            maxTier++;
            maxTier = Mathf.Clamp(maxTier, 1, 25);
            DelayedBuildObject();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            maxTier--;
            maxTier = Mathf.Clamp(maxTier, 1, 25);
            DelayedBuildObject();
        }
    }

    public void BuildObject()
    {
        obCount = 0;

        if (parentController != null)
        {
            DestroyImmediate(parentController);
        }

        parentController = new GameObject("Empty"); ;

        if (listOfPileAssets.Length > 0)
        {
            for (int tier = startTier; tier < maxTier; tier++)
            {
                var bredth = tier * obPerTier;
                for (int i = 0; i < bredth; i++)
                {
                    var v2 = Random.insideUnitCircle * tier * bredthMdf;
                    v2.y *= aspectRatio;
                    var obj = Instantiate(GetRandomAsset(), parentController.transform) as GameObject;
                    obj.transform.position = new Vector3(v2.x, tier, v2.y);
                    obj.transform.rotation = Random.rotation;
                    obj.transform.localScale *= 0.1f * tier * globalScale;
                    obCount++;
                }


                for (int i = 0; i < bredth * 5; i++)
                {
                    float angle = Random.Range(0, 360);
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                    z *= aspectRatio;




                    var obj = Instantiate(GetRandomStickAsset(), parentController.transform) as GameObject;
                    //obj.transform.position = new Vector3(x, tier, z);
                    obj.transform.SetPositionAndRotation(new Vector3(x, Random.Range(tier - 1.0f, tier), z), Quaternion.Euler(Random.Range(-15.0f, 15.0f), -1 * angle, Random.Range(-15.0f, 15.0f)));

                    var scale = obj.transform.localScale;
                    scale.x *= Random.Range(0.5f, 7.0f);
                    scale.y *= Random.Range(0.5f, 7.0f);
                    obj.transform.localScale = scale * globalScale;
                    //obj.transform.rotation = Random.rotation;
                    obCount++;
                }

            }

            GameObject nestObj = Instantiate(nestPrefab, parentController.transform);

            if (nestBase != null)
            {
                nestObj.transform.position = Vector3.up * (maxTier - 1);
                nestObj.GetComponent<Nest>().myEggCapacity = maxTier * 2;
                nestObj.GetComponent<Nest>().SetCurrentEggCount(nestBase.GetEggCount());
                nestBase = nestObj.GetComponent<Nest>();
            }
        }



    }


    [CustomEditor(typeof(PileControllerTest))]
    public class PileControllerTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PileControllerTest myScript = (PileControllerTest)target;
            if (GUILayout.Button("Build Object"))
            {
                EditorApplication.update += myScript.DelayedBuildObject;
            }
        }
    }
}
