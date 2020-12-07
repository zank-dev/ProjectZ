using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    // List of GameObjects in the Inspector of Unity
    public List<GameObject> listOfGameobjects = new List<GameObject>();

    private readonly Quaternion[] q = { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180, 0) };

    void Awake()
    {
        // Selects randomly on of the available Objects and spawns them at the Position where the Spawner is and destoys the Spawner
        GameObject gameObjectToSpawn = listOfGameobjects[Random.Range(0, listOfGameobjects.Count)];
        Instantiate(gameObjectToSpawn, transform.position, (q[Random.Range(0, 2)] * transform.rotation));
        Destroy(gameObject);
    }
}