using UnityEngine;

public class UndoAction : IPlayerAction
{
    public void Execute()
    {
        GameFlowManager.Instance.ExecuteUndoEvent(this);
    }
}
