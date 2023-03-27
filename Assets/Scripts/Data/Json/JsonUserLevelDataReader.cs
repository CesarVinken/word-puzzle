using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class JsonUserGameDataReader : IJsonFileReader
{
    public SerialisableUserGameData ReadData<SerialisableUserGameData>(string fileName = "user.json")
    {
        string jsonContent;

        string filePath = Path.Combine(Application.streamingAssetsPath, "User", fileName);

        if (!File.Exists(filePath))
        {
            throw new Exception($"File {fileName}.json doesn't exist.");
        }

        jsonContent = File.ReadAllText(filePath);

        SerialisableUserGameData serialisableGameData = JsonConvert.DeserializeObject<SerialisableUserGameData>(jsonContent);

        return serialisableGameData;
    }
}