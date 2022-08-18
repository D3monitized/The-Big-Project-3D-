using UnityEngine;
using System.Collections.Generic; 

public class HouseGenerator2 : MonoBehaviour
{
	public float ChunkWidth = 10;
	public float ChunkHeight = 10;
	public float HousePlotRadius = 1;

	private int AmountOfHousesHor;
	private int AmountOfHousesVer;

	[SerializeField]
	private GameObject[] HousePrefabs;

	List<HousePlot> HousePlots = new List<HousePlot>(); 

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
		Vector3 startPos = new Vector3(transform.position.x - ChunkWidth / 2 + HousePlotRadius, transform.position.y, transform.position.z + ChunkHeight / 2 - HousePlotRadius);
		Vector3 currentPos = startPos;

		for (int y = 0; y < AmountOfHousesVer; y++)
		{
			for (int x = 0; x < AmountOfHousesHor; x++)
			{
				HousePlot newHousePlot;
				newHousePlot.Position = currentPos;
				newHousePlot.Radius = HousePlotRadius;
				HousePlots.Add(newHousePlot);

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
			int randAngle = Random.Range(0, 360);
			Quaternion rot = Quaternion.Euler(0, randAngle, 0); 

			Instantiate(HousePrefabs[0], housePlot.Position, rot); 
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (HousePlot house in HousePlots)
			Gizmos.DrawWireCube(house.Position, Vector3.one * house.Radius * 2);
	}

	struct HousePlot
	{
		public Vector3 Position;
		public float Radius; 
	}
}
