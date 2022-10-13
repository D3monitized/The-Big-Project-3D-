using UnityEngine;

public abstract class CombatantBase : MonoBehaviour
{
	public CharacterStatsBase Stats;

	[HideInInspector]
	public int CurrentHealth;
	[HideInInspector]
	public int CurrentActionPoints;

	private void Start()
	{
		CurrentHealth = Stats.GetHealth();
		CurrentActionPoints = Stats.GetActionPoints(); 
	}

	public virtual void Attack(CombatantBase target)
	{
	}

	public virtual void TakeDamage(int amount)
	{
		CurrentHealth -= amount;

		if (GameManager.Instance.CurrentGameState == GameManager.GameState.Combatmode && CurrentHealth <= 0)
		{
			CombatManager.Instance.OnCombatantDefeated(this);
		}

		if (CombatUIManager.Instance)
			CombatUIManager.Instance.UpdatePortraits(); 
	}
}
