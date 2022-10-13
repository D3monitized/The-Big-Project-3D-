using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

	public void RecieveCombatant(CombatantBase combatant)
	{
		CurrentCombatant = combatant as EnemyCharacter;
		OnTurnStart(); 
	}

	private EnemyCharacter CurrentCombatant;
	
	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 
	}

	private void OnTurnStart()
	{
		//Get a target
		CombatantBase target = CombatManager.Instance.GetRandomTarget();

		//Do logic to decide action (move/attack/use ability) 

		//Attack - temp
		CurrentCombatant.Attack(target); 
		
		StartCoroutine(Wait()); 
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2); 
		CombatManager.Instance.OnNextTurn();
	}
}
