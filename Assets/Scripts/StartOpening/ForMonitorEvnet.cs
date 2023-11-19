using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForMonitorEvnet : MonoBehaviour
{
    private Animator animator;
    private bool isAnimationFinished = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlaySequentialAnimations()
    {
        animator.SetBool("isEnlarge", true);
        SceneManager.LoadScene("Tutorial");
    }

}
