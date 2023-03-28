using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public List<LetterPickAction> LastLetterPickActions = new List<LetterPickAction>();
    public Dictionary<int, CharacterTile> TilesById { get; private set; }  = new Dictionary<int, CharacterTile>();

    public PlayerActionHandler MoveHandler { get; private set; }

    public event EventHandler<LetterPickEvent> LetterPickEvent;
    public event EventHandler<WordSubmitEvent> WordSubmitEvent;
    public event EventHandler<WordValidatedEvent> WordValidatedEvent;


    public void Awake()
    {
        Instance = this;

        MoveHandler = new PlayerActionHandler();
    }

    public void Start()
    {
        LetterPickEvent += OnLetterPickEvent;
        WordSubmitEvent += OnWordSubmitEvent;
    }

    public void SetTilesById(Dictionary<int, CharacterTile> tilesById)
    {
        TilesById = tilesById;
    }

    public void RemoveLastAction()
    {
    // todo
    }

    private void ClearActions()
    {
        LastLetterPickActions.Clear();
    }

    public void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
        LastLetterPickActions.Add(e.LetterPickAction);
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        ClearActions();
    }

    public int GetCurrentWordScore()
    {
        return -1;
    }

    #region events

    public void ExecuteLetterPickEvent(LetterPickAction letterPickAction)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute LetterPickEvent");
        LetterPickEvent?.Invoke(this, new LetterPickEvent(letterPickAction));
    }

    public void ExecuteWordSubmitEvent(WordSubmitAction wordPickAction)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute WordSubmitEvent");
        WordSubmitEvent?.Invoke(this, new WordSubmitEvent(wordPickAction));
    } 
    
    public void ExecuteWordValidatedEvent()
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute WordValidatedEvent");
        WordValidatedEvent?.Invoke(this, new WordValidatedEvent());
    }

    #endregion
}

