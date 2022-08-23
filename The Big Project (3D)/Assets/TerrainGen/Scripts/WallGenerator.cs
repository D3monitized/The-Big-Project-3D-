using UnityEngine;

using System.Collections.Generic; 

public class WallGenerator : MonoBehaviour
{
	public int Openings = 1;
	public float OpeningLength = 2;
	public float WallSegmentLength = 5;
	public float DistanceFromMiddle = 5; 
	public int Corners = 6; 

	private List<Line> Lines = new List<Line>();

	private Vector3 ChunkMiddle;
	private const float WallStartAngleConstant = 90; // 67.5f; //Calculated through paint
	
	private void Start()
	{
		ChunkMiddle = transform.position;
		GenerateWalls();
	}

	void GenerateWalls()
	{
		float angleDiff = 360 / Corners;
		angleDiff *= Mathf.Deg2Rad;

		float startingAngle = Random.Range(0, 360);
		startingAngle *= Mathf.Deg2Rad; 

		float startDeltaZ = Mathf.Sin(startingAngle);
		float startDeltaX = startDeltaZ / Mathf.Tan(startingAngle);
		Vector3 dir = new Vector3(startDeltaX, transform.position.y, startDeltaZ).normalized * DistanceFromMiddle;

		Line startLine;
		startLine.LineStartPos = transform.position;
		startLine.LineEndPos = dir;
		Lines.Add(startLine); 

		float firstAngle = startingAngle + WallStartAngleConstant * Mathf.Deg2Rad;
		float firstDeltaZ = Mathf.Sin(firstAngle);
		float firstDeltaX = firstDeltaZ / Mathf.Tan(firstAngle);
		Vector3 firstDir = new Vector3(firstDeltaX, transform.position.y, firstDeltaZ).normalized;

		Line firstLine;
		firstLine.LineStartPos = dir + firstDir * (OpeningLength / 2);
		firstLine.LineEndPos = dir + firstDir * (WallSegmentLength / 2);
		Lines.Add(firstLine);

		float currentAngle = firstAngle + angleDiff * Mathf.Deg2Rad;
	

		for (int i = 0; i < Corners; i++)
		{
			float deltaZ = Mathf.Sin(currentAngle);
			float deltaX = deltaZ / Mathf.Tan(currentAngle);
			Vector3 currentDir = new Vector3(deltaX, transform.position.y, deltaZ).normalized;

			Line currentLine;
			currentLine.LineStartPos = Lines[Lines.Count - 1].LineEndPos;
			currentLine.LineEndPos = i != Corners - 1 ? currentDir * WallSegmentLength : currentDir * ((WallSegmentLength - OpeningLength) / 2);
			Lines.Add(currentLine);

			currentAngle += angleDiff; 
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Line l in Lines)
			Gizmos.DrawLine(l.LineStartPos, l.LineEndPos); 
	}

	struct Line
	{
		public Vector3 LineStartPos;
		public Vector3 LineEndPos; 
		public float LineLength
		{
			get { return Vector3.Distance(LineStartPos, LineEndPos); }			
		}
	}
}
