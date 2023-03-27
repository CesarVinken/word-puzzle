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
        SetName(levelData.LevelNumber, levelData.Title);
        SetHighScore(1000);
    }

    public void SetName(int levelNumber, string levelName)
    {
        _nameText.text = $"Level {levelNumber} - {levelName}";
    }
    
    public void SetHighScore(int score)
    {
        if(score == -1)
        {
            _scoreText.text = $"No score yet";
        }
        else
        {
            _scoreText.text = $"High score: {score}";
        }
    }
}
