using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public static SceneControlManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }
    public void LoadSceneAsync(string sceneName)
    {
        // 비동기로 씬 로딩을 시작합니다.
        StartCoroutine(LoadScene(sceneName));
        Debug.Log("LoadSceneAsync");
    }

    private IEnumerator LoadScene(string sceneNameToLoad)
    {
        // 비동기로 씬을 로드합니다.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNameToLoad);

        // 로딩이 완료될 때까지 기다립니다.
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.8f);
            Debug.Log("로딩 진행도: " + (progress * 100) + "%");
            yield return null; // 한 프레임을 대기하여 메인 스레드가 차단되지 않도록 합니다.
        }

        Debug.Log("로딩 완료!");
    }
}
