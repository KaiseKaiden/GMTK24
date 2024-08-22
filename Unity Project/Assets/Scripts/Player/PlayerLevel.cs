using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField]
    float myXp;
    [SerializeField]
    float myMaxXp;
    [SerializeField]
    float myXpExpo;

    [SerializeField]
    Image myHungerbarUI;
    [SerializeField]
    RectTransform myHungerbarTransform;

    [SerializeField]
    private TrailRenderer[] myTrails;
    float myStartWidth;

    [SerializeField] ParticleSystem myLevelupParticle;

    int myCurrentLevel = 1;

    Vector3 myStartScale;
    float myLevelUpEaseTimer;

    CharacterJoint[] myJointList;
    Vector3[] myConnectedAnchors;

    void Start()
    {
        AddXp(0.0f);

        myStartWidth = myTrails[0].widthMultiplier;

        myLevelUpEaseTimer = 1.0f;
        GetJointData();
    }

    void Update()
    {
        myHungerbarTransform.localScale = Vector3.Lerp(myHungerbarTransform.localScale, Vector3.one, 3.0f * Time.deltaTime);

        // Upgrade
        myLevelUpEaseTimer += Time.deltaTime * 2.0f;
        transform.localScale = myStartScale + Vector3.one * EaseOutBack(Mathf.Clamp01(myLevelUpEaseTimer));
        //UpdateJointData();
    }

    public void AddXp(float aXp)
    {
        myXp += aXp;
        if (myXp >= myMaxXp)
        {
            myCurrentLevel++;
            myXp -= myMaxXp;

            myMaxXp = Mathf.Pow(myMaxXp, myXpExpo);

            transform.localScale = myStartScale + Vector3.one * EaseOutElastic(myLevelUpEaseTimer);
            myStartScale = transform.localScale;
            myLevelUpEaseTimer = 0.0f;

            foreach (TrailRenderer t in myTrails)
            {
                t.widthMultiplier = myStartWidth * transform.localScale.x * 0.5f;
            }

            myLevelupParticle.Play();
        }

        myHungerbarUI.fillAmount = 1.0f - (myXp / myMaxXp);
        myHungerbarTransform.localScale = Vector3.one * 1.25f;
    }

    public int GetCurrentLevel()
    {
        return myCurrentLevel;
    }

    float EaseOutElastic(float aValue)
    {
        const float c4 = (2.0f * Mathf.PI) / 3.0f;

        return aValue == 0.0f ? 0.0f
               : aValue == 1.0f ? 1.0f
                                : Mathf.Pow(2.0f, -10.0f * aValue) * Mathf.Sin((aValue * 10.0f - 0.75f) * c4) + 1.0f;
    }

    float EaseOutBounce(float aValue)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (aValue < 1.0f / d1) {
            return n1* aValue * aValue;
        }
        else if (aValue < 2 / d1)
        {
            return n1 * (aValue -= 1.5f / d1) * aValue + 0.75f;
        }
        else if (aValue < 2.5 / d1)
        {
            return n1 * (aValue -= 2.25f / d1) * aValue + 0.9375f;
        }
        else
        {
            return n1 * (aValue -= 2.625f / d1) * aValue + 0.984375f;
        }
    }

    float EaseOutBack(float aValue)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;

        return 1.0f + c3 * Mathf.Pow(aValue - 1.0f, 3.0f) + c1 * Mathf.Pow(aValue - 1.0f, 2.0f);
    }

    void GetJointData()
    {
        myJointList = GetComponentsInChildren<CharacterJoint>();
        myConnectedAnchors = new Vector3[myJointList.Length];

        for (int i = 0; i < myJointList.Length; i++)
        {
            myConnectedAnchors[i] = myJointList[i].connectedAnchor;
        }
    }

    void UpdateJointData()
    {
        for (int i = 0; i < myJointList.Length; i++)
        {
            myJointList[i].connectedAnchor = myConnectedAnchors[i] * transform.localScale.x;
        }
    }
}
