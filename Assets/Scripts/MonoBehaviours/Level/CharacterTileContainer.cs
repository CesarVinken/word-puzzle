using System.Collections.Generic;
using UnityEngine;

public class CharacterTileContainer : MonoBehaviour
{
    private CharacterTileHandler _characterTileHandler;

    public void Setup()
    {
        _characterTileHandler = new CharacterTileHandler();
    }

    public void Initialise()
    {
        _characterTileHandler.PopulateLevel(transform);
        _characterTileHandler.SortTiles(transform);

        GameFlowManager.Instance.UndoEvent += OnUndoEvent;
    }

    public void Unload()
    {
        GameFlowManager.Instance.UndoEvent -= OnUndoEvent;
    }

    public void OnUndoEvent(object sender, UndoEvent e)
    {
        _characterTileHandler.UndoLastTile();
    }
}
