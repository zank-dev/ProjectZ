using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    // When the player is in the area of the enemy, the enemy shoots at the player with random forces

    public float range;
    public float fireRate;
    public float forcePower;
    public GameObject magicOrb;
    public Transform firePoint;
    public AudioClip shootingOrb;

    private float fireCountdown = 0f;
    private GameObject player;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) <= range && fireCountdown <= 0f)
            {
                fireCountdown = 1f / fireRate;

                // spawns orb and pushes it and plays sound
                GameObject mOrb = Instantiate(magicOrb, firePoint.position, Quaternion.identity);
                mOrb.AddComponent<Rigidbody>().AddForce(transform.forward * (forcePower + Random.Range(0f, 100f)));
                mOrb.GetComponent<Rigidbody>().AddForce(transform.right * Mathf.Sin(Time.time) * 100f);
                source.PlayOneShot(shootingOrb);
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player") player = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
        fireCountdown = 1f / fireRate;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}