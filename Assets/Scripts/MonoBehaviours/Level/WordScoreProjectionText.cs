using TMPro;
using UnityEngine;

public class WordScoreProjectionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wordScoreProjectionText;

    public void Setup()
    {
        if(_wordScoreProjectionText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _wordScoreProjectionText");
        }
    }

    public void Initialise()
    {
        GameFlowManager.Instance.WordValidatedEvent += OnWordValidatedEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
    }

    private void SetText(string newText)
    {
        _wordScoreProjectionText.text = newText;
    }

    public void OnWordValidatedEvent(object sender, WordValidatedEvent e)
    {
        if (e.IsValid)
        {
            string word = GameFlowManager.Instance.GetFormedWord();
            int score = GameFlowManager.Instance.GetCurrentWordScore(word);
            SetText($"Word Score {score}");
        }
        else
        {
            SetText($"");

        }
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        SetText($"");
    }
}
