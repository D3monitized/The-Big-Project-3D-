using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance; 

	public float MovementSpeed = 2;
	public float Sensitivity = 100;

    private Transform CamTransform;

	private Vector2 MoveInput;
	private Vector2 RotInput;

	private Transform Pivot;

	public void RecieveMoveInput(Vector2 moveInput) => MoveInput = moveInput;	
	public void RecieveRotInput(Vector2 rotInput) => RotInput = rotInput;

	public void FocusCombatant(CombatantBase combatant)
	{
		Pivot.position = new Vector3(combatant.transform.position.x, Pivot.position.y, combatant.transform.position.z); 
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 

		CreatePivot(); 
	}

	private void CreatePivot()
	{
		CamTransform = Camera.main.transform;
		Pivot = new GameObject().transform;
		Pivot.name = "PlayerCamera_Pivot";
		Pivot.position = CamTransform.position + new Vector3(0, -10, 5);
		CamTransform.SetParent(Pivot);
	}

	private void Start()
	{
		if(PlayerController.Instance.Config != null)
		{
			MovementSpeed = PlayerController.Instance.Config.CameraMovementSpeed;
			Sensitivity = PlayerController.Instance.Config.CameraSensitivity; 
		}
		else
		{
			MovementSpeed = 1;
			Sensitivity = 1; 
		}
	}


	private void Update()
	{
		Move();
		Rotate(); 
	}
	
	private void Move()
	{
		Pivot.position += (Pivot.right * MoveInput.x + Pivot.forward * MoveInput.y) * MovementSpeed * Time.deltaTime; 
	}

	private void Rotate()
	{
		Pivot.Rotate(new Vector3(0, RotInput.x * Sensitivity * Time.deltaTime, 0));		
	}
	

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		if(Pivot != null)
			Gizmos.DrawSphere(Pivot.position, .5f); 
	}
#endif

}
