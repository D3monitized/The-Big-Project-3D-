using UnityEngine;
using UnityEngine.AI; 

public class TestScript : MonoBehaviour
{
	[SerializeField]
	private Transform MovePosition;
	private NavMeshAgent Agent;

	private void Awake()
	{
		Agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		Agent.SetDestination(MovePosition.position);
	}
}
