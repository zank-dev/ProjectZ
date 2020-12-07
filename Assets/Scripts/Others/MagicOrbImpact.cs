using UnityEngine;

public class MagicOrbImpact : MonoBehaviour
{
    // When the orb hits the player, destroys the orb and spawns particles.

    public GameObject orbImpactParticles;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Instantiate(orbImpactParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}