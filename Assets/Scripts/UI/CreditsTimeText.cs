using TMPro;
using UnityEngine;

public class CreditsTimeText : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        float time = PlayerPrefs.GetFloat("currentTime"); // gets the value and the stored playtime
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
