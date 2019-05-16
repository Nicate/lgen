using UnityEngine;

public class Viewer : MonoBehaviour {
	[Header("Focus")]
	public GameObject focus;

	[Header("Settings")]
	public float moveSensitivity = 1.0f;
	public float rotateSensitivity = 1.0f;
	public float zoomSensitivity = 1.0f;

	public float startStrafe = 0.0f;
	public float startClimb = 0.0f;
	public float startAdvance = 0.0f;
	public float startPitch = 0.0f;
	public float startTilt = 0.0f;
	public float startZoom = 0.0f;

	public float minimumStrafe = 0.0f;
	public float maximumStrafe = 0.0f;

	public float minimumClimb = 0.0f;
	public float maximumClimb = 0.0f;

	public float minimumAdvance = 0.0f;
	public float maximumAdvance = 0.0f;

	public float minimumPitch = 0.0f;
	public float maximumPitch = 0.0f;
	
	public float minimumZoom = 0.0f;
	public float maximumZoom = 0.0f;
	
	[Header("Input")]
	public MouseButton strafeAndAdvanceMouseButton = MouseButton.Left;
	public MouseButton climbMouseButton = MouseButton.Middle;
	public MouseButton pitchAndTiltMouseButton = MouseButton.Right;
	public string zoomAxis = "Mouse ScrollWheel";
	public KeyCode resetKey = KeyCode.R;
	public bool allowReset = true;


	public enum MouseButton {
		None = -1,
		Left = 0,
		Right = 1,
		Middle = 2
	}

	
	private Camera viewer;

	private GameObject rig;
	private GameObject boom;
	
	private bool moving;
	private bool scaling; // So bad XD
	private bool rotating;
	private Vector3 mousePosition;

	private float strafe;
	private float climb;
	private float advance;

	private float pitch;
	private float tilt;

	private float zoom;


	private void Awake() {
		rig = new GameObject("Rig");
		rig.transform.SetParent(transform.parent, false);

		boom = new GameObject("Boom");
		boom.transform.SetParent(rig.transform, false);
		
		viewer = GetComponent<Camera>();
		viewer.transform.SetParent(boom.transform, false);

		viewer.transform.localPosition = Vector3.zero;
		viewer.transform.localRotation = Quaternion.identity;
		viewer.transform.localScale = Vector3.one;
	}
	
	private void Start() {
		moving = false;
		scaling = false;
		rotating = false;
		mousePosition = Input.mousePosition;

		resetPosition();
		resetRotation();
		resetZoom();

		updatePosition();
		updateRotation();
		updateZoom();
	}

	
	private void Update() {
		float distance = Vector3.Distance(focus.transform.position, viewer.transform.position);
		Vector3 delta = (Input.mousePosition - mousePosition) * distance;

		if(moving) {
			Quaternion rotation = Quaternion.Euler(0.0f, tilt, 0.0f);
			Vector3 translation = rotation * (Vector3.left * delta.x + Vector3.back * delta.y) * moveSensitivity;

			strafe += translation.x;
			advance += translation.z;

			strafe = Mathf.Clamp(strafe, minimumStrafe, maximumStrafe);
			advance = Mathf.Clamp(advance, minimumAdvance, maximumAdvance);
		}

		if(scaling) {
			climb -= delta.y * moveSensitivity;

			climb = Mathf.Clamp(climb, minimumClimb, maximumClimb);
		}

		if(rotating) {
			pitch -= delta.y * rotateSensitivity;
			tilt += delta.x * rotateSensitivity;

			pitch = Mathf.Clamp(pitch, minimumPitch, maximumPitch);
			tilt = tilt % 360.0f;
		}

		mousePosition = Input.mousePosition;

		int leftMouseButton = (int) strafeAndAdvanceMouseButton;
		int middleMouseButton = (int) climbMouseButton;
		int rightMouseButton = (int) pitchAndTiltMouseButton;

		if(leftMouseButton >= 0 && Input.GetMouseButtonDown(leftMouseButton)) {
			moving = true;
		}

		if(leftMouseButton >= 0 && Input.GetMouseButtonUp(leftMouseButton)) {
			moving = false;
		}

		if(middleMouseButton >= 0 && Input.GetMouseButtonDown(middleMouseButton)) {
			scaling = true;
		}

		if(middleMouseButton >= 0 && Input.GetMouseButtonUp(middleMouseButton)) {
			scaling = false;
		}

		if(rightMouseButton >= 0 && Input.GetMouseButtonDown(rightMouseButton)) {
			rotating = true;
		}

		if(rightMouseButton >= 0 && Input.GetMouseButtonUp(rightMouseButton)) {
			rotating = false;
		}

		zoom -= Input.GetAxis(zoomAxis) * zoomSensitivity;
		zoom = Mathf.Clamp(zoom, minimumZoom, maximumZoom);

		if(allowReset && Input.GetKeyDown(resetKey)) {
			resetPosition();
			resetRotation();
			resetZoom();
		}

		// These are calculated independent of other components.
		updateRotation();
		updateZoom();
	}
	
	private void LateUpdate() {
		// This is calculated dependent on another component.
		updatePosition();
	}


	private void resetPosition() {
		strafe = startStrafe;
		climb = startClimb;
		advance = startAdvance;
	}

	private void resetRotation() {
		pitch = startPitch;
		tilt = startTilt;
	}
	
	private void resetZoom() {
		zoom = startZoom;
	}


	private void updatePosition() {
		rig.transform.position = focus.transform.position + new Vector3(strafe, climb, advance);
	}
	
	private void updateRotation() {
		boom.transform.localRotation = Quaternion.Euler(pitch, tilt, 0.0f);
	}

	private void updateZoom() {
		float distance = Mathf.Exp(zoom);

		viewer.transform.localPosition = new Vector3(0.0f, 0.0f, -distance);
	}
}