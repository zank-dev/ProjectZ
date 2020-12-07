using UnityEngine;

public class EndOfCameraAnimation : MonoBehaviour
{
    // After the camera movement, turns on the tutorial and the player

    public GameObject[] tutorialtext;
    public PlayerController playerController;

    public void EnableComponents()
    {
        foreach (GameObject gameObject in tutorialtext)
        {
            gameObject.SetActive(true);
        }

        playerController.enabled = true;
    }
}
