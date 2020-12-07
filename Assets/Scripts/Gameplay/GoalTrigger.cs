using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject finishedLevelMenu;
    public GameObject finishedLevelParticleEffect;

    private void OnTriggerEnter(Collider other)
    {
        // When the Player touches the Collider, the Player won
        if (other.gameObject.name == "Player")
        {
            Time.timeScale = 0f;
            Instantiate(finishedLevelParticleEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            finishedLevelMenu.SetActive(true);
        }
    }
}