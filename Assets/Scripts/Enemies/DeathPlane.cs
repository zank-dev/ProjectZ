using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // When the player falls through the plane, the player lost the level / died

    public GameObject lostLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Time.timeScale = 0f;
            Cursor.visible = Input.GetJoystickNames().Length == 0 ||  Input.GetJoystickNames()[0] == "";
            lostLevel.SetActive(true);
        }
    }
}