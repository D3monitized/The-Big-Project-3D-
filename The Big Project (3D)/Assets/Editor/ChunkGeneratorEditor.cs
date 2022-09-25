using UnityEngine; 
using UnityEditor;

[CustomEditor(typeof(ChunkGenerator))]
[CanEditMultipleObjects]
public class ChunkGeneratorEditor : Editor
{
	SerializedProperty ChunkRadius; 
	SerializedProperty ChunkTypes;
	SerializedProperty MapTerrain;
	SerializedProperty WaterSurfaceTransform; 

	private DisplayType displayedCategory = DisplayType.Chunksettings; 
	private int totalSpawnChance; 

	private void OnEnable()
	{
		ChunkRadius = serializedObject.FindProperty("ChunkRadius"); 
		ChunkTypes = serializedObject.FindProperty("ChunkTypes");
		MapTerrain = serializedObject.FindProperty("MapTerrain");
		WaterSurfaceTransform = serializedObject.FindProperty("WaterSurfaceTransform");		
			
		totalSpawnChance = 0; 
		for (int i = 0; i < ChunkTypes.arraySize; i++)
		{
			ChunkBase c = ChunkTypes.GetArrayElementAtIndex(i).objectReferenceValue as ChunkBase;
			totalSpawnChance += c.SpawnChance; 			
		}
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();
		serializedObject.Update();

		displayedCategory = (DisplayType)EditorGUILayout.EnumPopup("Display", displayedCategory);
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		switch (displayedCategory)
		{
			case DisplayType.Chunksettings:
				ChunkSettings();
			break;
			case DisplayType.Components:
				Components();
			break; 
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		if (GUILayout.Button("Generate"))
		{
			ChunkGenerator gen = serializedObject.targetObject as ChunkGenerator;
			gen.Generate(); 
		}

		serializedObject.ApplyModifiedProperties();		
	}

	private void ChunkSettings()
	{
		EditorGUILayout.PropertyField(ChunkRadius);		
		EditorGUILayout.PropertyField(ChunkTypes);		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Chunk Spawn Chance", EditorStyles.boldLabel);

		for (int i = 0; i < ChunkTypes.arraySize; i++)
		{
			ChunkBase c = ChunkTypes.GetArrayElementAtIndex(i).objectReferenceValue as ChunkBase;

			if (c != null)
			{
				EditorGUILayout.LabelField(c.name);
				EditorGUI.BeginChangeCheck();
				c.SpawnChance = EditorGUILayout.IntSlider(c.SpawnChance, 0, 100);
				if (EditorGUI.EndChangeCheck())
				{
					AdjustSpawnChances(ref c, c.SpawnChance);
				}
			
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Max Sloping For Spawn", EditorStyles.boldLabel); 
		
		for (int i = 0; i < ChunkTypes.arraySize; i++)
		{
			ChunkBase c = ChunkTypes.GetArrayElementAtIndex(i).objectReferenceValue as ChunkBase;
			
			if (c != null)
			{
				EditorGUILayout.LabelField(c.name);
				c.MaxSlopeForSpawning = EditorGUILayout.IntSlider(c.MaxSlopeForSpawning, 0, 90);
			}
		}

		
	}

	private void Components()
	{
		EditorGUILayout.PropertyField(MapTerrain); 
		EditorGUILayout.PropertyField(WaterSurfaceTransform); 
	}

	private void AdjustSpawnChances(ref ChunkBase chunkRef, int spawnChance)
	{
		GetTotalSpawnChance(); 

		if (totalSpawnChance == 100)
			return;

		int rest = totalSpawnChance - 100; 

		for (int i = 0; i < ChunkTypes.arraySize; i++)
		{
			ChunkBase c = ChunkTypes.GetArrayElementAtIndex(i).objectReferenceValue as ChunkBase;

			if (c != chunkRef && c.SpawnChance - rest >= 0)
			{
				c.SpawnChance -= rest;
				break; 
			}
			else if(i == ChunkTypes.arraySize - 1)
			{
				chunkRef.SpawnChance = spawnChance; 
			}
		}
	}

	private void GetTotalSpawnChance()
	{
		totalSpawnChance = 0;

		for (int i = 0; i < ChunkTypes.arraySize; i++)
		{
			ChunkBase c = ChunkTypes.GetArrayElementAtIndex(i).objectReferenceValue as ChunkBase;

			totalSpawnChance += c.SpawnChance; 
		}
	}

	private enum DisplayType
	{
		Chunksettings,
		Components
	}
	
}
