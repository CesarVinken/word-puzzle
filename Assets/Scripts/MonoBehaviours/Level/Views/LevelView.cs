using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour, ILevelScreenView
{

    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _submittedWordsText;

    [SerializeField] private CurrentScoreText _currentScoreText;
    [SerializeField] private WordScoreProjectionText _wordScoreProjection;
    [SerializeField] private FormedWordContainer _formedWordContainer;
    [SerializeField] private CharacterTileContainer _characterTileContainer;
    [SerializeField] private EndGamePanel _endGamePanel;

    [SerializeField] private UndoButton _undoButton;
    [SerializeField] private WordConfirmButton _wordConfirmButton;
    [SerializeField] private SettingsMenuButton _settingsMenuButton;

    public void Setup()
    {
        if (_levelNameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find level name text on {gameObject.name}");
        }
        if (_currentScoreText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _currentScoreText on {gameObject.name}");
        }
        if (_wordScoreProjection == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _wordScoreProjectionText on {gameObject.name}");
        }
        if (_submittedWordsText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _submittedWordsText on {gameObject.name}");
        }

        if (_formedWordContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _formedWordContainer on {gameObject.name}");
        }
        if (_characterTileContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _letterTileContainer on {gameObject.name}");
        }

        if (_undoButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _undoButton on {gameObject.name}");
        }
        if (_wordConfirmButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _wordConfirmButton on {gameObject.name}");
        }
        if (_settingsMenuButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _settingsMenuButton on {gameObject.name}");
        }

        _undoButton.Setup();
        _wordConfirmButton.Setup();
        _settingsMenuButton.Setup();

        _currentScoreText.Setup();
        _wordScoreProjection.Setup();
        _formedWordContainer.Setup();
        _characterTileContainer.Setup();
    }

    public void Initialise()
    {
        _currentScoreText.Initialise();
        _wordScoreProjection.Initialise();
        _formedWordContainer.Initialise();
        _characterTileContainer.Initialise();

        _undoButton.Initialise();
        _wordConfirmButton.Initialise();
        _settingsMenuButton.Initialise();

        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;

        _levelNameText.text = GameManager.Instance.CurrentLevelData.Title;
    }

    public void Unload()
    {
        GameFlowManager.Instance.WordSubmitEvent -= OnWordSubmitEvent;

        _formedWordContainer.Unload();
        _wordScoreProjection.Unload();
        _characterTileContainer.Unload();

        _undoButton.Unload();
        _wordConfirmButton.Unload();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowEndGamePanel()
    {
        _endGamePanel.gameObject.SetActive(true);
        _endGamePanel.Setup();
        _endGamePanel.Initialise();
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        _submittedWordsText.text += $"{e.WordPickAction.FormedWord.Word}\n";
    }
}
