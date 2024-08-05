using Unity.VisualScripting;
using UnityEngine;

public class Pickups : MonoBehaviour
{

	private RaycastHit hit;

	private readonly float interactionDistance = 5.0F;

	void Update()
	{
		if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
		{
			if (hit.transform.tag == "Food" && Input.GetMouseButton((int)MouseButton.Left))
			{
				GameObject objectHit = hit.transform.gameObject;
				Destroy(objectHit);
			}
		}
	}
}
