using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPartyConfig", menuName = "Gameplay/Party/PartyConfig")]
public class PartyBase : ScriptableObject
{
    [SerializeField]
    public List<GameObject> PartyMembers;
    
    [SerializeField]
    private Vector3[] Offsets;

    private int n = 0; 

    public Vector3 GetFormationPosition()
	{
        int current = n; 
        n = n == Offsets.Length - 1 ? 0 : n + 1;
        return Offsets[current];
    }
}
