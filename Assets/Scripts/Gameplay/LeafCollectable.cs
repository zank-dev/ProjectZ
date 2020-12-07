using UnityEngine;

public class LeafCollectable : MonoBehaviour
{
    public GameObject collectedLeafParticle;
    public Camera cam;
    public float speed = 20f;
    public int targetScreenPos = 100;

    private GameManager gameManager;
    private bool gotCollected;
    private Vector3 targetPos;
    private Vector3 startPos;
    private float startTime;
    private float journeyLength;
    private float sizeDifference;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) // sends out event and the collection effect
    {
        if (other.gameObject.name == "Player")
        {
            Instantiate(collectedLeafParticle, transform.position, Quaternion.identity);
            int leafs = (int)gameManager.GetValues("leafs");
            leafs++;
            gameManager.SetLeafs(leafs);

            startPos = transform.position;
            startTime = Time.time;
            gotCollected = true;
        }
    }

    private void Update()
    {
        if (transform.position == targetPos) Destroy(gameObject); // reached camera and destroys the object
        GotCollected();
    }

    private void GotCollected() // moves the object close to the camera
    {
        if (gotCollected)
        {
            targetPos = cam.ScreenToWorldPoint(new Vector3(0, Screen.currentResolution.height - 5.21f * (Screen.currentResolution.width / 100), 2));
            journeyLength = Vector3.Distance(startPos, targetPos);
            sizeDifference = Vector3.Distance(startPos, targetPos);

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            float fractionOfSize = distCovered / sizeDifference;

            transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.25f, 0.25f, 0.25f), fractionOfSize);
        }
    }
}