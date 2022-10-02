using UnityEngine;
using UnityEngine.AI; 

public class PlayerCharacter : MonoBehaviour, IControllable
{
	private NavMeshAgent Agent;
	private Vector3 TargetDestination;

	private void Start()
	{
		Agent = GetComponent<NavMeshAgent>();		
	}

	private void Update()
	{
		print(Agent.pathPending);
		if(TargetDestination != null)
			Agent.SetDestination(TargetDestination); 
	}

	public void OnWalk(Vector3 _position)
	{
		NavMeshHit nHit;
		if(NavMesh.SamplePosition(_position, out nHit, 5, 0))
			TargetDestination = nHit.position; 
		
		//Agent.SetDestination(_position);

		print("OnWalk"); 
	}

	public void OnPossess()
	{
		print("Now Possessing: " + name); 
	}
	public void OnDepossess()
	{
		print("No longer Possessing: " + name);
	}	
}
