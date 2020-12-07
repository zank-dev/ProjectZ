using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject finishedLevelMenu;
    public GameObject finishedLevelParticleEffect;
    public GameObject lostLevelMenu;
    public GameObject resolutionDropDownMenu;
    public GameObject qualityDropDownMenu;
    public Animator transition;
    public Toggle isFixedCameraToggle;

    private GameManager gameManager;
    private GameObject player;
    private bool coroutineIsRunning;

    private void Start()
    {
        Cursor.visible = false;
        coroutineIsRunning = false;
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GetComponent<GameManager>();
        gameManager.SetCurrentLevel(SceneManager.GetActiveScene().buildIndex);
        gameManager.valueChanged.AddListener(UpdateFixedCameraChanged); // conects the text with the valueChangedEvent
        PlayerPrefs.SetInt("continueLevel", SceneManager.GetActiveScene().buildIndex); // saves the current level 

        UpdateFixedCamera();
    }

    private void Update()
    {
        if (finishedLevelMenu.activeSelf == true) return;
        if (lostLevelMenu.activeSelf == true) return;
        if (coroutineIsRunning) return;

        if (Input.GetButtonDown("Escape") && settingsMenu.activeSelf == false) PauseMenu(); // opens / closes pausemenu
        if (Input.GetButtonDown("Escape") && settingsMenu.activeSelf == true // open closes settings menu
            && resolutionDropDownMenu.transform.childCount < 4 && qualityDropDownMenu.transform.childCount < 4) OpenSettingsMenu();

        if (pauseMenu.activeSelf) gameManager.SetTime(Time.realtimeSinceStartup); // updates time when pausemenu is open
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf); // open or closes the menu

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f; // pauses the game
            Cursor.visible = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == ""; // when controller is not pluged in display cursor
        }
        else 
        { 
            Time.timeScale = 1f; // unpauses the game
            Cursor.visible = false;
        } 
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void Restart()
    {
        Instantiate(finishedLevelParticleEffect, player.transform.position, Quaternion.identity);
        Destroy(player);
        Time.timeScale = 1f;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        lostLevelMenu.SetActive(false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReturnToMainMenu()
    {
        Instantiate(finishedLevelParticleEffect, player.transform.position, Quaternion.identity);
        Destroy(player);
        Time.timeScale = 1f;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        lostLevelMenu.SetActive(false);
        StartCoroutine(LoadLevel(0));
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        finishedLevelMenu.SetActive(false);
        PlayerPrefs.SetInt("leafs", (int)gameManager.GetValues("leafs") + PlayerPrefs.GetInt("leafs", 0));
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        yield return new WaitForSeconds(0.5f);

        transition.SetTrigger("Fade");
        coroutineIsRunning = true;

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(index);
    }
    
    // all Camera Options part of the Settings, but dispalyed here, because the settings are not the same in main menu
    private void UpdateFixedCameraChanged(string name, float value) // on event trigger updates the text
    {
        if (name == "fixedCamera") isFixedCameraToggle.isOn = value == 1;
    }

    private void UpdateFixedCamera()
    {
        // gets the current Camera status
        float value = gameManager.GetValues("fixedCamera");
        isFixedCameraToggle.isOn = value == 1;
    }

    public void SetFixedCameraSetting(bool isFixedCamera)
    {
        gameManager.SetFixedCamera(isFixedCamera);
    }

    public void SetInverseHorizontalCamera(bool horizontalInversed)
    {
        int value;
        value = horizontalInversed ? -1 : 1;
        PlayerPrefs.SetInt("InverseRotationX", value);
    }

    public void SetInverseVerticalCamera(bool verticalInversed)
    {
        int value;
        value = verticalInversed ? -1 : 1;
        PlayerPrefs.SetInt("InverseRotationY", value);
    }
}