using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProloguetoTutorial : MonoBehaviour
{
    public GameManager formonitorevent;
    public string cutsceneTexts;
    public TMP_Text Text;
    bool istyping = false;

    private IEnumerator ShowCutscene()
    {
        string textToType = cutsceneTexts;
        yield return StartCoroutine(TypeText(textToType));
        ForMonitorEvnet forMonitorEvent = FindObjectOfType<ForMonitorEvnet>();
        if (forMonitorEvent != null)
        {
            forMonitorEvent.PlaySequentialAnimations();
        }

    }
    IEnumerator TypeText(string textToType)
    {
        istyping = true;
        Text.text = "";
        char[] charsToType = textToType.ToCharArray();
        for (int i = 0; i < charsToType.Length; i++)
        {
            Text.text += charsToType[i];
            yield return new WaitForSeconds(0.05f);
            if (!istyping)
            {
                Text.text = textToType;
                break;
            }
        }
        istyping = false;
    }

}
