using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerController : MonoBehaviour
{
	private List<IControllable> Posession;
	private SelectionComponent SelComp;

	private void Awake()
	{
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
}
