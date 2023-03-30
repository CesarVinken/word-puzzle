using System.Collections.Generic;
using UnityEngine;

public class LevelDataModel
{
    public int LevelNumber { get; private set; }
    public string Title { get; private set; }
    public List<CharacterTileDataModel> LetterTiles { get; private set; }

    public LevelDataModel(string title, List<CharacterTileDataModel> letterTiles)
    {
        Title = title;
        LetterTiles = letterTiles;
    }

    public LevelDataModel WithLevelNumber(int levelNumber)
    {
        LevelNumber = levelNumber;

        return this;
    }
}
