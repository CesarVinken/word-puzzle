using System.Collections.Generic;
using UnityEngine;

public class CharacterTileHandler
{
    public static List<CharacterTile> Tiles = new List<CharacterTile>();
    
    private Dictionary<int, CharacterTile> _tilesById = new Dictionary<int, CharacterTile>();
    private GameFlowService _gameFlowService;
    private int _currentJobs = 0;
    private Transform _characterTileContainer;

    public void Setup()
    {
        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
    }

    public void PopulateLevel(Transform container)
    {
        _characterTileContainer = container;
        List<CharacterTileDataModel> characterTileDatas = GameManager.Instance.CurrentLevelData.LetterTiles;

        Tiles.Clear();
        _tilesById.Clear();

        for (int i = 0; i < characterTileDatas.Count; i++)
        {
            _currentJobs++;
            CharacterTileFactory.Create(_characterTileContainer, this, characterTileDatas[i]);
        }
    }

    public void OnCharacterTileCreated(GameObject characterTileGO, CharacterTileMono characterTile)
    {
        _tilesById.Add(characterTile.CharacterTile.Id, characterTile.CharacterTile);
        Tiles.Add(characterTile.CharacterTile);
        _currentJobs--;

        if(_currentJobs == 0)
        {
            OnCharacterTileCreationFinished();
        }
    }

    public void OnCharacterTileCreationFinished()
    {
        _gameFlowService.SetTilesById(_tilesById);

        foreach (KeyValuePair<int, CharacterTile> item in _tilesById)
        {
            CharacterTile characterTile = item.Value;
            List<CharacterTile> tileChildren = GetChildConnections(characterTile, _tilesById);
            CharacterTileMono characterTileMono = characterTile.CharacterTileMono;
            if (characterTileMono)
            {
                characterTileMono.Initialise(tileChildren);
            }
            else
            {
                characterTile.Initialise(tileChildren);
            }
        }

        foreach (KeyValuePair<int, CharacterTile> item in _tilesById)
        {
            item.Value.SetInitialState();
        }

        SortTiles(_characterTileContainer);
    }

    // when the tiles are created they are not set in the correct order in the hierarchy
    public void SortTiles(Transform container)
    {
        List<RectTransform> children = new List<RectTransform>();
        foreach (RectTransform child in container.GetComponentsInChildren<RectTransform>())
        {
            if (child != container)
            {
                children.Add(child);
            }
        }

        children.Sort(delegate (RectTransform a, RectTransform b)
        {
            return b.localPosition.z.CompareTo(a.localPosition.z);
        });

        foreach (RectTransform child in children)
        {
            child.SetAsLastSibling();
        }
    }

    public void UndoLastTile()
    {
        if (_gameFlowService.LetterPickActions.Count == 0)
        {
            ConsoleLog.Warning(LogCategory.General, $"We should not be requested to undo a tile here if there, because there are no actions to undo");
            return;
        }

        LetterPickAction lastLetterPickAction = _gameFlowService.LetterPickActions[_gameFlowService.LetterPickActions.Count - 1];
        lastLetterPickAction.CharacterTile.SetCharacterTileState(CharacterTileState.Open);

        _gameFlowService.RemoveLastAction();

        string word = _gameFlowService.GetFormedWord();
        _gameFlowService.ValidationHandler.Validate(word);
    }

    private List<CharacterTile> GetChildConnections(CharacterTile characterTile, Dictionary<int, CharacterTile> tilesById)
    {
        List<int> tileChildrenIds = characterTile.CharacterTileData.TileChildren;
        List<CharacterTile> tileChildren = new List<CharacterTile>();

        foreach (int id in tileChildrenIds)
        {
            CharacterTile childTile = tilesById[id];
            tileChildren.Add(childTile);
        }

        return tileChildren;
    }
}