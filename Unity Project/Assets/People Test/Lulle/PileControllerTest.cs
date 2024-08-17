using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PileControllerTest : MonoBehaviour
{
    private GameObject parentController;
    public GameObject[] listOfPileAssets;

    public int maxTier = 25;
    public float bredthMdf = 10;
    public float obPerTier = 10;
    public float aspectRatio = 1 / 10;
    public int startTier = 0;

    [SerializeField]
    private int obCount = 0;

    private GameObject GetRandomAsset()
    {
        return listOfPileAssets[Random.Range(0, listOfPileAssets.Length)];
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

    public void BuildObject()
    {
        obCount = 0;

        if (parentController != null)
        {
            DestroyImmediate(parentController);
        }

        parentController = Instantiate(GetRandomAsset()) as GameObject;

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
                    obCount++;
                }
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
