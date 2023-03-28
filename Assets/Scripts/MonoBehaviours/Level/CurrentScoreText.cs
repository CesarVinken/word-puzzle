using TMPro;
using UnityEngine;

public class CurrentScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    public void Setup()
    {
        if (_currentScoreText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _currentScoreText");
        }
    }

    public void Initialise()
    {
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;

        SetText($"Score: 0");
    }

    private void SetText(string text)
    {
        _currentScoreText.text = text;
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent wordSubmitEvent)
    {
        int formedWordValue = wordSubmitEvent.WordPickAction.FormedWord.Value;
        int currentScore = GameFlowManager.Instance.CurrentScore;
        int newScore = currentScore + formedWordValue;
        GameFlowManager.Instance.SetCurrentScore(newScore);

        SetText($"Score: {newScore}");    
    }
}
