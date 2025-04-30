using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class EditorWorldCsvParser : EditorWindow
{
    private string csvFilePath = "Assets/Data/worlds.csv"; // Default path for CSV
    private const string OutputFolder = "Assets/Data/WorldAssets/"; // Hardcoded output folder

    [MenuItem("Tools/CSV Parser")]
    public static void ShowWindow()
    {
        GetWindow<EditorWorldCsvParser>("CSV Parser");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV File Parser", EditorStyles.boldLabel);
        csvFilePath = EditorGUILayout.TextField("CSV File Path:", csvFilePath);

        if (GUILayout.Button("Parse CSV"))
        {
            ParseCSV();
        }
    }

    private void ParseCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"CSV file not found at path: {csvFilePath}");
            return;
        }

        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        foreach (string line in lines.Skip(1)) // Skip header row
        {
            string[] values = line.Split(',');
            if (values.Length < 8)
            {
                Debug.LogWarning($"Skipping invalid row: {line}");
                continue;
            }

            WorldData worldData = CreateInstance<WorldData>();
            worldData.worldName = values[0].Trim();
            worldData.gravityMPS2 = float.Parse(values[1]);
            worldData.minTempF = int.Parse(values[2]);
            worldData.maxTempF = int.Parse(values[3]);

            // Updated rotation length parsing: now expecting separate integer columns
            worldData.dayNightRotationLength = new WorldData.SerializableTime
            {
                days = int.Parse(values[4]),
                hours = int.Parse(values[5]),
                minutes = int.Parse(values[6])
            };

            worldData.distanceFromSunAU = float.Parse(values[7]);
            worldData.parentBody = values[8].Trim();

            // Save as Scriptable Object
            string assetPath = $"{OutputFolder}{worldData.worldName}.asset";
            AssetDatabase.CreateAsset(worldData, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("CSV parsing completed. Scriptable Objects created successfully.");
    }
}