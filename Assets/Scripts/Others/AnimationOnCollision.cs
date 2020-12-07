using UnityEngine;

public class AnimationOnCollision : MonoBehaviour
{
    // Plays animation when the player touches the object

    private Animator animator;
    private AudioSource source;

    private void Start()
    {
        animator = transform.GetComponentInParent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            animator.SetTrigger("Collision");
            source.Play();
        }
    }
}