using UnityEngine;
using UnityEngine.AI; 

public class PlayerTurnBasedMovement : MonoBehaviour
{
	private NavMeshAgent Agent;

	private void Awake()
	{
		TryGetComponent<NavMeshAgent>(out Agent); 
	}

	public void OnMove(Vector3 destination)
	{
		if (!Agent || CombatManager.Instance.CurrentCombatant != this)
			return;
		
		Agent.SetDestination(destination); 
	}
}
