using UnityEngine;

public class Movement : MonoBehaviour
{

	public CharacterController characterController;
	public LayerMask groundMask;
	public Transform groundCheckTransform;

	private readonly KeyCode sprintKey = KeyCode.LeftControl;
	private readonly KeyCode jumpKey = KeyCode.Space;

	private readonly float gravityPerSecondSquared = -9.81F;
	private readonly float walkSpeedPerSecond = 6.6F;
	private readonly float sprintSpeedPerSecond = 10F;
	private readonly float maxStamina = 100F;
	private readonly float minStamina = 0F;
	private readonly float staminaDecreasePerSecond = 10F;
	private readonly float staminaRecoveryPerSecond = 5F;
	private readonly float jumpHeight = 4F;
	private readonly float groundCheckDistance = 0.12F;

	private Vector3 velocity = new();
	private float stamina;
	private bool isOnGround = false;

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

		if (isOnGround && !Input.GetKey(jumpKey))
		{
			velocity.y = 0;
		}

		characterController.Move(velocity * Time.deltaTime);
	}
}
