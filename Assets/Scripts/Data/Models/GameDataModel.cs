using System.Collections.Generic;
using UnityEngine;

public class GameDataModel
{
    public List<LevelDataModel> Levels { get; private set; }

    public GameDataModel(List<LevelDataModel> levels)
    {
        Levels = levels;
    }
}