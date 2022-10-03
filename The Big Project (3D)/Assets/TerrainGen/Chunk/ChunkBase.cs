using UnityEngine;

[CreateAssetMenu(fileName = "NewChunkBase", menuName = "TerrainGen/New Chunk Base")]
public class ChunkBase : ScriptableObject
{
	[Header("Settings")]
	[Range(1, 100)]
	public int SpawnChance = 1;
	[Range(1, 90)]
	public int MaxSlopeForSpawning = 1; 

	[Header("Components")]
	public GameObject[] Prefabs;
	public Color DebugColor; 
}
