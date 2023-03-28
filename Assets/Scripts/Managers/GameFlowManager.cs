using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public List<PlayerMove> LastUseActions = new List<PlayerMove>();
    public Dictionary<int, CharacterTile> TilesById { get; private set; }  = new Dictionary<int, CharacterTile>();

    public MoveHandler MoveHandler { get; private set; }

    public void Awake()
    {
        Instance = this;

        MoveHandler = new MoveHandler();
    }

    public void SetTilesById(Dictionary<int, CharacterTile> tilesById)
    {
        TilesById = tilesById;
    }

    public void AddAction(PlayerMove useCharacterTileAction)
    {
        LastUseActions.Add(useCharacterTileAction);
    }

    public void RemoveLastAction()
    {
    // todo

    }

    public void ClearActions()
    {
        LastUseActions.Clear();
    }
}
