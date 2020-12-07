using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    // After 5 seconds the object gets destroyed

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}