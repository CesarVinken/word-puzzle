using UnityEngine;
using UnityEngine.UI;

public class WordConfirmButton : MonoBehaviour, ILevelUIButton
{
    [SerializeField] private Button _button;
    private GameFlowService _gameFlowService;

    public void Setup()
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        _button.interactable = false;

        _gameFlowService.WordValidatedEvent += OnWordValidatedEvent;
        _gameFlowService.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void Unload()
    {
        _gameFlowService.WordValidatedEvent -= OnWordValidatedEvent;
        _gameFlowService.WordSubmitEvent -= OnWordSubmitEvent;
    }

    public void OnWordValidatedEvent(object sender, WordValidatedEvent e)
    {
        _button.interactable = e.IsValid;
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        _button.interactable = false;
    }


    public void OnClick()
    {
        _gameFlowService.MoveHandler.SubmitWord();
    }
}
