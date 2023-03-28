using UnityEngine;

public class ValidationHandler
{
    public void Validate(string word)
    {
        bool isValid = true;
        GameFlowManager.Instance.ExecuteWordValidatedEvent(isValid);
    }
}
