using System.Collections.Generic;

public class WordSubmitAction : IPlayerAction
{
    public FormedWord FormedWord { get; private set; }

    public WordSubmitAction(FormedWord formedWord)
    {
        FormedWord = formedWord;
    }

    public void Execute()
    {
        GameFlowManager.Instance.ExecuteWordSubmitEvent(this);
    }
}
