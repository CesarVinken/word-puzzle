using System.Collections.Generic;
using UnityEngine;

public class FormedWordContainer : MonoBehaviour
{
    private FormedWordHandler _formedWordHandler;
    [SerializeField] private List<FormedWordCharacterTile> _formedWordCharacters = new List<FormedWordCharacterTile>();

    public void Setup()
    {
        ConsoleLog.Warning(LogCategory.General, $"Setup");
        if(_formedWordCharacters.Count == 0)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could nog find any slots to form a word");
        }

        for (int i = 0; i < _formedWordCharacters.Count; i++)
        {
            _formedWordCharacters[i].Setup(null);
        }
    }

    public void Initialise()
    {
        ConsoleLog.Warning(LogCategory.General, $"Initialise. {_formedWordCharacters.Count} _formedWordCharacters");
        _formedWordHandler = new FormedWordHandler(_formedWordCharacters);

        GameFlowManager.Instance.LetterPickEvent += OnLetterPickEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
        GameFlowManager.Instance.UndoEvent += OnUndoEvent;
    }

    public void Unload()
    {
        GameFlowManager.Instance.LetterPickEvent -= OnLetterPickEvent;
        GameFlowManager.Instance.WordSubmitEvent -= OnWordSubmitEvent;
        GameFlowManager.Instance.UndoEvent -= OnUndoEvent;
    }

    private void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
        ConsoleLog.Warning(LogCategory.General, $"FillNextSlot!");
        _formedWordHandler.FillNextSlot(e.LetterPickAction.CharacterTile);
    }

    private void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        _formedWordHandler.RemoveFormedWord();
    }

    private void OnUndoEvent(object sender, UndoEvent e)
    {
        _formedWordHandler.RemoveLastLetter();
    }
}
