using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionView : MonoBehaviour, ITitleScreenView
{
    [SerializeField] private GameObject _levelSelectionTilePrefab;
    private List<LevelSelectionTile> _levelSelectionTiles = new List<LevelSelectionTile>();

    [SerializeField] private Transform _tilesContainer;

    public void Setup()
    {
        if (_levelSelectionTilePrefab == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find levelSelectionTilePrefab");
        }

        if (_tilesContainer == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _tilesContainer");
        }
    }

    public void Initialise()
    {
        List<LevelDataModel> gameData = GameManager.Instance.GameData;
        
        for (int i = 0; i < gameData.Count; i++)
        {
            ConsoleLog.Log(LogCategory.Data, $"Add tile for level {gameData[i].LevelNumber} {gameData[i].Title}");

            AddTile(gameData[i]);
        }
    }

    public void AddTile(LevelDataModel levelData)
    {
        // Move to creator class
        GameObject levelSelectionTileGO = GameObject.Instantiate(_levelSelectionTilePrefab, _tilesContainer);
        LevelSelectionTile levelSelectionTile = levelSelectionTileGO.GetComponent<LevelSelectionTile>();
        levelSelectionTile.Setup();
        levelSelectionTile.Initialise(levelData);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
