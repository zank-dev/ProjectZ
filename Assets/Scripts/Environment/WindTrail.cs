using UnityEngine;

public class WindTrail : MonoBehaviour
{
    // The wind trails move into a direction set by the spawner and random speed. 
    // When the trail is to far away from the player its not visible.

    public Vector3 maxWorld;
    public Vector3 direction;

    private float speed;
    private GameObject player;
    private TrailRenderer trail;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        speed = Random.Range(0.5f, 2f);
        player = GameObject.FindGameObjectWithTag("Player");

        trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speed * direction.x, Mathf.Sin(Time.time) * Time.deltaTime * 0.5f, Time.deltaTime * speed * direction.z);
        
        if (transform.position.z > (maxWorld.z * 2)) transform.position = new Vector3 (transform.position.x, transform.position.y, startPos.z - (maxWorld.z * 2f));
        if (transform.position.x > (maxWorld.x * 2)) transform.position = new Vector3(startPos.x - (maxWorld.x * 2f), transform.position.y, transform.position.z);

        if (player == null) return;
        if (Vector3.Distance(transform.position, player.transform.position) < 50f) trail.enabled = true;
        else trail.enabled = false;
    }
}