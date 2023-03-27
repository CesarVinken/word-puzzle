using System.Collections.Generic;
using UnityEngine;

public class GameDataModel
{
    public List<LevelDataModel> Levels;

    public GameDataModel(List<LevelDataModel> levels)
    {
        Levels = levels;
    }
}