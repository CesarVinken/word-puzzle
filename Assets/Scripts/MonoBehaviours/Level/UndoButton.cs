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
        SetButtonInteractability(false);

        GameFlowManager.Instance.LetterPickEvent += OnLetterPickEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void Unload()
    {
        GameFlowManager.Instance.LetterPickEvent -= OnLetterPickEvent;
        GameFlowManager.Instance.WordSubmitEvent -= OnWordSubmitEvent;
    }

    public void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
        SetButtonInteractability(true);
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        SetButtonInteractability(false);
    }

    private void SetButtonInteractability(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }
   
    public void OnClick()
    {
        // if after the undo action there are 0 actions left, then the button should not be interactable
        if (GameFlowManager.Instance.LetterPickActions.Count < 2) 
        {
            SetButtonInteractability(false);
        }

        GameFlowManager.Instance.MoveHandler.UndoTile();
    }

}
