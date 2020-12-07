using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void Restart()
    {
        // deletes saves and returns to main menu
        PlayerPrefs.DeleteKey("continueLevel"); 
        PlayerPrefs.DeleteKey("leafs");
        PlayerPrefs.DeleteKey("currentTime");
        SceneManager.LoadScene(0);
    }
}