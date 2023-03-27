using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGameDataModel
{
    public List<UserLevelDataModel> Levels;

    public UserGameDataModel(List<UserLevelDataModel> levels)
    {
        Levels = levels;
    }

    public SerialisableUserGameData Serialise()
    {
        List<SerialisableUserLevelData> userLevelDatas = new List<SerialisableUserLevelData>();

        for (int i = 0; i < Levels.Count; i++)
        {
            UserLevelDataModel userLevelData = Levels[i];
            SerialisableUserLevelData serialisableUserLevelData = userLevelData.Serialise();
            userLevelDatas.Add(serialisableUserLevelData);
        }

        SerialisableUserGameData serialisableUserGameData = new SerialisableUserGameData(userLevelDatas);

        return serialisableUserGameData;
    }
}
