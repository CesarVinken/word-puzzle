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
}
