using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForLight : MonoBehaviour
{
    private Animator animator;
    private bool isAnimation1Finished = false;
    private bool isAnimation2Finished = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlaySequentialAnimations());
    }

    IEnumerator PlaySequentialAnimations()
    {
        animator.SetBool("isTwinkle", true);
        while (!isAnimation1Finished)
        {
            yield return null;
        }
        animator.SetBool("isTwinkle", false);


        animator.SetBool("isEnlarge", true);
        while (!isAnimation2Finished)
        {
            yield return null;
        }

        SceneManager.LoadScene("Main");
    }

    public void OnAnimation1Finished()
    {
        isAnimation1Finished = true;
    }

    public void OnAnimation2Finished()
    {
        isAnimation2Finished = true;
    }
}
