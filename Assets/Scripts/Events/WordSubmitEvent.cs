using System;

public class WordSubmitEvent : EventArgs
{
    public WordSubmitAction WordPickAction { get; private set; }

    public WordSubmitEvent(WordSubmitAction wordPickAction)
    {
        WordPickAction = wordPickAction;
    }
}
