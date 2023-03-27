using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _levelButtonLockedGO;
    [SerializeField] private GameObject _levelButtonAvailableGO;

    private LevelDataModel _levelData;
    private UserLevelDataModel _userLevelData;

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
        if (_playButton == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _playButton on {gameObject.name}");
        }
        if (_levelButtonLockedGO == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _levelButtonLockedGO on {gameObject.name}");
        }
        if (_levelButtonAvailableGO == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _levelButtonAvailableGO on {gameObject.name}");
        }
    }

    public void Initialise(LevelDataModel levelData)
    {
        _levelData = levelData;
        _userLevelData = GameManager.Instance.UserData.Levels.FirstOrDefault(l => l.LevelNumber == _levelData.LevelNumber);

        if (_userLevelData == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find user level data for level {_levelData.LevelNumber}");
        }

        SetName(_levelData.LevelNumber, _levelData.Title);
        SetHighScore(_userLevelData.HighScore);
        SetPlayButton(_levelData.LevelNumber, _userLevelData.HighScore);
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
