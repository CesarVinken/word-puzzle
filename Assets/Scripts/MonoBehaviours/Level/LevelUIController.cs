using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    public static LevelUIController Instance;

    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _submittedWordsText;

    [SerializeField] private CurrentScoreText _currentScoreText;
    [SerializeField] private WordScoreProjectionText _wordScoreProjection;
    [SerializeField] private FormedWordContainer _formedWordContainer;
    [SerializeField] private CharacterTileContainer _characterTileContainer;

    [SerializeField] private UndoButton _undoButton;
    [SerializeField] private WordConfirmButton _wordConfirmButton;
    [SerializeField] private SettingsMenuButton _settingsMenuButton;

    public void Awake()
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

        Instance = this;

        _undoButton.Setup();
        _wordConfirmButton.Setup();
        _settingsMenuButton.Setup();

        _currentScoreText.Setup();
        _wordScoreProjection.Setup();
        _formedWordContainer.Setup();
        _characterTileContainer.Setup();

        if (GameManager.Instance.CurrentLevelData != null) // if we open the Level scene from the Unity inspector, initialisation is triggered through the GameManager
        {
            Initialise();
        }
    }


    public void Initialise()
    {
        _levelNameText.text = GameManager.Instance.CurrentLevelData.Title;

        _currentScoreText.Initialise();
        _undoButton.Initialise();
        _wordConfirmButton.Initialise();
        _settingsMenuButton.Initialise();

        _wordScoreProjection.Initialise();
        _formedWordContainer.Initialise();
        _characterTileContainer.Initialise();

        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void ToLevelSelection()
    {
        GameManager.Instance.ToLevelSelection();
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        _submittedWordsText.text += $"{e.WordPickAction.FormedWord.Word}\n";
    }
}
