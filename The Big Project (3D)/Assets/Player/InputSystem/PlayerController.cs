using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance; 
	public PlayerInputConfig Config; 

	private List<IControllable> Posession;
	private SelectionComponent SelComp;

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 

		TryGetComponent<SelectionComponent>(out SelComp);
		Posession = new List<IControllable>(); 
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			SelComp.ActivateSelectionBox();					
		}

		if (context.canceled && context.duration > .4f) //if button was held
		{
			List<IControllable> temp = SelComp.DeactivateSelectionBox();

			if (temp.Count == 0)
				return; 

			//Call OnDeposess for all current posessions
			if (Posession.Count > 0)
				foreach (IControllable po in Posession)
					po.OnDeposess();

			//Copy all cached-found-posessions to current-posession-list
			Posession = temp; 

			//Call OnPosess for all new posessions
			foreach (IControllable po in Posession)
				po.OnPosess(); 
		}

		if (context.canceled && context.duration < .4f) //if button was pressed
		{
			SelComp.DeactivateSelectionBox(); 

			RaycastHit hit = SelComp.QuickSelect();

			//if no posession selected and current posession is not null, move current posession to position
			if (hit.collider.GetComponent<IControllable>() == null && Posession.Count > 0)
			{
				if (Posession.Count > 1)
					foreach (IControllable po in Posession)
					{
						Vector3 destination = hit.point + GameManager.Instance.GetFormationPosition(); 
						po.OnMoveMultiple(destination);
					}
				else
					Posession[0].OnMove(hit.point); 
			}
			else if (hit.collider.GetComponent<IControllable>() != null) //if IControllable selected, change this to current posession
			{
				if(Posession.Count > 0)
				{
					foreach(IControllable po in Posession)					
						po.OnDeposess();

					Posession = new List<IControllable>(); 				
				}

				Posession.Add(hit.collider.GetComponent<IControllable>());
				Posession[0].OnPosess(); 
			}
		}			
	}	

	public void OnCameraMoveInput(InputAction.CallbackContext context)
	{
		Vector2 move = context.action.ReadValue<Vector2>();
		GetComponent<CameraController>().RecieveMoveInput(move);
	}

	private bool isLocked = true;
	public void OnCameraRotInputLock(InputAction.CallbackContext context)
	{
		if (context.performed)
			isLocked = false;
		else if (context.canceled)
			isLocked = true; 
	}

	public void OnCameraRotInput(InputAction.CallbackContext context)
	{
		if (isLocked)
		{
			GetComponent<CameraController>().RecieveRotInput(Vector2.zero);
			return; 
		}

		Vector2 rot = context.action.ReadValue<Vector2>();
		GetComponent<CameraController>().RecieveRotInput(rot); 
	}
}
