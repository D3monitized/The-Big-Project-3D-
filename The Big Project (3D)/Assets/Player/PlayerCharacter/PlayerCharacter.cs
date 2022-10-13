using UnityEngine;
using UnityEngine.AI; 

public class PlayerCharacter : CombatantBase
{
	//Stats
	public override void Attack(CombatantBase target)
	{
		base.Attack(target);
		if (CanAttack(target) != FailType.None)
			return;

		AttackWeaponDamage(target, Random.Range(22, 32));

		if (CombatUIManager.Instance)
			CombatUIManager.Instance.UpdateActionPointBar();		
	}

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount);
		print(name + ": " + "Ouch");
	}

	public void OnMove(Vector3 destination)
	{
		if (!Agent)
			return;

		switch (GameManager.Instance.CurrentGameState)
		{
			case GameManager.GameState.Freemode: OnFreeMove(destination); break;
			case GameManager.GameState.Combatmode: OnCombatMove(destination); break; 
		}

		if(CombatUIManager.Instance)
			CombatUIManager.Instance.UpdateActionPointBar();	
	}

	public FailType CanAttack(CombatantBase target)
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);

		if (distance > Stats.GetAttackRange())
			return FailType.Distance;
		else if (CurrentActionPoints < Stats.GetMeleeAttackCost())
			return FailType.ActionPoints;
		else
			return FailType.None; 	
	}

	public NavMeshAgent Agent;

	private void Awake()
	{
		TryGetComponent<NavMeshAgent>(out Agent); 
	}

	private void OnFreeMove(Vector3 destination) => Agent.SetDestination(destination); 

	private void OnCombatMove(Vector3 destination)
	{
		if (CombatManager.Instance.CurrentCombatant != this)
			return;

		print("OnCombatMove"); 

		float distance = Vector3.Distance(transform.position, destination);
		int cost = Mathf.RoundToInt(distance / Stats.GetMoveCostPerDistance());

		if (cost <= CurrentActionPoints)
		{
			Agent.SetDestination(destination);
			CurrentActionPoints -= cost;
		}
	}

	private void AttackWeaponDamage(CombatantBase target, int damage)
	{
		target.TakeDamage(damage);

		CurrentActionPoints -= Stats.GetMeleeAttackCost();

		print("Have at you: " + target.name); 
	}

	public enum FailType
	{
		Distance, ActionPoints, None
	}
}
