using System.Collections;
using UnityEngine;

public class RemoveKodamaTrigger : MonoBehaviour
{
    // Removes the Kodama and the trigger and spawns a new Kodama.

    public GameObject destroyFirstKodama;
    public GameObject spawnNewKodama;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Destroy(destroyFirstKodama);
            source.Play();
            StartCoroutine(SpawnNewKodama());
        }
    }

    IEnumerator SpawnNewKodama()
    {
        yield return new WaitForSeconds(0.25f);

        spawnNewKodama.SetActive(true);
        Destroy(gameObject);
    }
}
