using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _scoreCalculationText;
    [SerializeField] private Button _button;

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
        int highScore = GetHighScore();

        if (highScore > 0)
        {
            _highScoreText.text = $"Current high score: {highScore}";
        }
        else
        {
            _highScoreText.text = $"";
        }
    }

    private void SetScoreCalculationText()
    {
        int levelScore = GameFlowManager.Instance.CurrentScore;
        int tilesPenalty = GetTilesPenalty();
        int totalScore = levelScore - tilesPenalty;

        string tilesPenaltyString = tilesPenalty == 0 ? "0" : $"-{tilesPenalty}";

        _scoreCalculationText.text = $"Level score: {levelScore}\nTiles remaining: {tilesPenaltyString}\n--------\nTotal Score: {totalScore}";
    }

    private int GetTilesPenalty()
    {
        int levelScore = GameFlowManager.Instance.CurrentScore;
        int tilesLeft = CharacterTileHandler.Tiles.Where(t => t.State != CharacterTileState.Used).Count();
        int penaltyPerTile = 100;

        int tilesPenalty = tilesLeft * penaltyPerTile;

        return tilesPenalty;
    }

    private int GetHighScore()
    {
        int currentLevelId = GameManager.Instance.CurrentLevelData.LevelNumber;
        UserLevelDataModel userLevelData = GameManager.Instance.UserData.Levels[currentLevelId - 1];
        return userLevelData.HighScore;
    }

    public void OnClick()
    {
        ConsoleLog.Log(LogCategory.General, $"to do: save high score or unlock level");
        LevelUIController.Instance.ToLevelSelection();
    }
}
