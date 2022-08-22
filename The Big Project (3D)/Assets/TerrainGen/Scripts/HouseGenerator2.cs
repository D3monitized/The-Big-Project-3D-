using UnityEngine;
using System.Collections.Generic; 

public class HouseGenerator2 : MonoBehaviour
{
	public float ChunkWidth = 10;
	public float ChunkHeight = 10;
	public float HousePlotRadius = 1;
	public int AmountOfHouses = 10; 

	private int AmountOfHousesHor;
	private int AmountOfHousesVer;

	[SerializeField]
	private GameObject[] HousePrefabs;

	//List<HousePlot> HousePlots = new List<HousePlot>();
	HousePlot[,] HousePlots; 

	private void Start()
	{
		ChunkWidth = GetComponent<Renderer>().bounds.size.x;
		ChunkHeight = GetComponent<Renderer>().bounds.size.z;
		GenerateHousePlots();
		BuildHouses(); 
	}

	void GenerateHousePlots()
	{
		AmountOfHousesHor = Mathf.RoundToInt(ChunkWidth / (HousePlotRadius * 2));
		AmountOfHousesVer = Mathf.RoundToInt(ChunkHeight / (HousePlotRadius * 2));
		HousePlots = new HousePlot[AmountOfHousesHor, AmountOfHousesVer];
		
		//Makes sure there aren't more houses than plots or less houses than horizontal rows
		if (AmountOfHouses < AmountOfHousesVer)
			AmountOfHouses = AmountOfHousesVer;
		else if (AmountOfHouses > AmountOfHousesHor * AmountOfHousesVer)
			AmountOfHouses = AmountOfHousesHor * AmountOfHousesVer; 

		Vector3 startPos = new Vector3(transform.position.x - ChunkWidth / 2 + HousePlotRadius, transform.position.y, transform.position.z + ChunkHeight / 2 - HousePlotRadius);
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
	}

	void BuildHouses()
	{
		foreach(HousePlot housePlot in HousePlots)
		{
			if (!housePlot.Open)
				continue; 

			int randAngle = Random.Range(0, 360);
			Quaternion rot = Quaternion.Euler(0, randAngle, 0); 

			Instantiate(HousePrefabs[0], housePlot.Position, rot); 
		}
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
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
