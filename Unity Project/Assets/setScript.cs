using UnityEditor;
using UnityEngine;

public class setScript : MonoBehaviour
{

    void RecursiveAddComponents(Transform transform)
    {
        var rb = transform.parent.GetComponent<Rigidbody>();
        var capsule = transform.gameObject.AddComponent<CapsuleCollider>();

        capsule.radius = 0.0001f;
        capsule.height = 0.0001f;
        transform.gameObject.AddComponent<ConfigurableJoint>().connectedBody = rb;
        ConfigurableJointExtensions.SetupAsCharacterJoint(GetComponent<ConfigurableJoint>());
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            RecursiveAddComponents(child);
        }
    }

    public void AddStuff()
    {
        RecursiveAddComponents(transform);
    }
}

[CustomEditor(typeof(setScript))]
public class setScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        setScript myScript = (setScript)target;
        if (GUILayout.Button("PlaceStuff"))
        {
            myScript.AddStuff();
        }
    }
}
