using UnityEngine;

public class StopGameplayMusic : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        source.mute = true;
        source.Stop();
    }
}