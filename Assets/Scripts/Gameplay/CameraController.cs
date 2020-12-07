using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
	public float minZoomDistance = 2f;
	public float maxZoomDistance = 12f;
	public float cameraMoveSpeed = 5f;
	public float cameraZoomSpeed = 100f;
	public float distanceFromObstacle = 5f;

	private float distance;
	private float currentZoomDistance;
	private float hideObjectTimer = 0.5f;
	private float moveTimer = 0.125f;
	private Vector3 playerDirection;
	private GameObject obstacleObject;
	private string[] joysticks;

	private void Start()
	{
		joysticks = Input.GetJoystickNames();
		playerDirection = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
		currentZoomDistance = distance;
	}

	private void LateUpdate()
	{
		CameraZoom();
		CameraCollision();

		// Sets Camera Position
		transform.localPosition = Vector3.Lerp(transform.localPosition, playerDirection * distance, Time.deltaTime * cameraMoveSpeed);
	}

	private void CameraZoom()
    {
		if (joysticks.Length != 0) // Controller Input
		{
			if (joysticks[0] != "") // the controller is known
			{
				// Adds the inputs together, because they have no rest state
				float controllerZoom = 0f;
				if (Input.GetAxisRaw("ControllerZoomIn") > 0f) controllerZoom += Input.GetAxisRaw("ControllerZoomIn");
				if (Input.GetAxisRaw("ControllerZoomOut") < 0f) controllerZoom += Input.GetAxisRaw("ControllerZoomIn");

				currentZoomDistance = Mathf.Clamp(currentZoomDistance - controllerZoom * cameraZoomSpeed * Time.deltaTime, minZoomDistance, maxZoomDistance);
			}
			else // there was a Controller, but disconnected, so allow Mouse Input
				currentZoomDistance = Mathf.Clamp(currentZoomDistance - Input.GetAxisRaw("Mouse ScrollWheel") * cameraZoomSpeed * Time.deltaTime, minZoomDistance, maxZoomDistance);
		}
		else // no Controller => Mouse Input
			currentZoomDistance = Mathf.Clamp(currentZoomDistance - Input.GetAxisRaw("Mouse ScrollWheel") * cameraZoomSpeed * Time.deltaTime, minZoomDistance, maxZoomDistance);
	}

	private void CameraCollision()
    {
		// the wanted Position for the camera calculated from the parent + the direction and the current Zoom and displays it in the editor
		Vector3 wantedPosition = transform.parent.TransformPoint(playerDirection * currentZoomDistance);
		Debug.DrawLine(transform.parent.position, wantedPosition);

		if (Physics.Linecast(transform.parent.position, wantedPosition, out RaycastHit hit)) // if the Linecast hits something
		{
			if (transform.localPosition.magnitude - hit.distance > 1.5f) // if the distance from the camera to the object is greater than 1.5
			{
				if (hideObjectTimer <= 0f && hit.transform.name != "Player") // and not the player and timer lower than 0 hides the object
				{
					// when the stored object is different than the hit object, the old object gets restored
					if (obstacleObject != hit.transform.gameObject && obstacleObject != null) ChangeShadowCastingMode(obstacleObject);
					obstacleObject = hit.transform.gameObject; // the hit object gets stored and depending on the meshrender
					if (obstacleObject.GetComponent<MeshRenderer>()) obstacleObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly; // only displays the shadow
					else if (obstacleObject.GetComponent<SkinnedMeshRenderer>()) obstacleObject.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly; // only displays the shadow
					else return; // In case there is no Mesh
				}

				hideObjectTimer -= Time.deltaTime;
				distance = currentZoomDistance; 
			}
			else // distance from camera to hit object is less than 1.5 
			{
				if (moveTimer < 0) // moves camera closer to player
					distance = Mathf.Clamp(hit.distance * (100 - distanceFromObstacle) / 100, minZoomDistance, maxZoomDistance);

				moveTimer -= Time.deltaTime;
			}
		}
		else // if the linecast hit nothing
		{
			if (obstacleObject != null) // if it hit something in the past and depending on what meshrenderer
                ChangeShadowCastingMode(obstacleObject);

			// resets hideTimer, the moveTimer and set the camera distance to the current Zoom distance
			hideObjectTimer = 0.5f;
			moveTimer = 0.25f;
			distance = currentZoomDistance;
		}
	}

    private void ChangeShadowCastingMode(GameObject gameObject)
    {
		if (gameObject.GetComponent<MeshRenderer>()) gameObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On; // sets everything back to default
		else if (gameObject.GetComponent<SkinnedMeshRenderer>()) gameObject.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.On; // sets everything back to default
		else return; // In case there is no Mesh
	}
}