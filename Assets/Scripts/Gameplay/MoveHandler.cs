using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler
{
    public void UseTile(CharacterTile characterTile)
    {
        new PlayerMove(characterTile).Execute();
    }

    public void UndoTile(CharacterTile characterTile)
    {
        // TO do
    }
}