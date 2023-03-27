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
        //temporary
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
        AddTile();
    }

    public void AddTile()
    {
        // Move to creator class
        GameObject levelSelectionTileGO = GameObject.Instantiate(_levelSelectionTilePrefab, _tilesContainer);
        LevelSelectionTile levelSelectionTile = levelSelectionTileGO.GetComponent<LevelSelectionTile>();
        levelSelectionTile.Setup();
        levelSelectionTile.Initialise();
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
