using TMPro;
using UnityEngine;

public class LevelTextController : MonoBehaviour
{
    private GameManager gameManager;
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.valueChanged.AddListener(UpdateCurrentLevel); // conects the text with the valueChangedEvent
        UpdateLevel();
    }

    private void UpdateCurrentLevel(string name, float value) // on event trigger updates the text
    {
        if (name == "currentLevel") text.text = $"Level {value}  / {gameManager.GetValues("levels")}";
    }

    private void UpdateLevel()
    {
        // gets the current level and the total levels
        text.text = $"Level {gameManager.GetValues("currentLevel")} / {gameManager.GetValues("levels")}";
    }
}