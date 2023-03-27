using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionTileCreator
{
    [SerializeField] private GameObject _levelSelectionTilePrefab;
    [SerializeField] private Transform _tilesContainer;

    public LevelSelectionTileCreator(GameObject levelSelectionTilePrefab, Transform tilesContainer)
    {
        _levelSelectionTilePrefab = levelSelectionTilePrefab;
        _tilesContainer = tilesContainer;
    }

    public void CreateTiles()
    {
        List<LevelDataModel> levels = GameManager.Instance.GameData.Levels;

        for (int i = 0; i < levels.Count; i++)
        {
            ConsoleLog.Log(LogCategory.Data, $"Add tile for level {levels[i].LevelNumber} {levels[i].Title}");

            AddTile(levels[i]);
        }
    }

    private void AddTile(LevelDataModel levelData)
    {
        GameObject levelSelectionTileGO = GameObject.Instantiate(_levelSelectionTilePrefab, _tilesContainer);
        LevelSelectionTile levelSelectionTile = levelSelectionTileGO.GetComponent<LevelSelectionTile>();
        levelSelectionTile.Setup();
        levelSelectionTile.Initialise(levelData);
    }
}