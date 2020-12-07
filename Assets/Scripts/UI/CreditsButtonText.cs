using System;
using TMPro;
using UnityEngine;

public class CreditsButtonText : MonoBehaviour
{
    // changes text acording to user input

    public String mouseAndKeyboad = "";
    public String controller = "";
            
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "" ? mouseAndKeyboad : controller;
    }
}
