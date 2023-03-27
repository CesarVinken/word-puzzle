using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialisableUserGameData
{
    public List<SerialisableUserLevelData> UserLevelData;

    public SerialisableUserGameData(List<SerialisableUserLevelData> userLevelData)
    {
        UserLevelData = userLevelData;
    }

    public UserGameDataModel Deserialise()
    {
        List<UserLevelDataModel> userLevelDatas = new List<UserLevelDataModel>();

        for (int i = 0; i < UserLevelData.Count; i++)
        {
            UserLevelDataModel userLevelData = UserLevelData[i].Deserialise();
            userLevelDatas.Add(userLevelData);
        }

        return new UserGameDataModel(userLevelDatas);
    }
}
