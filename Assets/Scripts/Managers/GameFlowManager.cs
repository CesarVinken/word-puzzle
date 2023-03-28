using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public List<PlayerMove> LastPlayerMoves = new List<PlayerMove>();
    public Dictionary<int, CharacterTile> TilesById { get; private set; }  = new Dictionary<int, CharacterTile>();

    public MoveHandler MoveHandler { get; private set; }

    public event EventHandler<PlayerMoveEvent> PlayerMoveEvent;


    public void Awake()
    {
        Instance = this;

        MoveHandler = new MoveHandler();
    }

    public void Start()
    {
        PlayerMoveEvent += OnPlayerMoveEvent;
    }

    public void SetTilesById(Dictionary<int, CharacterTile> tilesById)
    {
        TilesById = tilesById;
    }

    //public void AddAction(PlayerMove useCharacterTileAction)
    //{
    //}

    public void RemoveLastAction()
    {
    // todo

    }

    public void ClearActions()
    {
        LastPlayerMoves.Clear();
    }

    public void OnPlayerMoveEvent(object sender, PlayerMoveEvent e)
    {
        LastPlayerMoves.Add(e.PlayerMove);
    }

    public void ExecutePlayerMoveEvent(PlayerMove playerMove)
    {
        PlayerMoveEvent?.Invoke(this, new PlayerMoveEvent(playerMove));
    }
}

public class PlayerMoveEvent : EventArgs
{
    public PlayerMove PlayerMove { get; private set; }

    public PlayerMoveEvent(PlayerMove playerMove)
    {
        PlayerMove = playerMove;
    }
}
