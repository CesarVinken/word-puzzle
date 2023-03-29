using System;
using UnityEngine;

public class UndoEvent : EventArgs
{
    public UndoAction UndoAction { get; private set; }

    public UndoEvent(UndoAction undoAction)
    {
        UndoAction = undoAction;
    }
}
