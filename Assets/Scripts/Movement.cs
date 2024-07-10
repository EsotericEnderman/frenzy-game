using UnityEngine;

public class Movement : MonoBehaviour
{
	public CharacterController characterController;
	public KeyCode sprintKey = KeyCode.LeftControl;
	public KeyCode jumpKey = KeyCode.Space;

	public float walkSpeedPerSecond = 6.6F;
	public float sprintSpeedPerSecond = 10F;

	public float staminaDecreasePerSecond = 10F;
	public float staminaRecoveryPerSecond = 5F;

	public float gravityPerSecondSquared = -9.81F;
	public float jumpHeight = 4000F;

	public Transform groundCheckTransform;
	public float groundCheckDistance = 0.12F;
	public LayerMask groundMask;

	public float maxStamina = 100F;
	public float minStamina = 0F;
	public float stamina;

	private Vector3 velocity = new();
	public bool isOnGround;

	void Start()
	{
		stamina = maxStamina;
	}

	void Update()
	{
		float speedPerSecond;

		if (stamina > 0 && Input.GetKey(sprintKey))
		{
			speedPerSecond = sprintSpeedPerSecond;

			stamina -= Time.deltaTime * staminaDecreasePerSecond;
		}
		else
		{
			speedPerSecond = walkSpeedPerSecond;

			stamina += Time.deltaTime * staminaRecoveryPerSecond;
		}

		stamina = Mathf.Clamp(stamina, minStamina, maxStamina);

		GameManager.instance.staminaBar.fillAmount = stamina / maxStamina;

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 direction = transform.right * x + transform.forward * z;

		characterController.Move(direction * speedPerSecond * Time.deltaTime);

		isOnGround = Physics.CheckSphere(groundCheckTransform.position, groundCheckDistance, groundMask);

		if (Input.GetKey(jumpKey) && isOnGround)
		{
			velocity.y += Mathf.Sqrt(jumpHeight * -3.0F * gravityPerSecondSquared);
		}

		velocity.y += gravityPerSecondSquared * Time.deltaTime;

		if (isOnGround)
		{
			velocity.y = 0;
		}

		characterController.Move(velocity * Time.deltaTime);
	}
}
