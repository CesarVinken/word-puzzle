using System;
using UnityEngine;

public class WordValidatedEvent : EventArgs
{
    public bool IsValid { get; private set; }

    public WordValidatedEvent(bool isValid)
    {
        IsValid = isValid;
    }
}
