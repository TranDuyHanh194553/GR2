using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Vector2 touchStart1, touchStart2;
	// private float initialZoom, zoomSpeed = 0.1f;

	void Start()
	{
		// Set the camera to perspective projection when the script starts
		Camera.main.orthographic = false;
	}

	void Update()
	{
		if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}

		HandleCameraInput();
	}

	void HandleCameraInput()
	{
		// if (Input.touchCount == 2)
		// {
		// 	Touch touch1 = Input.GetTouch(0);
		// 	Touch touch2 = Input.GetTouch(1);

		// 	switch (touch1.phase)
		// 	{
		// 		case TouchPhase.Began:
		// 			touchStart1 = touch1.position;
		// 			touchStart2 = touch2.position;
		// 			initialZoom = Camera.main.fieldOfView;
		// 			break;

		// 		case TouchPhase.Moved:
		// 			Vector2 currentTouch1 = touch1.position;
		// 			Vector2 currentTouch2 = touch2.position;

		// 			float currentDistance = Vector2.Distance(currentTouch1, currentTouch2);
		// 			float previousDistance = Vector2.Distance(touchStart1, touchStart2);

		// 			float deltaDistance = previousDistance - currentDistance;

		// 			float zoomFactor = deltaDistance * zoomSpeed;
		// 			Camera.main.fieldOfView = Mathf.Clamp(initialZoom + zoomFactor, 1f, Mathf.Infinity);
		// 			break;
		// 	}
		// }
		// else 
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					break;

				case TouchPhase.Moved:
					Vector2 direction = touch.deltaPosition;
					Camera.main.transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime;
					break;
			}
		}
	}
}