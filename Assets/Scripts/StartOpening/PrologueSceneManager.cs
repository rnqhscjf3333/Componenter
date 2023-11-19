using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueSceneManager : MonoBehaviour
{
    public void forSkipButton()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
