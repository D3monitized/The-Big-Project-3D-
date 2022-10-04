using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float MovementSpeed = 2;
	public float Sensitivity = 100;

    private Transform CamTransform;

	private Vector2 MoveInput;
	private Vector2 RotInput;

	private Transform Pivot;

	public void RecieveMoveInput(Vector2 moveInput) => MoveInput = moveInput;	
	public void RecieveRotInput(Vector2 rotInput) => RotInput = rotInput;

	private void Awake()
	{
		CamTransform = Camera.main.transform;
		Pivot = new GameObject().transform;
		Pivot.name = "PlayerCamera_Pivot"; 
		Pivot.position = CamTransform.position + new Vector3(0, -10, 15);
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

	private Vector3 FindGround()
	{
		Ray ray = Camera.main.ScreenPointToRay(Vector3.zero);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			return hit.point; 
		else
			return new Vector3(); 
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
