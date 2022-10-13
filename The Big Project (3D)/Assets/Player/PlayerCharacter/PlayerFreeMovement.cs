using UnityEngine;
using UnityEngine.AI; 

public class PlayerFreeMovement : MonoBehaviour
{
	private NavMeshAgent Agent;

	private void Awake()
	{
		TryGetComponent<NavMeshAgent>(out Agent);
	}

	public void OnMove(Vector3 destination)
	{
		if (!Agent)
			return;

		Agent.SetDestination(destination); 
	}
}
