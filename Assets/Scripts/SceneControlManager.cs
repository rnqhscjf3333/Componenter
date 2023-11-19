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
        // �񵿱�� �� �ε��� �����մϴ�.
        StartCoroutine(LoadScene(sceneName));
        Debug.Log("LoadSceneAsync");
    }

    private IEnumerator LoadScene(string sceneNameToLoad)
    {
        // �񵿱�� ���� �ε��մϴ�.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNameToLoad);

        // �ε��� �Ϸ�� ������ ��ٸ��ϴ�.
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.8f);
            Debug.Log("�ε� ���൵: " + (progress * 100) + "%");
            yield return null; // �� �������� ����Ͽ� ���� �����尡 ���ܵ��� �ʵ��� �մϴ�.
        }

        Debug.Log("�ε� �Ϸ�!");
    }
}
