using UnityEngine;

public class PlayerManager : MonoBehaviour, IControllable
{	
	public void OnMove(Vector3 destination)
	{
		if (!Character)
			return;

		Character.OnMove(destination); 
	}

	public void OnAttack(CombatantBase _target)
	{
		//Check if current character can attack
		PlayerCharacter.FailType failType = Character.CanAttack(_target); 

		if(failType == PlayerCharacter.FailType.Distance)
		{
			//Move to target and attack
			Character.Agent.stoppingDistance = Character.Stats.GetAttackRange(); 
			Character.OnMove(_target.transform.position);
			target = _target;
			MoveAttack = true; 
		}
		else if (failType == PlayerCharacter.FailType.None)
		{
			Character.Attack(_target); 
		}
	}

	public void OnPosess()
	{
		if (transform.Find("Plane"))
			transform.Find("Plane").gameObject.SetActive(true);
	}

	public void OnDeposess()
	{
		if (transform.Find("Plane"))
			transform.Find("Plane").gameObject.SetActive(false);
	}


	private PlayerCharacter Character;
	private CombatantBase target;
	private bool MoveAttack = false; 

	private void Awake()
	{
		TryGetComponent<PlayerCharacter>(out Character);
	}

	private void Update()
	{
		if (MoveAttack)
			CheckIfCanAttack(); 
	}

	private void CheckIfCanAttack()
	{
		if (Vector3.Distance(transform.position, target.transform.position) <= Character.Stats.GetAttackRange())
		{
			Character.Attack(target);
			Character.Agent.destination = Character.transform.position; 
			MoveAttack = false; 
		}
	}
}
