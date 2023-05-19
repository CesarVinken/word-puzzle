using System.Collections.Generic;
using UnityEngine;

public class FormedWordContainer : MonoBehaviour
{
    [SerializeField] private List<FormedWordCharacterTile> _formedWordCharacters = new List<FormedWordCharacterTile>();

    private FormedWordHandler _formedWordHandler;
    private GameFlowService _gameFlowService;

    public void Setup()
    {
        if(_formedWordCharacters.Count == 0)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could nog find any slots to form a word");
        }

        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();

        for (int i = 0; i < _formedWordCharacters.Count; i++)
        {
            _formedWordCharacters[i].Setup(null);
        }
    }

    public void Initialise()
    {
        _formedWordHandler = new FormedWordHandler(_formedWordCharacters);

        _gameFlowService.LetterPickEvent += OnLetterPickEvent;
        _gameFlowService.WordSubmitEvent += OnWordSubmitEvent;
        _gameFlowService.UndoEvent += OnUndoEvent;
    }

    public void Unload()
    {
        _gameFlowService.LetterPickEvent -= OnLetterPickEvent;
        _gameFlowService.WordSubmitEvent -= OnWordSubmitEvent;
        _gameFlowService.UndoEvent -= OnUndoEvent;
    }

    private void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
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
