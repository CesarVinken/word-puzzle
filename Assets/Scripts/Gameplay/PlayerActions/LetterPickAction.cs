public class LetterPickAction : IPlayerAction
{
    public CharacterTile CharacterTile { get; private set; }

    public LetterPickAction(CharacterTile characterTile)
    {
        CharacterTile = characterTile;
    }

    public void Execute()
    {
        GameFlowManager.Instance.ExecuteLetterPickEvent(this);

        CharacterTile.SetCharacterTileState(CharacterTileState.Used);
    }
}
