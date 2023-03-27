using System.Collections.Generic;
using UnityEngine;

public class UserDataWriter
{
    public static void Reset()
    {
        JsonUserLevelDataWriter fileWriter = new JsonUserLevelDataWriter();
        UserGameDataModel userGameDataModel = CreateEmptyUserData();
        SerialisableUserGameData serialisableUserGameData = userGameDataModel.Serialise();

        fileWriter.WriteData(serialisableUserGameData);
    }

    // read game data and create empty entries for each existing level. The empty UserGameDataModel lists all levels but no high score
    private static UserGameDataModel CreateEmptyUserData()
    {
        List<UserLevelDataModel> userLevelDatas = new List<UserLevelDataModel>();

        DataHandler dataHandler = new DataHandler();
        dataHandler.Initialise();

        GameDataModel gameData = dataHandler.GetGameData();

        for (int i = 0; i < gameData.Levels.Count; i++)
        {
            LevelDataModel levelData = gameData.Levels[i];
            UserLevelDataModel userLevelDataModel = new UserLevelDataModel(levelData.LevelNumber, -1);

            userLevelDatas.Add(userLevelDataModel);
        }

        UserGameDataModel userGameDataModel = new UserGameDataModel(userLevelDatas);

        return userGameDataModel;
    }
}
