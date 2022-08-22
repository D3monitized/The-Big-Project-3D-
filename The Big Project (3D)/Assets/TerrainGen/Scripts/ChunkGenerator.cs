using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField]
	private float ChunkRadius = 16;
	[Range(1, 100)] [SerializeField]
	private int VillageSpawnChance = 1; 

	[Header("Components")]
	[SerializeField]
	private Transform MapTerrain;
	[SerializeField]
	private Transform WaterSurfaceTransform; 
	
	private Vector2 MapSize;
	private Vector3 MapCenter; 

	private int AmountOfChunksHor;
	private int AmountOfChunksVer;
	private Chunk[,] Chunks; 

	

	private void Start()
	{
		if (MapTerrain == null || WaterSurfaceTransform == null)
			return; 

		GetMapInfo(); 
		GenerateChunks(); 
	}

	private void GetMapInfo()
	{
		MapSize.x = MapTerrain.GetComponent<Terrain>().terrainData.size.x;
		MapSize.y = MapTerrain.GetComponent<Terrain>().terrainData.size.z;
		MapCenter = new Vector3(MapTerrain.position.x + MapSize.x / 2, MapTerrain.position.y, MapTerrain.position.z + MapSize.y / 2);
	}

	private void GenerateChunks()
	{
		AmountOfChunksHor = Mathf.RoundToInt(MapSize.x / (ChunkRadius * 2));
		AmountOfChunksVer = Mathf.RoundToInt(MapSize.y / (ChunkRadius * 2));
		Chunks = new Chunk[AmountOfChunksHor, AmountOfChunksVer];

		Vector3 startPos = new Vector3(MapCenter.x - MapSize.x / 2 + ChunkRadius, 0, MapCenter.z + MapSize.y / 2 - ChunkRadius);
		Vector3 currentPos = startPos;

		for (int y = 0; y < AmountOfChunksVer; y++)
		{
			for (int x = 0; x < AmountOfChunksHor; x++)
			{
				currentPos = GetTerrainHeight(currentPos); 

				Chunks[x, y].Radius = ChunkRadius;
				Chunks[x, y].Position = currentPos;

				if (currentPos.y <= WaterSurfaceTransform.position.y)
					Chunks[x, y].type = Chunk.ChunkType.None;
				else
				{
					float chance = VillageSpawnChance;
					chance /= 100; 
					float rand = Random.value;				
					
					Chunks[x, y].type = rand < chance ? Chunk.ChunkType.Village : Chunk.ChunkType.Woods; 
				}

				currentPos.x += ChunkRadius * 2; 
			}

			currentPos.z -= ChunkRadius * 2; 
			currentPos.x = startPos.x; 
		}
	}

	Vector3 GetTerrainHeight(Vector3 position)
	{
		Vector3 newPosition = position; 

		RaycastHit hit;
		if(Physics.Raycast(new Vector3(position.x, 1000, position.z), -Vector3.up, out hit, Mathf.Infinity))
		{
			newPosition.y = hit.point.y;
		}

		return newPosition; 
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return; 

		foreach(Chunk c in Chunks)
		{
			if (c.type == Chunk.ChunkType.Village)
				Gizmos.color = Color.cyan;
			else if (c.type == Chunk.ChunkType.Woods)
				Gizmos.color = Color.green;
			else
				Gizmos.color = Color.grey; 

			Gizmos.DrawCube(c.Position, Vector3.one * c.Radius * 2); 
		}
	}

	struct Chunk
	{
		public Vector3 Position;
		public float Radius;
		public ChunkType type; 

		public enum ChunkType
		{
			Village,
			Woods,
			None
		}
	}
}
