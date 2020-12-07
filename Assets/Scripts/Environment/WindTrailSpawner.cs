using UnityEngine;

public class WindTrailSpawner : MonoBehaviour
{    
    // Spawns the wind trail gameobjects

    public int minWindTrails;
    public int maxWindTrails;
    public GameObject windTrail;
    public Vector3 worldArea;
    public Vector3 direction;

    private void Awake()
    {
        for (int i = 0; i < Random.Range(minWindTrails, maxWindTrails); i++)
        {
            Vector3 preparedPos = new Vector3(Random.Range(-(worldArea.x + worldArea.x / 2), worldArea.x + worldArea.x / 2) + transform.position.x,
                Random.Range(2.5f, 10f) + transform.position.y / 2, Random.Range(-(worldArea.z * 10), worldArea.z) + transform.position.z);

            GameObject windTrailGameObject = Instantiate(windTrail);
            windTrailGameObject.transform.position = preparedPos + transform.position;
            windTrailGameObject.GetComponent<WindTrail>().maxWorld = worldArea;
            windTrailGameObject.GetComponent<WindTrail>().direction = direction;
        }
    }
}