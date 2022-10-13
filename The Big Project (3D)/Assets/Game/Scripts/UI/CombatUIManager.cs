using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatUIManager : MonoBehaviour
{
	public static CombatUIManager Instance;

	[SerializeField]
	private Button EndTurnButton;
	[SerializeField]
	private GameObject PortraitPrefab;
	[SerializeField]
	private GameObject ActionPointPrefab; 

	private List<GameObject> Portraits = new List<GameObject>();

	public void OnPlayerTurn()
	{
		EndTurnButton.gameObject.SetActive(true);
		UpdatePortraits();

		if (!UpdateActionPointBar())
			CreateActionPoints(); 
	}

	public void OnEnemyTurn()
	{
		EndTurnButton.gameObject.SetActive(false);
		UpdatePortraits();

		if (!UpdateActionPointBar())
			CreateActionPoints();
	}

	public void RecieveCombatantInfo(ref List<CombatantBase> combatants)
	{
		for (int i = 0; i < combatants.Count; i++)
		{
			GameObject portrait = Instantiate(PortraitPrefab);
			Portraits.Add(portrait); //Instantiate portrait
			Portraits[Portraits.Count - 1].GetComponent<Portrait>().OnInstantiate(combatants[i], combatants[i].GetInstanceID()); //Give portrait ref to combatant
		}
	}

	public void RemovePortrait(int combatantID)
	{
		foreach (GameObject go in Portraits)
		{
			if (go.GetComponent<Portrait>().CombatantID == combatantID)
				Destroy(go);
		}
	}

	public void OnCombatOver()
	{
		Animator animator;
		TryGetComponent<Animator>(out animator);

		if (animator)
		{
			animator.SetTrigger("OnDisable");
		}

		MouseUIComponent.Instance.ChangeCursorToDefaultSprite();
		MouseUIComponent.Instance.DeactivateMovementCost(); 

		Destroy(gameObject, 1.1f);
	}

	public void UpdatePortraits()
	{
		for (int i = 0; i < CombatManager.Instance.Combatants.Count; i++)
		{
			foreach (GameObject go in Portraits)
			{
				if (go.GetComponent<Portrait>().CombatantID == CombatManager.Instance.Combatants[i].GetInstanceID())
					go.GetComponent<Portrait>().OnCombatantUpdated(CombatManager.Instance.Combatants[i]);
			}
		}
	}

	private List<GameObject> aps = new List<GameObject>();

	public bool UpdateActionPointBar()
	{
		if (aps.Count == 0)
			return false; 

		int currentActionPoints = CombatManager.Instance.CurrentCombatant.CurrentActionPoints;

		for (int i = 0; i < aps.Count; i++)
		{
			if (i < currentActionPoints)
				aps[i].GetComponent<RawImage>().color = Color.green;
			else
				aps[i].GetComponent<RawImage>().color = Color.red; 
		}

		return true; 
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}

	private void OnEnable()
	{
		EndTurnButton.onClick.AddListener(delegate { CombatManager.Instance.OnNextTurn(); });		
	}	

	public void CreateActionPoints()
	{
		int actionPoints = CombatManager.Instance.CurrentCombatant.Stats.GetActionPoints();
		int currentActionPoints = CombatManager.Instance.CurrentCombatant.CurrentActionPoints;

		for (int i = 0; i < actionPoints; i++)
		{
			GameObject ap = Instantiate(ActionPointPrefab);
			ap.transform.SetParent(transform.Find("Panel_ActionPoints"));

			if (i <= currentActionPoints)
				ap.GetComponent<RawImage>().color = Color.green;
			else
				ap.GetComponent<RawImage>().color = Color.red;

			aps.Add(ap);
		}
	}

	private void OnDisable()
	{
	}
}
