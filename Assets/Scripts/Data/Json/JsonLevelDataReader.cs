using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonLevelDataReader : IJsonFileReader
{
    public SerialisableLevelData ReadData<SerialisableLevelData>(string levelName)
    {
        string jsonContent = GetDataContent(levelName);

        SerialisableLevelData serialisableLevelData = JsonConvert.DeserializeObject<SerialisableLevelData>(jsonContent);

        return serialisableLevelData;
    }

    private string GetDataContent(string levelName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "levels", levelName);
        Debug.Log($"!!! filePath LevelDataReader is {filePath}");
        ConsoleLog.Log(LogCategory.General, $".... filePath LevelDataReader is {filePath}");
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest loadingRequest = UnityWebRequest.Get(filePath);
            loadingRequest.SendWebRequest();
            while (!loadingRequest.isDone && loadingRequest.result != UnityWebRequest.Result.ConnectionError && loadingRequest.result != UnityWebRequest.Result.ProtocolError);
            string result = System.Text.Encoding.UTF8.GetString(loadingRequest.downloadHandler.data);
            return result;
        }
        else
        {
            if (!File.Exists(filePath))
            {
                throw new Exception($"File {levelName}.json doesn't exist.");
            }

            return File.ReadAllText(filePath);
        }
    }
}