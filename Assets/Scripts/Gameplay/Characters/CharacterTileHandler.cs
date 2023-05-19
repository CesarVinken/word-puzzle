using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CharacterTileHandler
{
    public static List<CharacterTile> Tiles = new List<CharacterTile>();
    private GameFlowService _gameFlowService;
    Dictionary<int, CharacterTile> _tilesById = new Dictionary<int, CharacterTile>();
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
    //    GameObject characterTilePrefab = CharacterTileFactory.Create(container);
     //   Dictionary<int, CharacterTile> tilesById = new Dictionary<int, CharacterTile>();
        Tiles.Clear();
        _tilesById.Clear();
      //  Tiles = new List<CharacterTile>();

        for (int i = 0; i < characterTileDatas.Count; i++)
        {
            _currentJobs++;
            CharacterTileFactory.Create(_characterTileContainer, this, characterTileDatas[i]);
            //CharacterTileMono characterTileMono = CreateTile(characterTileDatas[i], characterTilePrefab, container);
          //  tilesById.Add(characterTileMono.CharacterTile.Id, characterTileMono.CharacterTile);
     //       Tiles.Add(characterTileMono.CharacterTile);
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
        LetterPickAction lastLetterPickAction = _gameFlowService.LetterPickActions[_gameFlowService.LetterPickActions.Count - 1];
        lastLetterPickAction.CharacterTile.SetCharacterTileState(CharacterTileState.Open);

        _gameFlowService.RemoveLastAction();

        string word = _gameFlowService.GetFormedWord();
        _gameFlowService.ValidationHandler.Validate(word);
    }

    private CharacterTileMono CreateTile(CharacterTileDataModel characterTileData, GameObject characterTilePrefab, Transform container)
    {
        // todo we can use object pooling here
        GameObject characterTileGO = GameObject.Instantiate(characterTilePrefab, container);

        RectTransform tileRectTransform = characterTileGO.GetComponent<RectTransform>();

        Vector3 tilePosition = new Vector3((characterTileData.TilePosition.x * 12), characterTileData.TilePosition.y * 12, characterTileData.TilePosition.z);

        tileRectTransform.localPosition = tilePosition;

        CharacterTileMono characterTile = characterTileGO.GetComponent<CharacterTileMono>();
        characterTile.Setup(characterTileData);
        return characterTile;
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