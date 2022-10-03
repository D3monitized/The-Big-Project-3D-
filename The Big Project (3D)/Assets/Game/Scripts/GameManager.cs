using UnityEngine;
using System.Collections.Generic; 

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 
	}

	[SerializeField]
	private PartyBase PartyInfo;

	[HideInInspector]
	public List<GameObject> PartyMembers; 

	private void Start()
	{
		Vector3 offset = new Vector3(); 

		foreach(GameObject go in PartyInfo.PartyMembers)
		{
			PartyMembers.Add(Instantiate(go, Vector3.zero + offset, Quaternion.identity));
			offset.x += 3; 
		}
	}

	public Vector3 GetFormationPosition()
	{
		return PartyInfo.GetFormationPosition(); 
	}
}
