using System.Collections.Generic;
using UnityEngine;

public class ValidationHandler
{
    private GameFlowService _gameFlowService;

    public ValidationHandler()
    {
        _gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
    }

    public void Validate(string word)
    {
        bool isValid = false;

        if(word.Length > 0) 
        { 
            List<string> letterDictionary = GameManager.Instance.WordDictionary[word[0]];

            if (letterDictionary.Contains(word))
            {
                isValid = true;
            }
        }

        _gameFlowService.ExecuteWordValidatedEvent(isValid);
    }
}
