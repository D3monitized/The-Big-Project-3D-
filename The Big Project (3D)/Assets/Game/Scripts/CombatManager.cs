using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
	public static CombatManager Instance { get; private set; }	
	public CombatantBase CurrentCombatant { get; private set; }

	[SerializeField]
	private GameObject CombatHUD;
	private GameObject CurrentHUD;

	[HideInInspector]
	public List<CombatantBase> Combatants;

	private int CCIndex;

	private bool Enemies = false;
	private bool Players = false;
	private bool CombatOver = false; 	

	public void OnCombatantDefeated(CombatantBase combatant)
	{
		CombatUIManager.Instance.RemovePortrait(combatant.GetInstanceID());
		Combatants.Remove(combatant);
		Destroy(combatant);
		CheckCombatants(); 
	}

	//Returns random target to EnemyCharacter script
	public CombatantBase GetRandomTarget()
	{
		List<CombatantBase> temp = new List<CombatantBase>();

		foreach (CombatantBase cb in Combatants)
		{
			if (cb as PlayerCharacter)
			{
				temp.Add(cb);
			}
		}

		return temp[Random.Range(0, temp.Count)];
	}

	private void OnCombatStart()
	{
		CombatUIManager.Instance.RecieveCombatantInfo(ref Combatants); 
		OnTurnBegin(); 
	}

	private void OnTurnBegin()
	{
		CurrentCombatant = Combatants[CCIndex];
		CameraController.Instance.FocusCombatant(CurrentCombatant);
		if (CurrentCombatant as EnemyCharacter)
		{
			CurrentHUD.GetComponent<CombatUIManager>().OnEnemyTurn();
			EnemyController.Instance.RecieveCombatant(CurrentCombatant);
		}
		else
			CurrentHUD.GetComponent<CombatUIManager>().OnPlayerTurn(); 
	}

	//Called when player/enemy has ended turn
	public void OnNextTurn()
	{
		if (CombatOver)
			return;

		CCIndex++;
		if (CCIndex > Combatants.Count - 1)
			CCIndex = 0;

		OnTurnBegin();
	}

	//Called from EnemyCharacter script
	public void RecieveEnemyCombatants(CombatantBase temp)
	{
		Combatants.Add(temp);
		Enemies = true;
		if (Enemies && Players)
		{
			SelectionSort(ref Combatants);
			OnCombatStart();
		}
	}

	//Check if there are combatants left from both sides
	private void CheckCombatants()
	{
		bool player = false;
		bool enemy = false; 

		foreach(CombatantBase cb in Combatants)
		{
			if (cb as PlayerCharacter)
				player = true;
			else if (cb as EnemyCharacter)
				enemy = true;

			if (player && enemy)
				break; 			
		}

		if (!player || !enemy) //if there are no enemies/players
		{
			GameManager.Instance.SwitchGamestateFreemode();
			CombatUIManager.Instance.OnCombatOver(); 
			CombatOver = true; 
		}
	}

	#region OnEnable/Disable

	private void Awake() //Singleton setup
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	private void OnEnable()
	{
		GetAllCombatants();
		EnableUI();
		gameObject.AddComponent<EnemyController>();
	}
	
	private void GetAllCombatants(CombatantBase temp = null)
	{
		Combatants = new List<CombatantBase>();

		foreach (GameObject go in GameManager.Instance.PartyMembers)
		{
			if (go.GetComponent<CombatantBase>())
				Combatants.Add(go.GetComponent<CombatantBase>());
		}

		Players = true;

		if (Enemies && Players)
		{
			SelectionSort(ref Combatants);
			OnCombatStart();
		}
	}

	private void EnableUI() 
	{
		CurrentHUD = Instantiate(CombatHUD);
	}

	//Sort combatants after initiative (highest initiative attacks first)
	private void SelectionSort(ref List<CombatantBase> combatants)
	{
		for (var i = 0; i < combatants.Count; i++)
		{
			var min = i;
			for (var j = i + 1; j < combatants.Count; j++)
			{
				if (combatants[min].Stats.GetInitiative() < combatants[j].Stats.GetInitiative())
				{
					min = j;
				}
			}

			if (min != i)
			{
				CombatantBase lowerValue = combatants[min];
				combatants[min] = combatants[i];
				combatants[i] = lowerValue;
			}
		}
	}

	private void OnDisable()
	{
		Destroy(gameObject.GetComponent<EnemyController>());
	}

	#endregion
}
