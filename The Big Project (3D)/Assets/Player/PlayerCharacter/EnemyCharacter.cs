using UnityEngine;
using UnityEngine.InputSystem; //temp

public class EnemyCharacter : CombatantBase
{
	public CombatantBase PreviousTarget; //Cached when in combat 

	public override void TakeDamage(int amount)
	{
		base.TakeDamage(amount); 
		print(name + ": " + "Ouch");
	}

	public override void Attack(CombatantBase target) //Change into bool return type for success check
	{
		base.Attack(target);

		//Decide what kind of attack (weaponattack/spell/ability)
		AttackWeapon(target, Random.Range(20, 32)); 
	}

	public void TempPlayerDetection()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 100);

		foreach (Collider col in colliders)
		{
			if (col.GetComponent<PlayerCharacter>())
			{
				GameManager.Instance.SwitchGamestateCombatmode();
				CombatManager.Instance.RecieveEnemyCombatants(this);
				break; 
			}
		}
	}

	private void Update()
	{
		if (Keyboard.current.digit0Key.wasPressedThisFrame)
			TempPlayerDetection(); 
	}

	private void AttackWeapon(CombatantBase target, int damage)
	{
		target.TakeDamage(damage);
		print("Have at you: " + target.name); 
	}
}
