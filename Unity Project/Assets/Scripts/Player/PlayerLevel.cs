using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] int myThicknessLevel;
    [SerializeField] float myXp;
    [SerializeField] float myMaxXp;
    [SerializeField] float myXpExpo;

    [SerializeField] Image myHungerbarUI;
    [SerializeField] RectTransform myHungerbarTransform;

    int myCurrentLevel;

    void Update()
    {
        myHungerbarTransform.localScale = Vector3.Lerp(myHungerbarTransform.localScale, Vector3.one, 5.0f * Time.deltaTime);
    }

    public void AddXp(float aXp)
    {
        myXp += aXp;
        if (myXp > myMaxXp)
        {
            myCurrentLevel++;
            myXp -= myMaxXp;

            myMaxXp = Mathf.Pow(myMaxXp, myXpExpo);

            transform.localScale += transform.localScale;
        }

        myHungerbarUI.fillAmount = (myXp / myMaxXp);
        myHungerbarTransform.localScale = Vector3.one * 1.5f;
    }

    public int GetCurrentLevel()
    {
        return myCurrentLevel;
    }
}
