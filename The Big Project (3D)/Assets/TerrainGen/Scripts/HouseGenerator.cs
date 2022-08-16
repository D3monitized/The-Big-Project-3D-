using UnityEngine;

//Generate Straight line (Startpos/Endpos)
//Choose a couple of random house models and spread out on line
//Place houses along line with some offset between them

public class HouseGenerator : MonoBehaviour
{
	public float LineAmount = 1; 
	public float HouseOffset = 2; 

	[SerializeField]
	private GameObject[] HousePrefabs; 

	private Transform GroundMeshTransform;
	private Renderer GroundMeshRenderer;
	private Vector3 GroundMeshStartCorner;
	private Vector3 GroundMeshEndCorner;

	private Vector3 LineStartPos;
	private Vector3 LineEndPos; 

	private void Awake()
	{
		GroundMeshTransform = GetComponent<Transform>();
		GroundMeshRenderer = GetComponent<Renderer>(); 
	}

	private void Start()
	{
		GroundMeshStartCorner = new Vector3(GroundMeshTransform.position.x + GroundMeshRenderer.bounds.size.x / 2, GroundMeshTransform.position.y, GroundMeshTransform.position.z + GroundMeshRenderer.bounds.size.z / 2);
		GroundMeshEndCorner = new Vector3(GroundMeshTransform.position.x - GroundMeshRenderer.bounds.size.x / 2, GroundMeshTransform.position.y, GroundMeshTransform.position.z - GroundMeshRenderer.bounds.size.z / 2);

		GenerateLines();
		SpawnHouses();
	}

	private void GenerateLines()
	{
		LineStartPos = new Vector3(Random.Range(GroundMeshStartCorner.x, GroundMeshEndCorner.x), GroundMeshTransform.position.y, Random.Range(GroundMeshStartCorner.z, GroundMeshEndCorner.z));
		LineEndPos = new Vector3(Random.Range(GroundMeshStartCorner.x, GroundMeshEndCorner.x), GroundMeshTransform.position.y, Random.Range(GroundMeshStartCorner.z, GroundMeshEndCorner.z));
	}

	private void SpawnHouses()
	{
		float lineLength = Vector3.Distance(LineStartPos, LineEndPos);
		float houseLength = HousePrefabs[0].GetComponent<Renderer>().bounds.size.x;
		int houseAmount = Mathf.RoundToInt(lineLength / houseLength);
		houseAmount -= houseAmount * 2 > lineLength ? 1 : 0;
		float totalLength = houseAmount * houseLength + HouseOffset * (houseAmount - 1); 

		float startOffset = (lineLength - totalLength) / 2 + houseLength / 2;

		Vector3 dir = LineStartPos - LineEndPos;
		dir.Normalize();

		float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(0, angle + 90, 0);

		Vector3 currentPos = LineStartPos - dir * startOffset;

		for (int i = 0; i < houseAmount; i++)
		{
			Instantiate(HousePrefabs[0], currentPos, rotation);
			currentPos -= dir * (houseLength + HouseOffset); 
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawWireCube(GroundMeshStartCorner, Vector3.one);
		//Gizmos.DrawWireCube(GroundMeshEndCorner, Vector3.one);

		//Gizmos.DrawWireCube(LineStartPos, Vector3.one); 
		//Gizmos.DrawWireCube(LineEndPos, Vector3.one);
		Debug.DrawLine(LineStartPos, LineEndPos);
	}
}
