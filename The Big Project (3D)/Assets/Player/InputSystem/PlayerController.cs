using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }
	public PlayerInputConfig Config;

	private List<IControllable> Posession;
	private CameraController CamController;
	private PlayerInput PInput; 
	
	public void OnGameStateSwitched(GameManager.GameState state)
	{
		switch (state)
		{
			case GameManager.GameState.Freemode: PInput.SwitchCurrentActionMap("Freemode"); break; 
			case GameManager.GameState.Combatmode: PInput.SwitchCurrentActionMap("Combatmode"); break; 
		}
	}

	#region Freemode/Combatmode
	public void OnCameraMoveInput(InputAction.CallbackContext context)
	{
		if (!CamController)
			return; 

		Vector2 move = context.action.ReadValue<Vector2>();
		CamController.RecieveMoveInput(move);
	}

	private bool isLocked = true;
	public void OnCameraRotInputLock(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			isLocked = false;

			MouseUIComponent temp;
			TryGetComponent<MouseUIComponent>(out temp);
			if (temp)
				temp.ChangeCursorToPanSprite(); 
		}
		else if (context.canceled)
		{
			isLocked = true;

			MouseUIComponent temp;
			TryGetComponent<MouseUIComponent>(out temp);
			if (temp)
				temp.ChangeCursorToDefaultSprite();
		}
	}

	public void OnCameraRotInput(InputAction.CallbackContext context)
	{
		if (!CamController)
			return; 

		if (isLocked)
		{
			CamController.RecieveRotInput(Vector2.zero);
			return; 
		}

		Vector2 rot = context.action.ReadValue<Vector2>();
		CamController.RecieveRotInput(rot); 
	}

	public void OnTemp(InputAction.CallbackContext context)
	{
		if (!context.canceled)
			return; 

		switch (GameManager.Instance.CurrentGameState) 
		{
			case GameManager.GameState.Freemode: GameManager.Instance.SwitchGamestateCombatmode(); break;
			case GameManager.GameState.Combatmode: GameManager.Instance.SwitchGamestateFreemode(); break; 
		}		
	}
	#endregion

	#region Freemode
	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			MouseUIComponent.Instance.ActivateSelectionBox();
		}

		if (context.canceled && context.duration > .4f) //if button was held
		{
			List<IControllable> temp = MouseUIComponent.Instance.DeactivateSelectionBox(); 

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
			MouseUIComponent.Instance.DeactivateSelectionBox();

			RaycastHit hit = MouseUIComponent.Instance.QuickSelect(); 
			IControllable temp;

			//If character was targeted -> Posess that character
			if (hit.collider.TryGetComponent<IControllable>(out temp))
			{
				DeposessAll();

				Posession.Add(temp);
				Posession[0].OnPosess();
			}
			else //If no character was targeted -> All posessions go to point
			{
				if (Posession.Count > 0)
					foreach (IControllable po in Posession)
					{
						Vector3 destination = hit.point + GameManager.Instance.GetFormationPosition();
						po.OnMove(destination);
					}
			}
		}
	}	
	#endregion

	#region Combatmode
	public void OnTarget(InputAction.CallbackContext context)
	{
		if (!context.performed)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if(hit.collider.GetComponent<CombatantBase>() as EnemyCharacter)
			{
				if (CombatManager.Instance.CurrentCombatant as PlayerCharacter)
				{
					PlayerManager current;
					current = CombatManager.Instance.CurrentCombatant.GetComponent<PlayerManager>();
					current.OnAttack(hit.collider.GetComponent<CombatantBase>()); 
				}
			}
			else
			{
				if(CombatManager.Instance.CurrentCombatant as PlayerCharacter)
				{
					PlayerCharacter current;
					current = CombatManager.Instance.CurrentCombatant as PlayerCharacter;
					current.OnMove(hit.point); 
				}
			}
		}
	}
	#endregion

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		TryGetComponent<CameraController>(out CamController);
		Posession = new List<IControllable>();
	}

	private void DeposessAll()
	{
		foreach(IControllable po in Posession)
		{
			po.OnDeposess(); 
		}

		Posession = new List<IControllable>(); 
	}

	private void OnEnable()
	{
		TryGetComponent<PlayerInput>(out PInput);

		if(PInput)
			PInput.actions.Enable(); 
	}

	private void OnDisable()
	{
		if (PInput)
			PInput.actions.Disable();
	}
}
