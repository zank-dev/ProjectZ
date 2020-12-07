using UnityEngine;

public class AnimationOnTrigger : MonoBehaviour
{
    // Plays the animation when the player enters the trigger.

    private Animator animator;
    private AudioSource source;
    private float randomTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        randomTime = Random.Range(0, 1.5f);
    }

    private void Update()
    {
        if(randomTime < 0)
        {
            animator.enabled = true;
        }
        randomTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            animator.SetTrigger("Collision");
            source.Play();
        }
    }
}