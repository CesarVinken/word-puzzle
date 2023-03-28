using System;

public class LetterPickEvent : EventArgs
{
    public LetterPickAction LetterPickAction { get; private set; }

    public LetterPickEvent(LetterPickAction letterPickAction)
    {
        LetterPickAction = letterPickAction;
    }
}
