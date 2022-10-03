using UnityEngine;
using UnityEngine.AI; 

public class PlayerCharacter : MonoBehaviour, IControllable
{
	private NavMeshAgent Agent;

	private void Awake()
	{
		Agent = GetComponent<NavMeshAgent>();
	}

	public void OnMove(Vector3 destination)
	{
		Agent.SetDestination(destination);
	}
	public void OnMoveMultiple(Vector3 destination)
	{
		Agent.SetDestination(destination); 
	}

	public void OnPosess()
	{
		print("Posessed: " + name);
		if (transform.Find("Plane"))
			transform.Find("Plane").gameObject.SetActive(true); 
	}

	public void OnDeposess()
	{
		print("Deposessed: " + name);
		if (transform.Find("Plane"))
			transform.Find("Plane").gameObject.SetActive(false);
	}

}
