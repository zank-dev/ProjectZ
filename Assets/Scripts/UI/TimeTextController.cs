using TMPro;
using UnityEngine;

public class TimeTextController : MonoBehaviour
{
    private GameManager gameManager;
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.valueChanged.AddListener(UpdateTime); // conects the text with the valueChangedEvent
        SetTime();
    }

    private void UpdateTime(string name, float value) // on event trigger updates the text
    {
        if (name == "timePlayed")
        {
            float time = value + PlayerPrefs.GetFloat("playTime", 0); // gets the value and the stored playtime 
            text.text = PreparedTime(time);
        }
    }

    private void SetTime() // gets the value from gamemanager and sets the text
    {
        float time = gameManager.GetValues("timePlayed") + PlayerPrefs.GetFloat("playTime", 0); // gets the value and the stored playtime 
        text.text = PreparedTime(time);
    }

    private string PreparedTime(float time) // prepares timefomat
    {
        // converts the float into hours, minutes, seconds with leading zeros if needed
        int hours = (int)time / 3600;
        int minutes = (int)(time - (3600 * hours)) / 60;
        int seconds = (int)time - 3600 * hours - minutes * 60;
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}