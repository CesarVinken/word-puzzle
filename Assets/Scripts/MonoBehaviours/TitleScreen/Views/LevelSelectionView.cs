using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionView : MonoBehaviour, ITitleScreenView
{
    [SerializeField] private GameObject _levelSelectionTilePrefab;

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
        LevelSelectionTileCreator levelSelectionTileCreator = new LevelSelectionTileCreator(_levelSelectionTilePrefab, _tilesContainer);
        levelSelectionTileCreator.CreateTiles();
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
