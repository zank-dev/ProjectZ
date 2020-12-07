using UnityEngine;

public class DealDamageOnTrigger : MonoBehaviour
{
    // When the player enters the trigger an event gets send out to deal 1 damage to the player

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            int health = (int)gameManager.GetValues("health");
            health--;
            gameManager.SetHealth(health);
        }
    }
}