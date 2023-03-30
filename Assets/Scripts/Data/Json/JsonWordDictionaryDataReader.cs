using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonWordDictionaryDataReader : IJsonFileReader
{
    public SerialisableWordDictionaryData ReadData<SerialisableWordDictionaryData>(string fileName = "words.json")
    {
        string jsonContent = GetDataContent();

        SerialisableWordDictionaryData serialisableWordDictionaryData = JsonConvert.DeserializeObject<SerialisableWordDictionaryData>(jsonContent);

        return serialisableWordDictionaryData;
    }

    private string GetDataContent()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Words", "words.json");

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest loadingRequest = UnityWebRequest.Get(filePath);
            loadingRequest.SendWebRequest();
            while (!loadingRequest.isDone && loadingRequest.result != UnityWebRequest.Result.ConnectionError && loadingRequest.result != UnityWebRequest.Result.ProtocolError);
            return loadingRequest.downloadHandler.text.Trim();
        }
        else
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"File words.json doesn't exist.");
            }

            return File.ReadAllText(filePath);
        }
    }
}
