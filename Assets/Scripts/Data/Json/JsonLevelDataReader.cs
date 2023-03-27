using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class JsonLevelDataReader : IJsonFileReader
{
    public SerialisableLevelData ReadData<SerialisableLevelData>(string levelName)
    {
        string jsonContent;

        string filePath = Path.Combine(Application.streamingAssetsPath, "Levels", levelName);

        if (!File.Exists(filePath))
        {
            throw new Exception($"File {levelName}.json doesn't exist.");
        }

        jsonContent = File.ReadAllText(filePath);

        SerialisableLevelData serialisableLevelData = JsonConvert.DeserializeObject<SerialisableLevelData>(jsonContent);

        return serialisableLevelData;
    }
}