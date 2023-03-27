using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataHandler
{
    private List<string> levelFileNames = new List<string>();

    public void Initialise()
    {
        string filePath = Path.Combine(Path.Combine(Application.streamingAssetsPath, "Levels"));

        levelFileNames = Directory.GetFiles(filePath)
            .Where(file => !file.EndsWith(".meta"))
            .Select(Path.GetFileName)
            .ToList();
    }

    public List<LevelDataModel> GetGameData()
    {
        List<LevelDataModel> gameData = new List<LevelDataModel>();

        for (int i = 0; i < levelFileNames.Count; i++)
        {
            string levelFileName = levelFileNames[i];
            LevelDataModel levelData = GetLevelData(levelFileName);

            gameData.Add(levelData);
        }

        // keep the levels sorted by level number, otherwise 1 is followed by 11 instead of 2
        gameData = gameData.OrderBy(l => l.LevelNumber).ToList();

        ConsoleLog.Log(LogCategory.Data, $"Loaded data for {gameData.Count} levels", LogPriority.Low);

        return gameData;
    }

    private LevelDataModel GetLevelData(string levelFileName)
    {
        SerialisableLevelData serialisableLevelData = new JsonLevelDataReader().ReadData<SerialisableLevelData>(levelFileName);

        if (serialisableLevelData == null)
        {
            ConsoleLog.Error(LogCategory.Data, $"Could not load Game Data");
        }

        int levelNumber = ExtractLevelNumber(levelFileName);
        return serialisableLevelData.Deserialise().WithLevelNumber(levelNumber);
    }

    private int ExtractLevelNumber(string levelFileName)
    {
        string[] parts = levelFileName.Split('_', '.');
        string numberString = parts[1];

        if (int.TryParse(numberString, out int levelNumber))
        {
            return levelNumber;
        }

        throw new CouldNotParseException($"Could not parse '{numberString}' from the level file name '{levelFileName}' as a number.");
    }

    public List<UserLevelDataModel> GetUserData()
    {
        return new List<UserLevelDataModel>();
    }
}