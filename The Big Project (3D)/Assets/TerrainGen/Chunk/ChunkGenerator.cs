using UnityEngine;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour
{
	[Header("Mapgeneration")]
	[SerializeField]
	private float ChunkRadius = 16;
	[SerializeField]
	private ChunkBase[] ChunkTypes;

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
		PlaceChunkBuildings();		
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
				float steepestSlopeAngle = GetHighestSlopeAngle(currentPos, ChunkRadius);
				
				Chunks[x, y].Radius = ChunkRadius;
				Chunks[x, y].Position = currentPos;

				if (WaterSurfaceTransform != null && currentPos.y > WaterSurfaceTransform.position.y)
				{
					List<ChunkBase> chunksByChance = new List<ChunkBase>();

					//Check if slopeangle is to steep for some chunks
					foreach (ChunkBase cb in ChunkTypes)
					{
						if (steepestSlopeAngle <= cb.MaxSlopeForSpawning)
							chunksByChance.Add(cb);
					}

					if (chunksByChance.Count == 1)
					{
						Chunks[x, y].ChunkInfo = chunksByChance[0];
					}
					else if (chunksByChance.Count > 1)
					{
						SortByChance(chunksByChance);

						//Spawn chunks with lowest spawn chance first
						float rand = Random.value;
						foreach (ChunkBase cb in chunksByChance)
						{
							float chance = cb.SpawnChance;
							chance /= 100;

							if (rand < chance)
							{
								Chunks[x, y].ChunkInfo = cb;
								break;
							}
						}
					}
				}

				currentPos.x += ChunkRadius * 2;
			}

			currentPos.z -= ChunkRadius * 2;
			currentPos.x = startPos.x;
		}
	}

	void PlaceChunkBuildings()
	{
		PrefabGridGenerator generator;

		if (GetComponent<PrefabGridGenerator>())
			generator = GetComponent<PrefabGridGenerator>();
		else
		{
			Debug.LogError("PrefabGridGenerator script missing");
			return;
		}

		foreach (Chunk c in Chunks)
		{
			generator.GeneratePrefabs(c.Position, c.Radius, c.ChunkInfo);
		}
	}

	Vector3 GetTerrainHeight(Vector3 position)
	{
		Vector3 newPosition = position;

		RaycastHit hit;
		if (Physics.Raycast(new Vector3(position.x, 1000, position.z), -Vector3.up, out hit, Mathf.Infinity))
		{
			newPosition.y = hit.point.y;
		}

		return newPosition;
	}

	float GetHighestSlopeAngle(Vector3 Position, float ChunkRadius)
	{
		float currentMaxSlopeAngle = 0;

		//Get positions of all directions
		Vector3 eastSlope = Position + Vector3.right * ChunkRadius;
		eastSlope = GetTerrainHeight(eastSlope);
		Vector3 westSlope = Position - Vector3.right * ChunkRadius;
		westSlope = GetTerrainHeight(westSlope);
		Vector3 northSlope = Position + Vector3.forward * ChunkRadius;
		northSlope = GetTerrainHeight(northSlope);
		Vector3 southSlope = Position - Vector3.forward * ChunkRadius;
		southSlope = GetTerrainHeight(southSlope);

		//Get all angles from all directions
		float eastDeltaX = Mathf.Abs(eastSlope.x - Position.x);
		float eastDeltaY = Mathf.Abs(eastSlope.y - Position.y);
		float eastAngle = Mathf.Atan2(eastDeltaY, eastDeltaX);
		float westDeltaX = Mathf.Abs(westSlope.x - Position.x);
		float westDeltaY = Mathf.Abs(westSlope.y - Position.y);
		float westAngle = Mathf.Atan2(westDeltaY, westDeltaX);

		float northDeltaZ = Mathf.Abs(northSlope.z - Position.z);
		float northDeltaY = Mathf.Abs(northSlope.y - Position.y);
		float northAngle = Mathf.Atan2(northDeltaY, northDeltaZ);
		float southDeltaZ = Mathf.Abs(southSlope.z - Position.z);
		float southDeltaY = Mathf.Abs(southSlope.y - Position.y);
		float southAngle = Mathf.Atan2(southDeltaY, southDeltaZ);

		//Get the steepest angle
		currentMaxSlopeAngle = eastAngle;
		currentMaxSlopeAngle = currentMaxSlopeAngle > westAngle ? currentMaxSlopeAngle : westAngle;
		currentMaxSlopeAngle = currentMaxSlopeAngle > northAngle ? currentMaxSlopeAngle : northAngle;
		currentMaxSlopeAngle = currentMaxSlopeAngle > southAngle ? currentMaxSlopeAngle : southAngle;

		return currentMaxSlopeAngle * Mathf.Rad2Deg;
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return;

		foreach (Chunk c in Chunks)
		{
			if (c.ChunkInfo != null)
				Gizmos.color = c.ChunkInfo.DebugColor;
			else
				Gizmos.color = Color.grey;

			Gizmos.DrawCube(c.Position, Vector3.one * c.Radius * 2);
		}
	}

	struct Chunk
	{
		public Vector3 Position;
		public float Radius;
		public ChunkBase ChunkInfo;
	}

	private void SortByChance(List<ChunkBase> ChunkBases)
	{
		bool itemMoved = false;
		do
		{
			itemMoved = false;
			for (int i = 0; i < ChunkBases.Count - 1; i++)
			{
				if (ChunkBases[i].SpawnChance > ChunkBases[i + 1].SpawnChance)
				{
					ChunkBase lowerValue = ChunkBases[i + 1];
					ChunkBases[i + 1] = ChunkBases[i];
					ChunkBases[i] = lowerValue;
					itemMoved = true;
				}
			}
		} while (itemMoved);
	}
}
