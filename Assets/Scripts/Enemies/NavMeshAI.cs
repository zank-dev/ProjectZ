using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    // When the player enters the area of the enemy, the enemy moves to the player
    // and when the player left the area the enemy returns to origin

    private Vector3 startPos;
    private Vector3 playerPos;
    private NavMeshAgent agent;

    private void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Target(Vector3 target)
    {
        agent.destination = target;
        if (agent.stoppingDistance > 0) transform.LookAt(target);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player") 
        {
            playerPos = other.transform.position;
            Target(playerPos);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        Target(startPos);
    }
}