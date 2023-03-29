using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class JsonWordDictionaryDataReader : IJsonFileReader
{
    public SerialisableWordDictionaryData ReadData<SerialisableWordDictionaryData>(string fileName = "words.json")
    {
        string jsonContent;

        string filePath = Path.Combine(Application.streamingAssetsPath, "Words", fileName);

        if (!File.Exists(filePath))
        {
            throw new Exception($"File {fileName}.json doesn't exist.");
        }

        jsonContent = File.ReadAllText(filePath);

        SerialisableWordDictionaryData serialisableWordDictionaryData = JsonConvert.DeserializeObject<SerialisableWordDictionaryData>(jsonContent);

        return serialisableWordDictionaryData;
    }
}
