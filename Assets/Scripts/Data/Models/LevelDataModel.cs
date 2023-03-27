using System.Collections.Generic;
using UnityEngine;

public class LevelDataModel
{
    public int LevelNumber;
    public string Title;
    public List<CharacterTileDataModel> LetterTiles;

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
