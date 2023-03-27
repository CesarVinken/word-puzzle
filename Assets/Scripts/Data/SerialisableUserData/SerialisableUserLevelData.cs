using System;
using UnityEngine;

[Serializable]
public class SerialisableUserLevelData
{
    public int LevelNumber;
    public int HighScore;

    public SerialisableUserLevelData(int levelNumber, int highScore)
    {
        LevelNumber = levelNumber;
        HighScore = highScore;
    }

    public UserLevelDataModel Deserialise()
    {
        return new UserLevelDataModel(LevelNumber, HighScore);
    }
}
