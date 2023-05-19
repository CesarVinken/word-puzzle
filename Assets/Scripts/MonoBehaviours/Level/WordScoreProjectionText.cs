using TMPro;
using UnityEngine;

public class WordScoreProjectionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wordScoreProjectionText;
    private GameFlowService _gameFlowService;

    public void Setup()
    {
        if(_wordScoreProjectionText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _wordScoreProjectionText");
        }

        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
    }

    public void Initialise()
    {
        _gameFlowService.WordValidatedEvent += OnWordValidatedEvent;
        _gameFlowService.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void Unload()
    {
        _gameFlowService.WordValidatedEvent -= OnWordValidatedEvent;
        _gameFlowService.WordSubmitEvent -= OnWordSubmitEvent;
    }

    private void SetText(string newText)
    {
        _wordScoreProjectionText.text = newText;
    }

    private void OnWordValidatedEvent(object sender, WordValidatedEvent e)
    {
        if (e.IsValid)
        {
            string word = _gameFlowService.GetFormedWord();
            int score = _gameFlowService.GetCurrentWordScore(word);
            SetText($"Word Score {score}");
        }
        else
        {
            SetText($"");
        }
    }

    private void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        SetText($"");
    }
}
