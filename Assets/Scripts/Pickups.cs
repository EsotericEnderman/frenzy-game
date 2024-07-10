using UnityEngine;

public class Pickups : MonoBehaviour
{
	RaycastHit hit;

	public float interactDistance;

	void Update()
	{
		if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
		{
			if (hit.transform.tag == "Food" && Input.GetMouseButton(0))
			{
				GameObject objectHit = hit.transform.gameObject;

				Destroy(objectHit);
			}
		}
	}
}
