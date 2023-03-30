using System.IO;
using UnityEngine;

public class JsonUserLevelDataWriter : IJsonFileWriter
{
    private SerialisableUserGameData _data;
    private string _path;

    public void WriteData<T>(T configurationData, string fileName = "user")
    {
        _data = configurationData as SerialisableUserGameData;
        _path = GetPath();

        string jsonDataString = JsonUtility.ToJson(_data, true).ToString();

        File.WriteAllText(_path, jsonDataString);

        ConsoleLog.Log(LogCategory.General, $"Completed writing user level data for {_data.UserLevelData.Count} levels to {fileName}.json");
    }

    private string GetPath()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.LogWarning("Trying to write user level data on Android");

            if (!File.Exists(Path.Combine(Application.persistentDataPath, "user", "user.json")))
            {
                File.Create(Path.Combine(Application.persistentDataPath, "user", "user.json"));
            }

            return Path.Combine(Application.persistentDataPath, "StreamingAssets", "user", "user.json");
        }
        else
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets", "user"));

            return Path.Combine(Application.dataPath, "StreamingAssets", "user", "user.json");
        }
    }
}
