using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameFlowService : IGameService
{
    public List<LetterPickAction> LetterPickActions = new List<LetterPickAction>();
    public Dictionary<int, CharacterTile> TilesById { get; private set; }  = new Dictionary<int, CharacterTile>();
    public int CurrentScore { get; private set; }

    public PlayerActionHandler MoveHandler { get; private set; }
    public ValidationHandler ValidationHandler { get; private set; }
    private FormableWordChecker _formableWordChecker;

    public event EventHandler<LetterPickEvent> LetterPickEvent;
    public event EventHandler<WordSubmitEvent> WordSubmitEvent;
    public event EventHandler<WordValidatedEvent> WordValidatedEvent;
    public event EventHandler<UndoEvent> UndoEvent;

    public void Setup()
    {
        MoveHandler = new PlayerActionHandler();
        ValidationHandler = new ValidationHandler();
        _formableWordChecker = new FormableWordChecker();
    }

    public void Initialise()
    {
        LetterPickEvent += OnLetterPickEvent;
        WordSubmitEvent += OnWordSubmitEvent;
    }

    public void Unload()
    {
        LetterPickEvent -= OnLetterPickEvent;
        WordSubmitEvent -= OnWordSubmitEvent;
    }

    public void SetTilesById(Dictionary<int, CharacterTile> tilesById)
    {
        TilesById = tilesById;
    }

    public void RemoveLastAction()
    {
        if (LetterPickActions.Count == 0) return;

        LetterPickActions.RemoveAt(LetterPickActions.Count - 1);
    }

    private void ClearActions()
    {
        LetterPickActions.Clear();
    }

    public void OnLetterPickEvent(object sender, LetterPickEvent e)
    {
        LetterPickActions.Add(e.LetterPickAction);

        string word = GetFormedWord();
        ValidationHandler.Validate(word);
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        ClearActions();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        bool formableWordLeft = CheckForLevelEnd();

        if (!formableWordLeft)
        {
            LevelUIController.Instance.ShowEndGamePanel();
        }

        stopwatch.Stop();
        ConsoleLog.Log(LogCategory.General, $"Check took {stopwatch.ElapsedMilliseconds} milliseconds");
    }

    public void SetCurrentScore(int newScore)
    {
        CurrentScore = newScore;
    }

    public string GetFormedWord()
    {
        string word = "";
        for (int i = 0; i < LetterPickActions.Count; i++)
        {
            word += LetterPickActions[i].CharacterTile.CharacterTileData.Character;
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

    private bool CheckForLevelEnd()
    {
        bool formableWordLeft = _formableWordChecker.FindFormableWord();
        return formableWordLeft;
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
