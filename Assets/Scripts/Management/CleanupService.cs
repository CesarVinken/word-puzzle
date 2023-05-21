using UnityEngine;

public class CleanupService : IGameService
{
    public void UnloadLevel()
    {
        GameFlowService gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
        gameFlowService.Unload();

        LevelUIController levelUIController = UIComponentLocator.Instance.Get<LevelUIController>();
        levelUIController.Unload();
    }

    public void UnloadTitleScreen()
    {
        TitleScreenController titleScreenController = UIComponentLocator.Instance.Get<TitleScreenController>();
        titleScreenController.Unload();
    }
}
