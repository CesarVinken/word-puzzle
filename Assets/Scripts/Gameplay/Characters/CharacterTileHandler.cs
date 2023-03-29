using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CharacterTileHandler
{
    public static List<CharacterTile> Tiles = new List<CharacterTile>();

    public void PopulateLevel(Transform container)
    {
        List<CharacterTileDataModel> characterTileDatas = GameManager.Instance.CurrentLevelData.LetterTiles;
        GameObject characterTilePrefab = AssetManager.Instance.GetCharacterTilePrefab();
        Dictionary<int, CharacterTile> tilesById = new Dictionary<int, CharacterTile>();
        Tiles = new List<CharacterTile>();

        for (int i = 0; i < characterTileDatas.Count; i++)
        {
            CharacterTile characterTile = CreateTile(characterTileDatas[i], characterTilePrefab, container);
            tilesById.Add(characterTile.Id, characterTile);
            Tiles.Add(characterTile);
        }

        GameFlowManager.Instance.SetTilesById(tilesById);

        foreach (KeyValuePair<int, CharacterTile> item in tilesById)
        {
            List<CharacterTile> tileChildren = GetChildConnections(item.Value, tilesById);
            item.Value.Initialise(tileChildren);
        }

        foreach (KeyValuePair<int, CharacterTile> item in tilesById)
        {
            item.Value.SetInitialState();
        }
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
        LetterPickAction lastLetterPickAction = GameFlowManager.Instance.LetterPickActions[GameFlowManager.Instance.LetterPickActions.Count - 1];
        lastLetterPickAction.CharacterTile.SetCharacterTileState(CharacterTileState.Open);
        
        GameFlowManager.Instance.RemoveLastAction();

        string word = GameFlowManager.Instance.GetFormedWord();
        GameFlowManager.Instance.ValidationHandler.Validate(word.ToLower());
    }

    private CharacterTile CreateTile(CharacterTileDataModel characterTileData, GameObject characterTilePrefab, Transform container)
    {
        // todo we can use object pooling here
        GameObject characterTileGO = GameObject.Instantiate(characterTilePrefab, container);

        RectTransform tileRectTransform = characterTileGO.GetComponent<RectTransform>();

        Vector3 tilePosition = new Vector3((characterTileData.TilePosition.x * 12), characterTileData.TilePosition.y * 12, characterTileData.TilePosition.z);

        tileRectTransform.localPosition = tilePosition;

        CharacterTile characterTile = characterTileGO.GetComponent<CharacterTile>();
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

    public void ClearTiles()
    {
        Tiles.Clear();
    }
}