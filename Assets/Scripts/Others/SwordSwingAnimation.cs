using UnityEngine;

public class SwordSwingAnimation : MonoBehaviour
{
    // When the player is in the area of the enemy, plays the sword swing animation and when the player leaves the area stops the animation.

    public AudioClip swordSwing;

    private Animator animator;
    private AudioSource source;

    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            animator.SetTrigger("PlayerTarget");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("EndAnimation");
    }

    public void PlaySwordSwingSound() // for the animations that sends out an event to the script
    {
        source.PlayOneShot(swordSwing);
    }
}
