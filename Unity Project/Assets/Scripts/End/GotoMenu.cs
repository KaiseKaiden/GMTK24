using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GotoMenu : MonoBehaviour
{
    [SerializeField] GameObject myRootObject;

    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

        DontDestroyOnLoad(myRootObject);
    }

    public void ChangeToMenu()
    {
        SceneManager.LoadScene("MainMenu");

        myAnimator.SetTrigger("FadeOut");
    }

    public void Clear()
    {
        Destroy(myRootObject);
    }
}
