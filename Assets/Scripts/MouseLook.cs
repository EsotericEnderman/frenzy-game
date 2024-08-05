using UnityEngine;

public class MouseLook : MonoBehaviour
{

	public Transform playerBody;

	private readonly float sensitivity = 1000;

	private float xRotation = 0;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}
