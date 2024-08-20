using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

public class NestCreator : MonoBehaviour
{
    public static NestCreator myNestCreator;

    public GameObject nestPrefab;
    public GameObject centerMesh;
    public List<GameObject> listOfPileAssets;
    public List<GameObject> listOfStickAssets;

    public int maxTier = 25;
    public float bredthMdf = 10;
    public float obPerTier = 10;
    public float aspectRatio = 1;

    public int StartTier = 0;
    private int startTier
    {
        get {
            return Mathf.Clamp(StartTier, 0, 1000);
        }
    }

    public float TierHeight = 0.1f;
    public float tierHeight
    {
        get {
            return Mathf.Clamp(TierHeight, 0.1f, 1000.0f);
        }
    }

    public float globalScale = 1.0f;
    public float centerScale = 1.0f;
    public float objectScale = 1.0f;
    public float woodScale = 1.0f;

    public Transform spawnPosition;

    private int previousEggCount = 0;
    private Nest nestBase;
    private GameObject parentController;

    [SerializeField]
    private int obCount = 0;

    private GameObject GetRandomAsset()
    {
        var index = Random.Range(0, listOfPileAssets.Count);
        if (listOfPileAssets[index] != null)
        {
            return listOfPileAssets[index];
        }
        listOfPileAssets.RemoveAt(index);
        return GetRandomAsset();
    }
    private GameObject GetRandomStickAsset()
    {
        return listOfStickAssets[Random.Range(0, listOfStickAssets.Count)];
    }

    public void Awake()
    {
        myNestCreator = this;
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }
        DelayedBuildObject();
    }

    public void DelayedBuildObject()
    {
#if UNITY_EDITOR
        EditorApplication.delayCall += BuildObject;
#endif
    }

    public void Decrement()
    {
        maxTier--;
        maxTier = Mathf.Clamp(maxTier, 1, 1000);
        DelayedBuildObject();
    }

    public void Increment(GameObject mesh = null, float tierIncrement = 1.0f)
    {
        print("Increment");
        if (mesh != null)
        {
            listOfPileAssets.Add(mesh);
        }
        tierIncrement = Mathf.Abs(tierIncrement);
        maxTier += Mathf.FloorToInt(tierIncrement);
        maxTier = Mathf.Clamp(maxTier, 1, 1000);

        var obj = GameObject.FindGameObjectWithTag("EditorNestDestructionTag");
        if (obj != null)
        {
            IncrementObject(obj, tierIncrement);
        }
        else
        {
            DelayedBuildObject();
        }
    }

    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //     Increment();
        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //     Decrement();
        // }
    }

    private void IncrementObject(GameObject NestNodeParent, float steps = 1.0f)
    {
        AudioManager.instance.PlayOneshot(FMODEvents.instance.NestGrowEvent, transform.position);
        print("IncrementObject");
        Vector3 positionOffset = spawnPosition.position;
        // positionOffset.y += Mathf.Pow(maxTier, 0.8f) * 0.01f;

        var currentNodeCount = NestNodeParent.transform.childCount;
        Transform nestObj = NestNodeParent.transform.GetChild(currentNodeCount - 1);

        Nest nest;
        if (nestObj.TryGetComponent(out nest))
        {
            nest.myEggCapacity += (int)steps;
            nest.PlayParticleEffect();
        }
        for (float tier = maxTier - Mathf.FloorToInt(steps); Mathf.FloorToInt(tier) < maxTier + (int)steps;
             tier += tierHeight)
        {
            print("Added " + tier);
            GameObject central = Instantiate(centerMesh, NestNodeParent.transform);
            central.name = "Tier " + tier;
            central.transform.position = new Vector3(0, tier, 0) + positionOffset;
            central.transform.localScale *= tier * centerScale * globalScale;
            central.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            var bredth = Mathf.Log(tier * obPerTier);
            for (int i = 0; i < bredth; i++)
            {
                float angle = Random.Range(0, 360);
                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                float z = Mathf.Sin(angle * Mathf.Deg2Rad) * tier * bredthMdf;
                z *= aspectRatio;
                var obj = Instantiate(GetRandomAsset(), central.transform);

                var components = obj.GetComponents(typeof(UnityEngine.Component));
                foreach (var comp in components)
                {
                    if (comp is not Transform && comp is not MeshFilter && comp is not MeshRenderer)
                    {
                        Destroy(comp);
                    }
                }

                obj.transform.position = new Vector3(x, tier, z) + positionOffset;
                obj.transform.rotation = Random.rotation;
                obj.transform.localScale *= 1 / central.transform.localScale.x;
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
                    new Vector3(x, Random.Range(tier - 1.0f, tier), z) + positionOffset,
                    Quaternion.Euler(Random.Range(-15.0f, 15.0f), -1 * angle, Random.Range(-15.0f, 15.0f)));

                var scale = obj.transform.localScale;
                scale.x *= Random.Range(1.0f, 10.0f);
                scale.y *= Random.Range(1.0f, 10.0f);
                obj.transform.localScale = scale * woodScale;
                obCount++;
            }
        }
        nestObj.transform.position = Vector3.up * (maxTier) + positionOffset;
        if (nest != null)
        {
            nest.myEggCapacity += (int)steps;
            nest.PlayParticleEffect();
        }
        nestObj.SetSiblingIndex(NestNodeParent.transform.childCount - 1);
        nestObj.localScale = Vector3.one + Vector3.one * maxTier * centerScale * globalScale * 0.2f;
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

        foreach (var item in GameObject.FindGameObjectsWithTag("EditorNestDestructionTag"))
        {
            DestroyImmediate(item);
        }

        Vector3 positionOffset = spawnPosition.position;
        parentController = new GameObject("NestBase");
        parentController.tag = "EditorNestDestructionTag";
        parentController.transform.position = positionOffset;
        for (float tier = startTier; Mathf.FloorToInt(tier) < maxTier; tier += tierHeight)
        {
            GameObject central = Instantiate(centerMesh, parentController.transform);
            central.name = "Tier " + tier;
            central.transform.position = new Vector3(0, tier, 0) + positionOffset;
            central.transform.localScale *= tier * centerScale * globalScale;
            central.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            var bredth = Mathf.Log(tier * obPerTier);
            for (int i = 0; i < bredth; i++)
            {
                var v2 = Random.insideUnitCircle * tier * bredthMdf;
                v2.y *= aspectRatio;
                var obj = Instantiate(GetRandomAsset(), central.transform);

                var components = obj.GetComponents(typeof(UnityEngine.Component));
                foreach (var comp in components)
                {
                    if (comp is MeshFilter || comp is MeshRenderer || comp is Transform)
                    {
                        continue;
                    }
                    Destroy(comp);
                }

                obj.transform.position = new Vector3(v2.x, tier, v2.y) + positionOffset;
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
                    new Vector3(x, Random.Range(tier - 1.0f, tier), z) + positionOffset,
                    Quaternion.Euler(Random.Range(-15.0f, 15.0f), -1 * angle, Random.Range(-15.0f, 15.0f)));

                var scale = obj.transform.localScale;
                scale.x *= Random.Range(1.0f, 10.0f);
                scale.y *= Random.Range(1.0f, 10.0f);
                obj.transform.localScale = scale * woodScale;
                obCount++;
            }
        }

        GameObject nestObj = Instantiate(nestPrefab, parentController.transform);
        nestObj.transform.position = Vector3.up * (maxTier - 1) + positionOffset;
        nestObj.GetComponent<Nest>().myEggCapacity = maxTier * 2;
        nestObj.GetComponent<Nest>().SetCurrentEggCount(eggCount);
        nestBase = nestObj.GetComponent<Nest>();
    }
}

//[CustomEditor(typeof(NestCreator))]
// public class PileControllerTestEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        NestCreator myScript = (NestCreator)target;
//        if (GUILayout.Button("Build Object"))
//        {
//            myScript.DelayedBuildObject();
//        }
//    }
//}
