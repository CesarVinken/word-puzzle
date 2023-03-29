using System.Collections.Generic;
using UnityEngine;

public class ValidationHandler
{
    public void Validate(string word)
    {
        bool isValid = false;
        List<string> letterDictionary = GameManager.Instance.WordDictionary[word[0]];

        if (letterDictionary.Contains(word))
        {
            isValid = true;
        }

        GameFlowManager.Instance.ExecuteWordValidatedEvent(isValid);
    }
}
