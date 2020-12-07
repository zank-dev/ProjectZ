using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ValueChangedEvent : UnityEvent<string, float> { }; // Class for ValueChangedEvent

public class GameManager : MonoBehaviour
{
    public ValueChangedEvent valueChanged;  // ValueChangedEvent

    private static int levels;              // the amount of levels
    public static float timePlayed = 0f;  // the total time played
    public static int currentLevel;         // the current level
    private int health = 1;                 // player health
    private int leafs = 0;                  // collectables
    private float levelStartTime;           // the start time of the level
    private bool fixedCamera; 

    private void Start()
    {
        valueChanged = new ValueChangedEvent();
        levels = SceneManager.sceneCountInBuildSettings - 2; // the amount of levels minus main menu and credit
        levelStartTime = Time.realtimeSinceStartup;
    }

    public float GetValues(string name) // gets the value from the manager
    {
        switch (name)
        {
            case "leafs": 
                return leafs;
            case "health":
                return health;
            case "levels":
                return levels;
            case "timePlayed":
                return timePlayed;
            case "currentLevel":
                return currentLevel;
            case "currentLevelTime":
                return levelStartTime;
            case "fixedCamera":
                return fixedCamera == true ? 1 : 0;
            default:
                return -1;
        }
    }

    public void SetTime(float time) // sets the time
    {
        timePlayed = time;
        valueChanged.Invoke("timePlayed", time); // and triggers the event
    }

    public void SetHealth(int health) // sets the health
    {
        this.health = health;
        valueChanged.Invoke("health", health); // and triggers the event
    }

    public void SetLeafs(int leafs) // sets the collectables
    {
        this.leafs = leafs;
        valueChanged.Invoke("leafs", leafs); // and triggers the event
    }

    public void SetCurrentLevel(int index) // sets the current level
    {
        currentLevel = index;
        valueChanged.Invoke("currentLevel", index); // and triggers the event
    }

    public void SetFixedCamera(bool value) // sets the fixedCamera value
    {
        fixedCamera = value;
        float floatValue = value ? 1 : 0;

        valueChanged.Invoke("fixedCamera", floatValue); // sends the converted value
    }
}