using UnityEngine;
using UnityEngine.UI; 

public class Portrait : MonoBehaviour
{
	[HideInInspector]
	public int CombatantID { get; private set; } 
	
	public void OnCombatantUpdated(CombatantBase combatant)
	{
		transform.Find("Text_Health").GetComponent<Text>().text = "HP: " + combatant.CurrentHealth.ToString();
	}

	public void OnInstantiate(CombatantBase combatant, int instanceID)
	{
		CombatantID = instanceID; 
		
		transform.SetParent(GameObject.Find("Panel_PortraitLayoutGroup").transform);
		transform.Find("Text_Name").GetComponent<Text>().text = combatant.name;
		transform.Find("Text_Initiative").GetComponent<Text>().text = combatant.Stats.GetInitiative().ToString();
		transform.Find("Text_Health").GetComponent<Text>().text = "HP: " + combatant.CurrentHealth.ToString();
	}
}
