using System.Collections.Generic;
using UnityEngine;

public class CharacterTileContainer : MonoBehaviour
{
    private CharacterTileHandler _characterTileHandler;
    private GameFlowService _gameFlowService;

    public void Setup()
    {
        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();

        _characterTileHandler = new CharacterTileHandler();
        _characterTileHandler.Setup();
    }

    public void Initialise()
    {
        _characterTileHandler.PopulateLevel(transform);
        _characterTileHandler.SortTiles(transform);

        _gameFlowService.UndoEvent += OnUndoEvent;
    }

    public void Unload()
    {
        _gameFlowService.UndoEvent -= OnUndoEvent;
    }

    public void OnUndoEvent(object sender, UndoEvent e)
    {
        _characterTileHandler.UndoLastTile();
    }
}
