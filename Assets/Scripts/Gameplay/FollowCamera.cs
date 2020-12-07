using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float rotationSpeed = 10f;   // Rotation speed for Camera
    public float rotationVelocity = 0.1f;
    public Transform playerTransform;   // The Position (Transform) of the Player
    public float cameraAngle = 0f;

    private float mousePositionX, mousePositionY;
    private GameManager gameManager;
    private bool fixedCamera = true;
    private Vector3 smoothedRotation;
    private Vector3 currentRotation;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.valueChanged.AddListener(UpdateFixedCamera);
    }

    void LateUpdate()
    {
        if (!playerTransform) return;
        // Moves the Camerarig to the Player position
        transform.position = playerTransform.position;

        HandleFixedCamera();
        HandleRelativeCamera();
    }

    private void HandleFixedCamera()
    {
        if (!fixedCamera) 
        {
            if (Input.GetMouseButton(1)) // When the player is holding the right Mousebutton
            {
                mousePositionX -= Input.GetAxis("Mouse X") * rotationSpeed * PlayerPrefs.GetInt("InverseRotationX", 1);
                mousePositionY -= Input.GetAxis("Mouse Y") * rotationSpeed * PlayerPrefs.GetInt("InverseRotationY", 1);
                mousePositionY = Mathf.Clamp(mousePositionY, -25, 40);
            }
            else // else he migh use his controller (if it is plugged in)
            {
                mousePositionX -= Input.GetAxis("ControllerCameraX") * rotationSpeed * PlayerPrefs.GetInt("InverseRotationX", 1);
                mousePositionY -= Input.GetAxis("ControllerCameraY") * rotationSpeed * PlayerPrefs.GetInt("InverseRotationY", 1);
                mousePositionY = Mathf.Clamp(mousePositionY, -25, 40);
            }

            // if player presses Horizontal buttons turns the camera and tries to be behind the player
            if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f && !Input.GetMouseButtonDown(1))
                mousePositionX += Input.GetAxis("Horizontal") * (rotationSpeed * 2) * Time.deltaTime;

            // moves the camera 
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mousePositionY, mousePositionX), ref smoothedRotation, rotationVelocity);
            transform.eulerAngles = currentRotation;
        }
    }

    // If the Player moves with fixed Camera forward or backwards, the Camera moves back to the Center. Depending on the Side the Camera rotates left or right
    private void HandleRelativeCamera()
    {
        if (fixedCamera) // camera is fixed and moved the camera back to origin (when it was moved)
        {
            // when rotation was over 360° % it under 360
            mousePositionX %= 360; currentRotation.x %= 360; currentRotation.y %= 360; currentRotation.z %= 360;

            // reverts vertical camera position 
            if (mousePositionY > cameraAngle) mousePositionY--;
            if (mousePositionY < cameraAngle) mousePositionY++;
            
            // reverts horizontal camera position in 4 steps to fix jumping camera 
            if (mousePositionX > 180 && mousePositionX < 360) mousePositionX++;
            if (mousePositionX < 0 && mousePositionX > -180) mousePositionX++;
            if (mousePositionX < -180 && mousePositionX > -360) mousePositionX--;
            if (mousePositionX > 0 && mousePositionX < 180) mousePositionX--;

            // moves the camera
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mousePositionY, mousePositionX), ref smoothedRotation, rotationVelocity * Time.deltaTime);
            transform.eulerAngles = currentRotation;
        }
    }

    private void UpdateFixedCamera(string name, float value)
    {
        if (name == "fixedCamera") fixedCamera = value == 1;
    }
}