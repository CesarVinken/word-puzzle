using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public List<LetterPickAction> LastLetterPickActions = new List<LetterPickAction>();
    public Dictionary<int, CharacterTile> TilesById { get; private set; }  = new Dictionary<int, CharacterTile>();
    public int CurrentScore { get; private set; }

    public PlayerActionHandler MoveHandler { get; private set; }
    public ValidationHandler ValidationHandler { get; private set; }

    public event EventHandler<LetterPickEvent> LetterPickEvent;
    public event EventHandler<WordSubmitEvent> WordSubmitEvent;
    public event EventHandler<WordValidatedEvent> WordValidatedEvent;
    public event EventHandler<UndoEvent> UndoEvent;


    public void Awake()
    {
        Instance = this;

        MoveHandler = new PlayerActionHandler();
        ValidationHandler = new ValidationHandler();
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

        string word = GetFormedWord();
        ValidationHandler.Validate(word.ToLower());
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        ClearActions();
    }

    public void SetCurrentScore(int newScore)
    {
        CurrentScore = newScore;
    }

    public string GetFormedWord()
    {
        string word = "";
        for (int i = 0; i < LastLetterPickActions.Count; i++)
        {
            word += LastLetterPickActions[i].CharacterTile.CharacterTileData.Character;
        }

        return word;
    }

    public int GetCurrentWordScore(string word)
    {
        int score = 0;

        for (int i = 0; i < word.Length; i++)
        {
            string character = word[i].ToString();
            score += CharacterUtility.GetCharacterValue(character);
        }

        return score * (word.Length * 10);
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
    
    public void ExecuteWordValidatedEvent(bool isValid)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute WordValidatedEvent");
        WordValidatedEvent?.Invoke(this, new WordValidatedEvent(isValid));
    }

    public void ExecuteUndoEvent(UndoAction undoAction)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute Word UndoEvent");
        UndoEvent?.Invoke(this, new UndoEvent(undoAction));
    }

    #endregion
}

