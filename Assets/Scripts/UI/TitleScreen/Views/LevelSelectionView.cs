using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionView : MonoBehaviour, ITitleScreenView
{
    private GameObject _levelSelectionTilePrefab;
    private List<LevelSelectionTile> _levelSelectionTiles = new List<LevelSelectionTile>();

    public void Setup()
    {
        if(_levelSelectionTilePrefab == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find levelSelectionTilePrefab");
        }
    }

    public void Initialise()
    {
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
