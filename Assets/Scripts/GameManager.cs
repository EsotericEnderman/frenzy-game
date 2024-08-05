using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public Image staminaBar;

	void Awake()
	{
		instance = this;
	}
}
