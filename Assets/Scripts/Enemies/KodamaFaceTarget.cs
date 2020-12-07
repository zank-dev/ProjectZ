using UnityEngine;
using UnityEngine.Animations.Rigging;

public class KodamaFaceTarget : MonoBehaviour
{
    // Used with Unity Animation Rigging. When the player enters the area of the Kodama, the Kodama will look at the player.

    public Transform targetController;
    public Transform faceTarget;
    public AudioClip seeingPlayer;
    public MultiAimConstraint multiAimConstraint;
    public float weightSpeed = 0.05f;
    public float turnSpeed = 0.3f;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float weight;
        Vector3 targetPosition;

        weight = faceTarget == null ? 0 : 1; // Adjusts the weights for rigging
        targetPosition = faceTarget == null ? 
            transform.position + transform.forward + Vector3.up / 2 : faceTarget.position + Vector3.up / 2; // looks forward or at player

        // makes the head movements smooth
        multiAimConstraint.weight = Mathf.Lerp(multiAimConstraint.weight, weight, weightSpeed);
        targetController.position = Vector3.Lerp(targetController.position, targetPosition, turnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        { 
            faceTarget = other.transform;
            source.PlayOneShot(seeingPlayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player") faceTarget = null;
    }
}