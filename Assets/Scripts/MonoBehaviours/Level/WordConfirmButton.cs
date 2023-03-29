using UnityEngine;
using UnityEngine.UI;

public class WordConfirmButton : MonoBehaviour, ILevelUIButton
{
    [SerializeField] private Button _button;

    public void Setup()
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        GameFlowManager.Instance.WordValidatedEvent += OnWordValidatedEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void Unload()
    {
        GameFlowManager.Instance.WordValidatedEvent -= OnWordValidatedEvent;
        GameFlowManager.Instance.WordSubmitEvent -= OnWordSubmitEvent;
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
        GameFlowManager.Instance.MoveHandler.SubmitWord();
    }
}
