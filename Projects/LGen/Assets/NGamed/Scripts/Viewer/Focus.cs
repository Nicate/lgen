using UnityEngine;

public class Focus : MonoBehaviour {
	public float moveSensitivity = 1.0f;

	public float startHeight = 0.0f;

	public float minimumHeight = 0.0f;
	public float maximumHeight = 0.0f;


	private float height;

	private bool moving;
	private Vector3 mousePosition;


	private void Awake() {
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	private void Start() {
		moving = false;

		height = startHeight;

		updatePosition();
	}

	
	private void Update() {
		bool leftMouseDown = Input.GetMouseButtonDown(0);
		bool leftMouseUp = Input.GetMouseButtonUp(0);

		if(moving) {
			Vector3 delta = (Input.mousePosition - mousePosition) * moveSensitivity;

			height -= delta.y;

			height = Mathf.Clamp(height, minimumHeight, maximumHeight);

			mousePosition = Input.mousePosition;
		}

		if(leftMouseDown) {
			mousePosition = Input.mousePosition;

			moving = true;
		}

		if(leftMouseUp) {
			mousePosition = Vector3.zero;

			moving = false;
		}

		updatePosition();
	}


	private void updatePosition() {
		transform.localPosition = new Vector3(0.0f, height, 0.0f);
	}
}
