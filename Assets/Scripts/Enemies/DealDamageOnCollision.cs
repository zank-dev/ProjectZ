using UnityEngine;

public class DealDamageOnCollision : MonoBehaviour
{
    // When the player touches the object an event gets send out to deal 1 damage to the player

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            int health = (int) gameManager.GetValues("health");
            health--;
            gameManager.SetHealth(health);
        }
    }
}