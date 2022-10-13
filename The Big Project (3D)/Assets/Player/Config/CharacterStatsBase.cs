using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Gameplay/Character/CharacterStats")]
public class CharacterStatsBase : ScriptableObject
{
	public int GetInitiative() { return Initative; } 
	public int GetHealth() { return Health; } 
	public int GetActionPoints() { return ActionPoints; }
	public int GetMeleeAttackCost() { return MeleeAttackCost; }
	public float GetAttackRange() { return AttackRange; }
	public float GetMoveCostPerDistance() { return MoveCostPerDistance; }

	[SerializeField]
	private int Initative;
	[SerializeField]
	private int Health;
	[SerializeField]
	private int ActionPoints;
	[SerializeField]
	private int MeleeAttackCost; 
	[SerializeField]
	private float AttackRange;
	[SerializeField]
	private float MoveCostPerDistance; 


	private void Awake()
	{
		Initative = Random.Range(0, 20); 
	}
}
