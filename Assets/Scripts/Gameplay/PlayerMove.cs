using UnityEngine;

public class PlayerMove
{
    public CharacterTile CharacterTile { get; private set; }

    public PlayerMove(CharacterTile characterTile)
    {
        CharacterTile = characterTile;
    }

    public void Execute()
    {
        GameFlowManager.Instance.ExecutePlayerMoveEvent(this);

        CharacterTile.SetCharacterTileState(CharacterTileState.Used);
        //GameFlowManager.Instance.AddAction(this);
    }
}
