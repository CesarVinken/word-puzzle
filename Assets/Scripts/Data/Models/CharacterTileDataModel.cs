using System.Collections.Generic;
using UnityEngine;

public class CharacterTileDataModel
{
    public int Id { get; private set; }
    public Vector3 TilePosition { get; private set; }
    public string Character { get; private set; }
    public List<int> TileChildren { get; private set; }
    public List<int> TileParents { get; private set; }
    public CharacterTileState State = CharacterTileState.Open;

    public CharacterTileDataModel(int id, Vector3 tilePosition, string character, List<int> tileChildren)
    {
        Id = id;
        TilePosition = tilePosition;
        Character = character;
        TileChildren = tileChildren;
        TileParents = new List<int>();
    }

    public void AddParent(int parentId)
    {
        TileParents.Add(parentId);
    }
}

