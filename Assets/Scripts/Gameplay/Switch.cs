using UnityEngine;

public class Switch : MonoBehaviour
{
    // When the player touches the switch the animations will play with sound

    public GameObject target;

    private Animator targetAnimator;
    private Animator switchAnimator;
    private AudioSource source;

    private void Start()
    {
        targetAnimator = target.GetComponent<Animator>();
        switchAnimator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player" && targetAnimator.enabled == false)
        {
            targetAnimator.enabled = true;
            switchAnimator.enabled = true;
            source.Play();
        }
    }
}