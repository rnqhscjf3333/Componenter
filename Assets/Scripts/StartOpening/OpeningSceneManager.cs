using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void forSkipButton()
    {
        SceneManager.LoadScene("Main");
    }
}
