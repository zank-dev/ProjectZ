using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;                                     // The Speed of the Player
    public float jumpHeight;                                // The Jumpheight (Ridgidbody Force) of the Player
    public float jumpTimer;                                 // The Timer for higher jumps, when button is held down
    public float wasGroundedTimer;                          // Timer for wasGrounded
    public LayerMask groundMask;                            // LayerMask to detect what is Ground
    public GameObject lostLevel;
    public Transform cameraRig;
    public AudioClip bounceSound;
    public AudioClip collisionSound;

    private bool isJumping, isGrounded, playerJumped, isFixedCamera = true, firstCollision = true;
    private float horizontal, vertical, jumpTimerCountdown; // Horizontal and Vertical are for Inputs and the Countdown for the Jumping
    private float wasGroundedTimerCountdown;                // Countdown for wasGrounded
    private Rigidbody rb;                                   // The Ridgidbody of the Player
    private GameManager gameManager;
    private AudioSource source;

    private void Start()
    {
        // Get the Ridgidbody of the Player and the CameraController
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.valueChanged.AddListener(PlayerHealth);
        gameManager.valueChanged.AddListener(UpdateFixedCamera);
        gameManager.SetFixedCamera(isFixedCamera);
    }

    private void Update()
    {
        InputHandler();
        GroundHandler();
    }

    private void FixedUpdate()
    {
        MovementHandler();
        JumpHandler();
    }

    private void InputHandler()
    {
        // Stores the Inputvalues from the InputManager
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Sets isJumping to true, when the Player is pressing the ButtonDown and when the Player is on the Ground
        if (isGrounded) wasGroundedTimerCountdown = wasGroundedTimer;
        else wasGroundedTimerCountdown -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && wasGroundedTimerCountdown >= 0f) isJumping = true;

        // Resets the values, when the Player is releasing the Jump Button
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            playerJumped = false;
        }

        if (Input.GetButtonDown("Camera"))
        {
            isFixedCamera = !isFixedCamera;
            gameManager.SetFixedCamera(isFixedCamera);
        }
    }

    private void GroundHandler()
    {
        // Creates a ColliderArray and stores all the Objects that got hit from the OverlapsSpehere that are in the LayerMask "Ground"
        Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0f, -0.1f, 0f), 0.275f, groundMask);

        // If there is something in the Array the Player is still on the Ground, otherwise he is falling / jumping
        if (colliders.Length > 0) isGrounded = true;
        else isGrounded = false;
    }

    private void MovementHandler()
    {
        if (isGrounded) // If on Ground 
        {
            Vector3 moveDirection = Vector3.zero;

            // Inputs to move with fixed Camera
            if (isFixedCamera)
            {
                moveDirection += Vector3.forward * vertical;
                moveDirection += Vector3.right * horizontal;
                moveDirection.y = 0f;
            } else // otherwise it's relative 
            {
                moveDirection += cameraRig.forward * vertical;
                moveDirection += cameraRig.right * horizontal;
                moveDirection.y = 0f;
            }

            // moves player
            rb.AddForce(moveDirection.normalized * Time.fixedDeltaTime * speed, ForceMode.Acceleration);
        }
    }

    private void JumpHandler()
    {
        if (isJumping) // If the Player pressed the JumpButton 
        {
            // Adds a Force to the Player
            JumpForce(jumpHeight);

            // The Player Jumped now
            isJumping = false;
            playerJumped = true;

            // Sets the CountdownTimer to allow higher jumps
            jumpTimerCountdown = jumpTimer;
        }

        // If the Player is holding the JumpButton and he pressed the Button before
        if (Input.GetButton("Jump") && playerJumped)
        {
            if (jumpTimerCountdown > 0) // Add Force as long as the Countdown is over 0
            {
                jumpTimerCountdown -= Time.fixedDeltaTime;
                JumpForce(jumpHeight / 2); // The Force is halfed 
            }
            else playerJumped = false; // when countdown is under 0, do not allow higher jumps
        }
    }

    private void JumpForce(float force)
    {
        rb.AddForce(force * Time.fixedDeltaTime * Vector3.up, ForceMode.Impulse);
    }

    private void PlayerHealth(string name, float health)
    {
        if (name == "health" && health <= 0)
        {
            Time.timeScale = 0f;
            lostLevel.SetActive(true);
            Cursor.visible = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "";
        }
    }

    private void UpdateFixedCamera(string name, float value)
    {
        if (name == "fixedCamera") isFixedCamera = value == 1;
    }

    // Draws the OverlapsSphere into the Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, -0.1f, 0f), 0.275f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!firstCollision) source.PlayOneShot(collisionSound); // to remove the spawn Bounce sound
        else firstCollision = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        source.PlayOneShot(bounceSound);
    }
}