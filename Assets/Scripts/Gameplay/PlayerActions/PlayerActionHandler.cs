using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionHandler
{
    public void UseTile(CharacterTile characterTile)
    {
        new LetterPickAction(characterTile).Execute();
    }

    public static void UndoTile(CharacterTile characterTile)
    {
        // TO do
    }

    public void SubmitWord()
    {
        string word = GameFlowManager.Instance.GetFormedWord();;
        int wordValue = GameFlowManager.Instance.GetCurrentWordScore(word);

        FormedWord formedWord = new FormedWord(word, wordValue);

        new WordSubmitAction(formedWord).Execute();
    }
}
