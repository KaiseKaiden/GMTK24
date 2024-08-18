using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class NestCreator : MonoBehaviour
{
    public GameObject nestPrefab;
    public GameObject centerMesh;
    public GameObject[] listOfPileAssets;
    public GameObject[] listOfStickAssets;

    public int maxTier = 25;
    public float bredthMdf = 10;
    public float obPerTier = 10;
    public float aspectRatio = 1 / 10;
    public int startTier = 0;
    public float tierHeight = .5f;

    public float globalScale = 1.0f;
    public float centerScale = 1.0f;
    public float objectScale = 1.0f;
    public float woodScale = 1.0f;

    private int previousEggCount = 0;
    private Nest nestBase;
    private GameObject parentController;

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
        var eggCount = 0;
        Nest nest = null;
        if (nestBase != null && nestBase.TryGetComponent(out nest))
        {
            eggCount = nest.GetEggCount();
        }

        if (parentController != null)
        {
            DestroyImmediate(parentController);
        }

        parentController = new GameObject("Empty");
        ;

        if (listOfPileAssets.Length > 0)
        {
            for (float tier = startTier; tier < maxTier; tier += tierHeight)
            {
                GameObject central = Instantiate(centerMesh, parentController.transform);
                central.name = "Tier " + tier;
                central.transform.position = new Vector3(0, tier, 0);
                central.transform.localScale *= tier * centerScale * globalScale;
                central.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                var bredth = Mathf.Log(tier * obPerTier);
                for (int i = 0; i < bredth; i++)
                {
                    var v2 = Random.insideUnitCircle * tier * bredthMdf;
                    v2.y *= aspectRatio;
                    var obj = Instantiate(GetRandomAsset(), central.transform);
                    obj.transform.position = new Vector3(v2.x, tier, v2.y);
                    obj.transform.rotation = Random.rotation;
                    obj.transform.localScale *= objectScale;
                    obCount++;
                }

                for (int i = 0; i < bredth * 5; i++)
                {
                    float angle = Random.Range(0, 360);
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                    z *= aspectRatio;

                    var obj = Instantiate(GetRandomStickAsset(), central.transform);
                    obj.transform.SetPositionAndRotation(
                        new Vector3(x, Random.Range(tier - 1.0f, tier), z),
                        Quaternion.Euler(Random.Range(-15.0f, 15.0f), -1 * angle, Random.Range(-15.0f, 15.0f)));

                    var scale = obj.transform.localScale;
                    scale.x *= Random.Range(1.0f, 10.0f);
                    scale.y *= Random.Range(1.0f, 10.0f);
                    obj.transform.localScale = scale * woodScale;
                    obCount++;
                }
            }

            GameObject nestObj = Instantiate(nestPrefab, parentController.transform);
            nestObj.transform.position = Vector3.up * (maxTier);
            nestObj.GetComponent<Nest>().myEggCapacity = maxTier * 2;
            nestObj.GetComponent<Nest>().SetCurrentEggCount(eggCount);
            nestBase = nestObj.GetComponent<Nest>();
        }
    }

    [CustomEditor(typeof(NestCreator))]
    public class PileControllerTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NestCreator myScript = (NestCreator)target;
            if (GUILayout.Button("Build Object"))
            {
                EditorApplication.update += myScript.DelayedBuildObject;
            }
        }
    }
}
