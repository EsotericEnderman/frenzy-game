using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public Image staminaBar;
	public float maxStamina;

	public float stamina;

	void Awake()
	{
		instance = this;   
	}
}
