using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TutorialText : MonoBehaviour
{
    public String mouseAndKeyboad = "";
    public String controller = "";

    private TMP_Text text;
    private string textContent;
    private readonly string[] startString = { "<sup>", "<sub>" };
    private readonly string[] endString = { "</sup>", "</sub>" };

        private void Start()
    {
        text = GetComponent<TMP_Text>();
        textContent = Input.GetJoystickNames().Length == 0 ||  Input.GetJoystickNames()[0] == "" ? mouseAndKeyboad : controller;
        text.text = "";

        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        TMP_TextInfo textInfo = text.textInfo;

        for (int i = 0; i < textContent.Length; i++) // fills the TMP with text
        {
            text.text += textContent.Substring(i, 1);

            yield return new WaitForSeconds(0.25f);
        }

        while (true) // loops through the text to add an effect
        {
            int charCount = textInfo.characterCount;

            for (int i = 0; i < charCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible) continue;

                text.text = textContent.Insert(i, startString[i % 2]).Insert(i + startString[i % 2].Length + 1, endString[i % 2]);
                yield return new WaitForSeconds(0.25f);
            }

            Array.Reverse(startString);
            Array.Reverse(endString);
        }
    }
}