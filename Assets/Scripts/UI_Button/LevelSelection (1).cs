using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* SUBSCRIBING TO MY YOUTUBE CHANNEL: 'VIN CODES' WILL HELP WITH MORE VIDEOS AND CODE SHARING IN THE FUTURE :) THANK YOU */

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 5); /* < Change this int value to whatever your
                                                             level selection build index is on your
                                                             build settings */

        for (int i = 1; i < lvlButtons.Length; i++)
        {
            if (i + 6 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }

}
