using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionView : MonoBehaviour, ITitleScreenView
{
    [SerializeField] private Transform _tilesContainer;

    public void Setup()
    {

        if (_tilesContainer == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _tilesContainer");
        }
    }

    public void Initialise()
    {
        CreateTiles();
    }

    private void CreateTiles()
    {
        List<LevelDataModel> levels = GameManager.Instance.GameData.Levels;

        for (int i = 0; i < levels.Count; i++)
        {
            ConsoleLog.Log(LogCategory.Data, $"Add tile for level {levels[i].LevelNumber} {levels[i].Title}", LogPriority.Low);
            LevelSelectionTileFactory.Create(_tilesContainer, levels[i]);
        }
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
