using TMPro;
using UnityEngine;

public class CurrentScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    private GameFlowService _gameFlowService;

    public void Setup()
    {
        if (_currentScoreText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _currentScoreText");
        }

        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
    }

    public void Initialise()
    {
        _gameFlowService.WordSubmitEvent += OnWordSubmitEvent;

        SetText($"Score: 0");
    }

    private void SetText(string text)
    {
        _currentScoreText.text = text;
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent wordSubmitEvent)
    {
        int formedWordValue = wordSubmitEvent.WordPickAction.FormedWord.Value;
        int currentScore = _gameFlowService.CurrentScore;
        int newScore = currentScore + formedWordValue;
        _gameFlowService.SetCurrentScore(newScore);

        SetText($"Score: {newScore}");    
    }
}
