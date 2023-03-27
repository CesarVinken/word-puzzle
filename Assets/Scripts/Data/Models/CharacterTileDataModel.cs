using System.Collections.Generic;
using UnityEngine;

public class CharacterTileDataModel
{
    public int Id;
    public Vector3 TilePosition;
    public string Character;
    public List<int> TileChildren;

    public CharacterTileDataModel(int id, Vector3 tilePosition, string character, List<int> tileChildren)
    {
        Id = id;
        TilePosition = tilePosition;
        Character = character;
        TileChildren = tileChildren;
    }
}

