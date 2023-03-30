using System.Collections.Generic;
using UnityEngine;

public class CharacterTile
{
    public CharacterTileMono CharacterTileMono { get; private set; }
    public List<CharacterTile> TileParents = new List<CharacterTile>();
    public List<CharacterTile> TileChildren = new List<CharacterTile>();
    public int Id { get; private set; }

    public CharacterTileDataModel CharacterTileData { get; private set; }

    public CharacterTileState State = CharacterTileState.Open;

    public void Setup(CharacterTileDataModel characterTileData, CharacterTileMono characterTileMono = null)
    {
        CharacterTileMono = characterTileMono;
        CharacterTileData = characterTileData;
        Id = CharacterTileData.Id;
    }

    public void Initialise(List<CharacterTile> tileChildren)
    {
        TileChildren = tileChildren;

        for (int i = 0; i < TileChildren.Count; i++)
        {
            TileChildren[i].AddParentTile(this);
        }
    }

    public CharacterTile WithCharacterTileData(CharacterTileDataModel characterTileData)
    {
        CharacterTileData = characterTileData;
        return this;
    }

    public void SetInitialState()
    {
        if (TileParents.Count == 0)
        {
            CharacterTileMono?.Open();
            State = CharacterTileState.Open;
        }
        else
        {
            CharacterTileMono?.Block();
            State = CharacterTileState.Blocked;
        }
    }

    public void AddParentTile(CharacterTile characterTile)
    {
        TileParents.Add(characterTile);
    }

    public void SetCharacterTileState(CharacterTileState newState)
    {
        State = newState;

        switch (State)
        {
            case CharacterTileState.Blocked:
                CharacterTileMono?.Block();
                BlockChildren();
                break;
            case CharacterTileState.Open:
                CharacterTileMono?.Open();
                BlockChildren();
                break;
            case CharacterTileState.Used:
                CharacterTileMono?.Use();
                OpenChildren();
                break;
            default:
                throw new NotImplementedException("Character tile state", State.ToString());
        }
    }

    private void BlockChildren()
    {
        for (int i = 0; i < TileChildren.Count; i++)
        {
            if (TileChildren[i].State != CharacterTileState.Blocked)
            {
                CharacterTile childTile = TileChildren[i];
                childTile.CharacterTileMono?.Block();
                childTile.State = CharacterTileState.Blocked;
            }
        }
    }

    private void OpenChildren()
    {
        for (int i = 0; i < TileChildren.Count; i++)
        {
            if (TileChildren[i].State == CharacterTileState.Open) continue;

            bool hasBlockingParent = false;
            CharacterTile childTile = TileChildren[i];
            for (int j = 0; j < childTile.TileParents.Count; j++)
            {
                if (childTile.TileParents[j].State != CharacterTileState.Used)
                {
                    hasBlockingParent = true;
                }
            }

            if (hasBlockingParent) continue;

            childTile.CharacterTileMono?.Open();
            childTile.State = CharacterTileState.Open;
        }
    }
}