using System.Collections.Generic;
using UnityEngine;
public class CharacterTileHandler
{
    public void PopulateLevel(Transform container)
    {
        List<CharacterTileDataModel> characterTileDatas = GameManager.Instance.CurrentLevelData.LetterTiles;
        GameObject characterTilePrefab = AssetManager.Instance.GetCharacterTilePrefab();

        for (int i = 0; i < characterTileDatas.Count; i++)
        {
            CreateTile(characterTileDatas[i], characterTilePrefab, container);
        }
    }

    private void CreateTile(CharacterTileDataModel characterTileData, GameObject characterTilePrefab, Transform container)
    {
        // todo we can use object pooling here
        GameObject characterTileGO = GameObject.Instantiate(characterTilePrefab, container);

        RectTransform tileRectTransform = characterTileGO.GetComponent<RectTransform>();

        Vector3 tilePosition = new Vector3((characterTileData.TilePosition.x * 12), characterTileData.TilePosition.y * 12, characterTileData.TilePosition.z);
        tileRectTransform.localPosition = tilePosition;

        CharacterTile characterTile = characterTileGO.GetComponent<CharacterTile>();
        characterTile.Setup(characterTileData);
        characterTile.Initialise();
    }
}