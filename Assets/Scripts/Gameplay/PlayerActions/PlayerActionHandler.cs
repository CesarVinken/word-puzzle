using UnityEngine;

public class PlayerActionHandler
{
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
        string word = GameFlowManager.Instance.GetFormedWord();;
        int wordValue = GameFlowManager.Instance.GetCurrentWordScore(word);

        FormedWord formedWord = new FormedWord(word, wordValue);

        new WordSubmitAction(formedWord).Execute();
    }
}
