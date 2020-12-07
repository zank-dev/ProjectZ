using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject mainMenu;
    public GameObject startGame;
    public GameObject continueGame;
    public GameObject credits;
    public GameObject transition;
    public Animator camRigAnimator;
    public Animator camAnimator;
    public Animator mainMenuAnimator;
    public Animator creditsAnimator;

    private AudioSource source;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("continueLevel")) // if the player has a save changes the text
        {
            mainMenuAnimator.enabled = false;
            creditsAnimator.SetTrigger("ContinueGame");
            startGame.SetActive(false);
            continueGame.SetActive(true);
            camAnimator.SetTrigger("ContinueGame");
            mainMenu.GetComponent<AddMenuButtonToEventSystem>().selectedGameObject = continueGame;
        }
        else
        {
            mainMenuAnimator.enabled = true;
            creditsAnimator.enabled = true;
        }

        Cursor.visible = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "";
        source = GetComponent<AudioSource>();
    }

    public void StartGame() // loads the first level
    {
        StartCoroutine(LoadLevel(1));
    }

    public void ContinueGame() // loads the save
    {
        StartCoroutine(LoadLevel(PlayerPrefs.GetInt("continueLevel")));
    }

    public void QuitGame() // exits the game
    {
        Debug.Log("Exits Game");
        source.mute = true;
        source.Stop();
        Application.Quit();
    }

    public void OpenSettings() // opens the settings menu
    {
        mainMenuAnimator.enabled = false;
        settings.SetActive(!settings.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void RemoveSave() // removes the save !only available in main menu!, but displayed in settings
    {
        PlayerPrefs.DeleteKey("continueLevel");
        PlayerPrefs.DeleteKey("currentPlayTime");
        PlayerPrefs.DeleteKey("leafs");
        PlayerPrefs.DeleteKey("currentTime");
        startGame.SetActive(true);
        continueGame.SetActive(false);
        mainMenu.GetComponent<AddMenuButtonToEventSystem>().selectedGameObject = startGame;
    }

    IEnumerator LoadLevel(int index)
    {
        mainMenu.SetActive(false);
        credits.SetActive(false);
        transition.SetActive(true);
        camRigAnimator.enabled = true;

        yield return new WaitForSeconds(2.5f);

        source.mute = true;
        source.Stop();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(index);
    }
}