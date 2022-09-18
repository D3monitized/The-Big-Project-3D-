using UnityEngine;

public class PrefabGridGenerator : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField]
	private float PrefabPlotRadius = 1;
	[SerializeField]
	private int AmountOfPrefabs = 10;	

	[Header("Components")]	
	[SerializeField]
	private Transform WaterSurfaceTransform;

	private Vector3 ChunkPosition;
	private Vector2 ChunkSize;

	private PrefabPlot[,] PrefabPlots;
	private int AmountOfPrefabsHor;
	private int AmountOfPrefabsVer;

	private GameObject[] prefabs;	

	public void GeneratePrefabs(Vector3 _chunkPosition, float ChunkRadius, ChunkBase chunkInfo)
	{
		prefabs = new GameObject[0];

		ChunkSize.x = ChunkRadius * 2;
		ChunkSize.y = ChunkRadius * 2;
		ChunkPosition = _chunkPosition;

		if (chunkInfo != null)
			prefabs = chunkInfo.Prefabs;
		
		GeneratePrefabPlots();
	}

	private void GeneratePrefabPlots()
	{
		AmountOfPrefabsHor = Mathf.RoundToInt(ChunkSize.x / (PrefabPlotRadius * 2));
		AmountOfPrefabsVer = Mathf.RoundToInt(ChunkSize.y / (PrefabPlotRadius * 2));
		PrefabPlots = new PrefabPlot[AmountOfPrefabsHor, AmountOfPrefabsVer];

		//Makes sure there aren't more houses than plots or less houses than horizontal rows
		if (AmountOfPrefabs < AmountOfPrefabsVer)
			AmountOfPrefabs = AmountOfPrefabsVer;
		else if (AmountOfPrefabs > AmountOfPrefabsHor * AmountOfPrefabsVer)
			AmountOfPrefabs = AmountOfPrefabsHor * AmountOfPrefabsVer;

		Vector3 startPos = new Vector3(ChunkPosition.x - ChunkSize.x / 2 + PrefabPlotRadius, ChunkPosition.y, ChunkPosition.z + ChunkSize.y / 2 - PrefabPlotRadius);
		Vector3 currentPos = startPos;
		int amountOfPrefabsPerRow = Mathf.RoundToInt(AmountOfPrefabs / AmountOfPrefabsVer);

		for (int y = 0; y < AmountOfPrefabsVer; y++)
		{
			for (int i = 0; i < amountOfPrefabsPerRow; i++)
			{
				int r = Random.Range(0, AmountOfPrefabsHor);
				if (PrefabPlots[r, y].Open)
					i--;
				else
					PrefabPlots[r, y].Open = true;
			}

			for (int x = 0; x < AmountOfPrefabsHor; x++)
			{
				PrefabPlots[x, y].Position = currentPos;
				PrefabPlots[x, y].Radius = PrefabPlotRadius;

				currentPos.x += PrefabPlotRadius * 2;
			}

			currentPos.z -= PrefabPlotRadius * 2;
			currentPos.x = startPos.x;
		}

		BuildPrefabs();
	}

	private void BuildPrefabs()
	{
		for (int y = 0; y < PrefabPlots.GetLength(1); y++)
		{
			for (int x = 0; x < PrefabPlots.GetLength(0); x++)
			{
				if (!PrefabPlots[x, y].Open)
					continue;

				int randAngle = Random.Range(0, 360);
				Quaternion rot = GetTerrainSlope(PrefabPlots[x, y].Position);
				PrefabPlots[x, y].Position = GetTerrainHeight(PrefabPlots[x, y].Position);
				if (WaterSurfaceTransform != null && PrefabPlots[x, y].Position.y < WaterSurfaceTransform.position.y)
					continue;

				if(prefabs.Length > 0)
				{
					GameObject prefab = Instantiate(prefabs[Random.Range(0, prefabs.Length)], PrefabPlots[x, y].Position, rot);
					prefab.GetComponent<Transform>().RotateAround(prefab.transform.position, prefab.transform.up, randAngle);
				}				
			}
		}		
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

		if (PrefabPlots.GetLength(1) == 0)
			return;

		foreach (PrefabPlot prefab in PrefabPlots)
		{
			Gizmos.color = prefab.Open ? Color.green : Color.red;
			Gizmos.DrawWireCube(prefab.Position, Vector3.one * prefab.Radius * 2);
		}
	}

	struct PrefabPlot
	{
		public Vector3 Position;
		public float Radius;
		public bool Open;
	}	
}


