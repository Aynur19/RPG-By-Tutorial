using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	public LayerMask movementMask;

	private Camera mainCamera;
	private PlayerMotor motor;

	private void Start()
	{
		mainCamera = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}

	private void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementMask))
			{
				motor.MoveToPoint(hit.point);
			}
		}

		//if (Input.GetMouseButtonDown(1))
		//{
		//	Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		//	RaycastHit hit;

		//	if (Physics.Raycast(ray, out hit, 100))
		//	{
		//		Interactable interactable = hit.collider.GetComponent<Interactable>();
		//		if (interactable != null)
		//		{
		//			SetFocus(interactable);
		//		}
		//	}
		//}
	}
}
