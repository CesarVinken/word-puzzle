using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    public static LevelUIController Instance;

    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _wordScoreProjectText;

    [SerializeField] private FormedWordContainer _formedWordContainer;
    [SerializeField] private LetterTileContainer _letterTileContainer;

    [SerializeField] private UndoButton _undoButton;
    [SerializeField] private WordConfirmButton _wordConfirmButton;

    [SerializeField] private TemporaryVictoryButton _temporaryVictoryButton;

    public void Awake()
    {
        if (_levelNameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find level name text on {gameObject.name}");
        }
        if (_currentScoreText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _currentScoreText on {gameObject.name}");
        }
        if (_wordScoreProjectText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _wordScoreProjectText on {gameObject.name}");
        }

        if (_formedWordContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _formedWordContainer on {gameObject.name}");
        }
        if (_letterTileContainer == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _letterTileContainer on {gameObject.name}");
        }

        if (_undoButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _undoButton on {gameObject.name}");
        }
        if (_wordConfirmButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not _wordConfirmButton on {gameObject.name}");
        }

        Instance = this;

        _undoButton.Setup();
        _wordConfirmButton.Setup();
        _temporaryVictoryButton.Setup();
    }

    public void Start()
    {
        if(GameManager.Instance.CurrentLevelData != null) // if we open the Level scene from the Unity inspector, initialisation is triggered through the GameManager
        {
            Initialise();
        }
    }

    public void Initialise()
    {
        _levelNameText.text = GameManager.Instance.CurrentLevelData.Title;

        _undoButton.Initialise();
        _wordConfirmButton.Initialise();
        _temporaryVictoryButton.Initialise();
    }

    public void ToLevelSelection()
    {
        GameManager.Instance.ToLevelSelection();
    }
}
