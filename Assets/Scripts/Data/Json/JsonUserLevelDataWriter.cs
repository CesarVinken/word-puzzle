using System.IO;
using UnityEngine;

public class JsonUserLevelDataWriter : IJsonFileWriter
{
    private SerialisableUserGameData _data;
    private string _path;

    public void WriteData<T>(T configurationData, string fileName = "userb")
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets", "User"));

        _data = configurationData as SerialisableUserGameData;
        _path = Path.Combine(Application.dataPath, "StreamingAssets", "User", fileName + ".json");

        string jsonDataString = JsonUtility.ToJson(_data, true).ToString();

        File.WriteAllText(_path, jsonDataString);

        ConsoleLog.Log(LogCategory.General, $"Completed writing user level data for {_data.UserLevelData.Count} levels to {fileName}.json");
    }
}
