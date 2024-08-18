using UnityEngine;

public class ControllerHolder : MonoBehaviour
{
    private NestCreator creator;

    public void Init(NestCreator c)
    {
        creator = c;
    }

    void AddMass(int mass)
    {
        creator.maxTier++;
        creator.BuildObject();
    }
}
