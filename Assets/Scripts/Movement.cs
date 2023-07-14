using UnityEngine;

using Math = System.Math;

public class Movement : MonoBehaviour
{
	public CharacterController characterController;
	public KeyCode sprintKey;

	private float speed;

	public float walkSpeed;
	public float sprintSpeed;

	public float staminaInterval;

	public float gravity = -9.81F;
	public float jumpStrength;

	public Transform groundCheck;
	public float groundCheckDistance = 0.12F;
	public LayerMask groundMask;

	private bool isGrounded;

	private Vector3 fallingVelocity;

	// Update is called once per frame
	void Update()
	{
		float stamina = GameManager.instance.stamina;

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 direction = transform.right * x + transform.forward * z;

		if (stamina > 0 && Input.GetKey(sprintKey))
		{
			if (stamina > 0.01f) speed = sprintSpeed;

			stamina -= Time.deltaTime * staminaInterval;
		}
		else
		{
			speed = walkSpeed;

			stamina += Time.deltaTime * staminaInterval;
		}

		GameManager.instance.stamina = Mathf.Clamp01(stamina);

		GameManager.instance.staminaBar.fillAmount = GameManager.instance.stamina / GameManager.instance.maxStamina;

		characterController.Move(direction * speed * Time.deltaTime);

		if (Input.GetButton("Jump") && isGrounded)
		{
			fallingVelocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);

			characterController.Move(fallingVelocity);
		}

		isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

		fallingVelocity.y += gravity * Time.deltaTime;

		if (isGrounded)
		{
			fallingVelocity.y = 0;
		}

		// Terminal velocity = 55.5555m/s.
		// Minimum is 0 and maximum is 55.5555m/s.
		// Gravity is negative so min = -55.5555m/s and max = 0.
		fallingVelocity.y = Math.Clamp(fallingVelocity.y, -55f / 99f * 100, 0);

		characterController.Move(fallingVelocity * Time.deltaTime);
	}
}
