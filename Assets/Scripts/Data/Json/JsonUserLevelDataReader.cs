using Newtonsoft.Json;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class JsonUserGameDataReader : IJsonFileReader
{
    public SerialisableUserGameData ReadData<SerialisableUserGameData>(string fileName = "user.json")
    {
        string jsonContent = GetDataContent(fileName);

        SerialisableUserGameData serialisableGameData = JsonConvert.DeserializeObject<SerialisableUserGameData>(jsonContent);

        return serialisableGameData;
    }

    private string GetDataContent(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "user", fileName);

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest loadingRequest = UnityWebRequest.Get(filePath);
            loadingRequest.SendWebRequest();
            while (!loadingRequest.isDone && loadingRequest.result != UnityWebRequest.Result.ConnectionError && loadingRequest.result != UnityWebRequest.Result.ProtocolError) ;
            return loadingRequest.downloadHandler.text.Trim();
        }
        else
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"File {fileName} doesn't exist.");
            }

            return File.ReadAllText(filePath);
        }
    }
}