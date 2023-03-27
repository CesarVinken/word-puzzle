using System.Collections.Generic;

public class UserLevelDataModel
{
    public int LevelNumber { get; private set; }
    public int HighScore { get; private set; }

    public UserLevelDataModel(int levelNumber, int highScore)
    {
        LevelNumber = levelNumber;
        HighScore = highScore;
    }

    public SerialisableUserLevelData Serialise()
    {
        return new SerialisableUserLevelData(LevelNumber, HighScore);    
    }
}
