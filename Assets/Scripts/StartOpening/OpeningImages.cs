using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningImages : MonoBehaviour
{
    public Image[] cutsceneImages;
    public TMP_Text Text;
    bool istyping = false;
    public Sprite[] cutsceneSprites;
    public string[] cutsceneTexts;
    public float imageDisplayTime = 1f;
    public GameObject cut2;
    public GameObject cut3;
    private int currentImageIndex = 0;
    
    private void Start()
    {
        StartCoroutine(ShowCutscene());
    }

    private IEnumerator ShowCutscene()
    {
        if (currentImageIndex < cutsceneSprites.Length)
        {
            cutsceneImages[currentImageIndex].sprite = cutsceneSprites[currentImageIndex];
            cutsceneImages[currentImageIndex].gameObject.SetActive(true);
            string textToType = cutsceneTexts[currentImageIndex];
            yield return StartCoroutine(TypeText(textToType));
            yield return new WaitForSeconds(imageDisplayTime);
            
            currentImageIndex++;

            if (currentImageIndex < cutsceneImages.Length)
            {
                StartCoroutine(ShowCutscene());
            }
            else
            {
                if (gameObject.name == "Cut1")
                {
                    this.gameObject.SetActive(false);
                    cut2.SetActive(true);
                }
                if(gameObject.name == "Cut2")
                {
                    this.gameObject.SetActive(false);
                    cut3.SetActive(true);
                }
            }
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
