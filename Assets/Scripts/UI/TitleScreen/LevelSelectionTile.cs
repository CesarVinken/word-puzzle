using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ToLevelButton _toLevelButton;
    [SerializeField] private GameObject _levelButtonLockedGO;
    [SerializeField] private GameObject _levelButtonAvailableGO;

    public LevelDataModel LevelData { get; private set; }
    public UserLevelDataModel UserLevelData { get; private set; }

    public void Setup()
    {
        if (_nameText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _nameText on {gameObject.name}");
        }
        if (_scoreText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _scoreText on {gameObject.name}");
        }
        if (_toLevelButton == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _toLevelButton on {gameObject.name}");
        }
        if (_levelButtonLockedGO == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _levelButtonLockedGO on {gameObject.name}");
        }
        if (_levelButtonAvailableGO == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _levelButtonAvailableGO on {gameObject.name}");
        }

        _toLevelButton.Setup(this);
    }

    public void Initialise(LevelDataModel levelData)
    {
        LevelData = levelData;
        UserLevelData = GameManager.Instance.UserData.Levels.FirstOrDefault(l => l.LevelNumber == LevelData.LevelNumber);

        if (UserLevelData == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find user level data for level {LevelData.LevelNumber}");
        }

        SetName(LevelData.LevelNumber, LevelData.Title);
        SetHighScore(UserLevelData.HighScore);
        SetPlayButton(LevelData.LevelNumber, UserLevelData.HighScore);
    }

    public void SetName(int levelNumber, string levelName)
    {
        _nameText.text = $"Level {levelNumber} - {levelName}";
    }
    
    public void SetHighScore(int highScore)
    {
        if(highScore == -1)
        {
            _scoreText.text = $"No score yet";
        }
        else
        {
            _scoreText.text = $"High score: {highScore}";
        }
    }

    private void SetPlayButton(int levelNumber, int highScore)
    {
        // A level is playable if it is the first level or when it has any high score
        if (highScore > -1 || levelNumber == 1)
        {
            _levelButtonAvailableGO.SetActive(true);
            _levelButtonLockedGO.SetActive(false);
        }
        else
        {
            _levelButtonAvailableGO.SetActive(false);
            _levelButtonLockedGO.SetActive(true);
        }
    }
}

public class LevelSelectionTileHandler
{
}
