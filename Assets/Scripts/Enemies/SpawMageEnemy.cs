using UnityEngine;

public class SpawMageEnemy : MonoBehaviour
{
    // When the player is in the area of the enemy, the enemy will spawn more enemys. This will be first indicated with some effects.

    public float spawnRate;
    public GameObject spawnEnemy;
    public GameObject spawnIndicator;

    private float spawnCountdown;
    private GameObject player;
    private GameObject indicator;
    private AudioSource source;
    private bool spawned = false;

    private void Start()
    {
        spawnCountdown = spawnRate;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(player != null)
        {
            if (spawnCountdown <= 2.5f && !spawned) // will spawn an indicator
            { 
                indicator = Instantiate(spawnIndicator, player.transform.position, Quaternion.identity);
                spawned = true;
            }

            if(indicator != null) // moves the indicator on the player
                indicator.transform.position = new Vector3(player.transform.position.x, 10f, player.transform.position.z);

            if (spawnCountdown <= 0f) // spawns enemy 
            {
                Destroy(indicator);
                source.Play();
                spawnCountdown = spawnRate;
                Vector2 randomPos = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                Vector3 spawnPos = new Vector3(player.transform.position.x + randomPos.x, transform.position.y + 0.15f, player.transform.position.z + randomPos.y);
                Instantiate(spawnEnemy, spawnPos, Quaternion.identity);
                spawned = false;
            }

            transform.LookAt(player.transform.position);
            spawnCountdown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player") player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
        spawnCountdown = spawnRate;
    }
}