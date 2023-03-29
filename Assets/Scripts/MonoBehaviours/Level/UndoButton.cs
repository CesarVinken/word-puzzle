using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour, ILevelUIButton
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
        _button.interactable = false;

        GameFlowManager.Instance.LetterPickEvent += OnLetterPickEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
        GameFlowManager.Instance.UndoEvent += OnUndoEvent;
    }

    public void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
        _button.interactable = true;
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        _button.interactable = false;
    }
    
    public void OnUndoEvent(object sender, UndoEvent e)
    {

        ConsoleLog.Log(LogCategory.General, $"todo");
    }


    public void OnClick()
    {
        GameFlowManager.Instance.MoveHandler.UndoTile();

        // if there are no picked letters left, make button not uninteractable
    }

}
