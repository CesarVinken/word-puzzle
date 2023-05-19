using UnityEngine;

public class PlayerActionHandler
{
    private GameFlowService _gameFlowService;

    public PlayerActionHandler()
    {
        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
    }

    public void UseTile(CharacterTile characterTile)
    {
        new LetterPickAction(characterTile).Execute();
    }

    public void UndoTile()
    {
        new UndoAction().Execute();
    }

    public void SubmitWord()
    {
        string word = _gameFlowService.GetFormedWord();;
        int wordValue = _gameFlowService.GetCurrentWordScore(word);

        FormedWord formedWord = new FormedWord(word, wordValue);

        new WordSubmitAction(formedWord).Execute();
    }
}
