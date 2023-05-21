using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _scoreCalculationText;
    [SerializeField] private Button _button;

    private GameFlowService _gameFlowService;

    private int _totalScore = 0;
    private int _currentHighScore = 0;

    public void Setup()
    {
        if(_highScoreText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find high schore text");
        }
        if(_scoreCalculationText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _scoreCalculation text");
        }
        if(_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find _button text");
        }

        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        SetHighScoreText();
        SetScoreCalculationText();
    }

    private void SetHighScoreText()
    {
        _currentHighScore = GetCurrentHighScore();

        if (_currentHighScore > 0)
        {
            _highScoreText.text = $"Current high score: {_currentHighScore}";
        }
        else
        {
            _highScoreText.text = $"";
        }
    }

    private void SetScoreCalculationText()
    {
        GameFlowService gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
        int levelScore = gameFlowService.CurrentScore;
        int tilesPenalty = GetTilesPenalty();
        _totalScore = levelScore - tilesPenalty;

        string tilesPenaltyString = tilesPenalty == 0 ? "0" : $"-{tilesPenalty}";

        _scoreCalculationText.text = $"Level score: {levelScore}\nTiles remaining: {tilesPenaltyString}\n--------\nTotal Score: {_totalScore}";
    }

    private int GetTilesPenalty()
    {
        int levelScore = _gameFlowService.CurrentScore;
        int tilesLeft = CharacterTileHandler.Tiles.Where(t => t.State != CharacterTileState.Used).Count();
        int penaltyPerTile = 100;

        int tilesPenalty = tilesLeft * penaltyPerTile;

        return tilesPenalty;
    }

    private int GetCurrentHighScore()
    {
        int currentLevelId = GameManager.Instance.CurrentLevelData.LevelNumber;
        UserLevelDataModel userLevelData = GameManager.Instance.UserData.Levels[currentLevelId - 1];
        return userLevelData.HighScore;
    }

    public void OnClick()
    {
        LevelUIController levelUIController = UIComponentLocator.Instance.Get<LevelUIController>();

        if (_totalScore > _currentHighScore)
        {
            GameManager.Instance.SaveNewHighScore(_totalScore);
            levelUIController.ToCelebration();
        }
        else
        {
            levelUIController.ToLevelSelection();
        }
    }
}
