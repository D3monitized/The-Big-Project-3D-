using UnityEngine;
using System.Collections.Generic; 

public class HouseGenerator2 : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField]
	private float HousePlotRadius = 1;
	[SerializeField]
	private int AmountOfHouses = 10; 

	[Header("Components")]
	[SerializeField]
	private GameObject[] HousePrefabs;
	[SerializeField]
	private Transform WaterSurfaceTransform; 

	private Vector3 ChunkPosition; 
	private Vector2 ChunkSize; 

	private HousePlot[,] HousePlots; 
	private int AmountOfHousesHor;
	private int AmountOfHousesVer;

	public void GenerateHouses(Vector3 _chunkPosition, float ChunkRadius)
	{
		ChunkSize.x = ChunkRadius * 2;
		ChunkSize.y = ChunkRadius * 2;
		ChunkPosition = _chunkPosition; 

		GenerateHousePlots(); 
	}

	private void GenerateHousePlots()
	{
		AmountOfHousesHor = Mathf.RoundToInt(ChunkSize.x / (HousePlotRadius * 2));
		AmountOfHousesVer = Mathf.RoundToInt(ChunkSize.y / (HousePlotRadius * 2));
		HousePlots = new HousePlot[AmountOfHousesHor, AmountOfHousesVer];
		
		//Makes sure there aren't more houses than plots or less houses than horizontal rows
		if (AmountOfHouses < AmountOfHousesVer)
			AmountOfHouses = AmountOfHousesVer;
		else if (AmountOfHouses > AmountOfHousesHor * AmountOfHousesVer)
			AmountOfHouses = AmountOfHousesHor * AmountOfHousesVer; 

		Vector3 startPos = new Vector3(ChunkPosition.x - ChunkSize.x / 2 + HousePlotRadius, ChunkPosition.y, ChunkPosition.z + ChunkSize.y / 2 - HousePlotRadius);
		Vector3 currentPos = startPos;
		int amountOfHousesPerRow = Mathf.RoundToInt(AmountOfHouses / AmountOfHousesVer);

		for (int y = 0; y < AmountOfHousesVer; y++)
		{
			for (int i = 0; i < amountOfHousesPerRow; i++)
			{
				int r = Random.Range(0, AmountOfHousesHor);
				if (HousePlots[r, y].Open)
					i--;
				else
					HousePlots[r, y].Open = true; 
			}

			for (int x = 0; x < AmountOfHousesHor; x++)
			{
				HousePlots[x, y].Position = currentPos;
				HousePlots[x, y].Radius = HousePlotRadius;
				
				currentPos.x += HousePlotRadius * 2; 
			}

			currentPos.z -= HousePlotRadius * 2; 
			currentPos.x = startPos.x; 
		}

		BuildHouses(); 
	}

	private void BuildHouses()
	{
		for (int y = 0; y < HousePlots.GetLength(1); y++)
		{
			for (int x = 0; x < HousePlots.GetLength(0); x++)
			{
				if (!HousePlots[x, y].Open)
					continue;

				int randAngle = Random.Range(0, 360);				
				Quaternion rot = GetTerrainSlope(HousePlots[x, y].Position);		
				HousePlots[x, y].Position = GetTerrainHeight(HousePlots[x, y].Position);
				if (WaterSurfaceTransform != null && HousePlots[x, y].Position.y < WaterSurfaceTransform.position.y)
					continue; 
				
				GameObject house = Instantiate(HousePrefabs[0], HousePlots[x, y].Position, rot);
				house.GetComponent<Transform>().RotateAround(house.transform.position, house.transform.up, randAngle); 
			}
		}

		/*foreach(HousePlot housePlot in HousePlots)
		{
			if (!housePlot.Open)
				continue;

			int randAngle = Random.Range(0, 360);
			Quaternion rot = Quaternion.Euler(0, randAngle, 0);
			Vector3 position = GetTerrainHeight(housePlot.Position);

			Instantiate(HousePrefabs[0], housePlot.Position, rot);
		}*/
	}

	private Vector3 GetTerrainHeight(Vector3 position)
	{
		Vector3 newPosition = position;

		RaycastHit hit;
		if (Physics.Raycast(new Vector3(position.x, 1000, position.z), -Vector3.up, out hit, Mathf.Infinity))
		{
			newPosition.y = hit.point.y;
		}

		return newPosition;
	}

	private Quaternion GetTerrainSlope(Vector3 position)
	{
		Quaternion newRotation = new Quaternion(); 

		RaycastHit hit;
		if (Physics.Raycast(new Vector3(position.x, 1000, position.z), -Vector3.up, out hit, Mathf.Infinity))
		{
			newRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);			
		}

		return newRotation; 
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return;

		if (HousePlots.GetLength(1) == 0)
			return; 

		foreach (HousePlot house in HousePlots)
		{
			Gizmos.color = house.Open ? Color.green : Color.red;
			Gizmos.DrawWireCube(house.Position, Vector3.one * house.Radius * 2);
		}
	}

	struct HousePlot
	{
		public Vector3 Position;
		public float Radius;
		public bool Open; 
	}
}
