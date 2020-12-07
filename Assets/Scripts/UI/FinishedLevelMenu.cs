using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedLevelMenu : MonoBehaviour
{
    // Shows the current time of the level and the highscore and updates the highscore

    public TMP_Text currentTime;
    public TMP_Text highScoreTime;
    
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.GetComponent<StopGameplayMusic>().StopMusic();
        Cursor.visible = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "";
        DisplayHighScoreTime();
        UpdateCurrentTime();
    }

    private void UpdateCurrentTime()
    {
        float time = Time.realtimeSinceStartup - gameManager.GetValues("currentLevelTime");
        currentTime.text = PreparedScore(time);
        PlayerPrefs.SetFloat("currentTime", time + PlayerPrefs.GetFloat("currentTime", 0));
        // if the time is lower than the saved score or when there is no save, saves the score
        if (time < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name.ToString(), time + 1))
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name.ToString(), time);
    }

    private void DisplayHighScoreTime()
    {
        float time = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name.ToString(), 0);
        highScoreTime.text = PreparedScore(time);
    }

    private string PreparedScore(float score)
    {
        int hours = (int)score / 3600;
        int minutes = (int)(score - (3600 * hours)) / 60;
        int seconds = (int)score - 3600 * hours - minutes * 60;
        int milliseconds = (int)(score * 1000) % 1000;
        return string.Format("{0:00}:{1:00}:{2:00}<size=16>{3:000}", hours, minutes, seconds, milliseconds);
    }
}