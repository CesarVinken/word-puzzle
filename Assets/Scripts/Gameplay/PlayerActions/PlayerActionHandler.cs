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
        List<LetterPickAction> letterPickActions = GameFlowManager.Instance.LastLetterPickActions;

        string word = "";
        for (int i = 0; i < letterPickActions.Count; i++)
        {
            //characters.Add(new FormedWordCharacter(letterPickActions[i].CharacterTile.CharacterTileData.Character));
            word += letterPickActions[i].CharacterTile.CharacterTileData.Character;
        }

        int wordValue = GameFlowManager.Instance.GetCurrentWordScore();

        FormedWord formedWord = new FormedWord(word, wordValue);

        new WordSubmitAction(formedWord).Execute();
    }
}
