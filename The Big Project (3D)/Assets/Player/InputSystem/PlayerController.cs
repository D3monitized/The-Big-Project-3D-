using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private IControllable ControlledPawn;
	public void OnInteraction(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
			RaycastHit hit;
			Physics.Raycast(ray, out hit, Mathf.Infinity);


			if (hit.collider.GetComponent<IControllable>() != null)
			{
				GameObject go = hit.collider.gameObject;
				ControlledPawn = go.GetComponent<IControllable>();
				hit.collider.GetComponent<IControllable>().OnPossess();
			}
			else if (ControlledPawn != null)
			{
				ControlledPawn.OnWalk(hit.point);
			}
		}		
	}
}
