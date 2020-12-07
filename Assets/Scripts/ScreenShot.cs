using System;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    // atm only for personal use no game use!
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            DateTime date = DateTime.Now;
            ScreenCapture.CaptureScreenshot(date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString()
                + "-" + date.Hour.ToString() + "-" + date.Minute.ToString() + "-" + date.Second.ToString() + ".png");
        }
    }
}