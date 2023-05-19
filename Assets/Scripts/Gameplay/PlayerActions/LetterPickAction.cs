public class LetterPickAction : IPlayerAction
{
    public CharacterTile CharacterTile { get; private set; }

    public LetterPickAction(CharacterTile characterTile)
    {
        CharacterTile = characterTile;
    }

    public void Execute()
    {
        GameFlowService gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();

        gameFlowService.ExecuteLetterPickEvent(this);

        CharacterTile.SetCharacterTileState(CharacterTileState.Used);
    }
}
