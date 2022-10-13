using UnityEngine;
using System.Collections.Generic; 

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } //Singleton instance

	[HideInInspector]
	public List<GameObject> PartyMembers;

	[HideInInspector]
	public GameState CurrentGameState = GameState.Freemode;
	
	[System.Serializable]
	public enum GameState
	{
		Freemode,
		Combatmode
	}

	public void SwitchGamestateCombatmode() //Switch Gamestate
	{
		if (CurrentGameState == GameState.Combatmode)
			return;

		CurrentGameState = GameState.Combatmode;
		CM.enabled = true; 
		OnGamestateSwitched();
		PlayerController.Instance.OnGameStateSwitched(CurrentGameState); 
	}
	public void SwitchGamestateFreemode()
	{
		if (CurrentGameState == GameState.Freemode)
			return; 

		CurrentGameState = GameState.Freemode;
		CM.enabled = false;
		OnGamestateSwitched();
		PlayerController.Instance.OnGameStateSwitched(CurrentGameState);
	} 

	//Get all party members designated position
	public Vector3 GetFormationPosition()
	{
		return PartyInfo.GetFormationPosition();
	}

	private void Awake()
	{
		//Create singleton
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		TryGetComponent<CombatManager>(out CM); 
	}

	[SerializeField]
	private PartyBase PartyInfo; //Includes character party members with their stats etc.	
	private CombatManager CM; 

	private void Start()
	{
		CreateParty();
	}

	//Creates the party of characters that the player controls
	private void CreateParty()
	{
		Vector3 offset = new Vector3();

		foreach (GameObject go in PartyInfo.PartyMembers)
		{
			PartyMembers.Add(Instantiate(go, Vector3.zero + offset, Quaternion.identity));
			offset.x += 3;
		}
	}

	//Called when gamestate is switched
	private void OnGamestateSwitched()
	{

	}
}
