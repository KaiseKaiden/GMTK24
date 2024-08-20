using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] float myXp;
    [SerializeField] float myMaxXp;
    [SerializeField] float myXpExpo;

    [SerializeField] Image myHungerbarUI;
    [SerializeField] RectTransform myHungerbarTransform;

    [SerializeField] private TrailRenderer[] myTrails;
    float myStartWidth;

    int myCurrentLevel = 1;

    void Start()
    {
        AddXp(0.0f);

        myStartWidth = myTrails[0].widthMultiplier;
    }

    void Update()
    {
        myHungerbarTransform.localScale = Vector3.Lerp(myHungerbarTransform.localScale, Vector3.one, 3.0f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddXp(myMaxXp - myXp);
        }
    }

    public void AddXp(float aXp)
    {
        myXp += aXp;
        if (myXp >= myMaxXp)
        {
            myCurrentLevel++;
            myXp -= myMaxXp;

            myMaxXp = Mathf.Pow(myMaxXp, myXpExpo);

            transform.localScale += Vector3.one;

            foreach(TrailRenderer t in myTrails)
            {
                t.widthMultiplier = myStartWidth * transform.localScale.x * 0.5f;
            }
        }

        myHungerbarUI.fillAmount = 1.0f - (myXp / myMaxXp);
        myHungerbarTransform.localScale = Vector3.one * 1.25f;
    }

    public int GetCurrentLevel()
    {
        return myCurrentLevel;
    }
}
