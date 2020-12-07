using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    // When the player leaves the area the camera will go to main camera and removes the tutorial text.

    public GameObject mainCam;
    public GameObject secondCam;
    public GameObject removeTutorial;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            mainCam.SetActive(true);
            secondCam.SetActive(false);
            Destroy(removeTutorial);
            Destroy(gameObject);
        }
    }
}
