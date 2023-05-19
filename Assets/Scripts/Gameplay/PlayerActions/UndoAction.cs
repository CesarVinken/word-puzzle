using UnityEngine;

public class UndoAction : IPlayerAction
{
    public void Execute()
    {
        GameFlowService gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
        gameFlowService.ExecuteUndoEvent(this);
    }
}
