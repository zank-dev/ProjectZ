using UnityEngine;

public class LostLevelMenu : MonoBehaviour
{
    // stops gameplay music and shows cursor or not

    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.GetComponent<StopGameplayMusic>().StopMusic();
        Cursor.visible = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "";
    }
}
