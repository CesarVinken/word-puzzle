using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameDataModel GameData { get; private set; }
    public UserGameDataModel UserData { get; private set; }

    private DataHandler _dataHandler;
    public LevelDataModel CurrentLevelData { get; private set; } = null;

    public Dictionary<char, List<string>> WordDictionary { get; private set; }
    public SceneType PreviousScene {get; private set; } // the start-up scene. If we load the Level scene first in Unity we need to load a level

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        UIComponentLocator.Setup();

        ServiceLocator.Setup();
        ServiceLocator.Instance.Register<GameFlowService>(new GameFlowService());
        ServiceLocator.Instance.Register<CleanupService>(new CleanupService());

        _dataHandler = new DataHandler();

        DontDestroyOnLoad(this);

        PreviousScene = SceneType.None;
    }

    public void Start()
    {
        GameFlowService gameFlowService = ServiceLocator.Instance.Get<GameFlowService>();
        gameFlowService.Setup();
        gameFlowService.Initialise();


        _dataHandler.Initialise();
        GameData = _dataHandler.GetGameData();
        UserData = _dataHandler.GetUserData();
        WordDictionary = _dataHandler.GetDictionaryData();

        string sceneName = SceneManager.GetActiveScene().name;
        if (PreviousScene == SceneType.None && sceneName == "Level") // this means this is a start up of the Level scene directly in Unity and we never selected a current level in the menu
        {
            SetCurrentLevel(GameData.Levels[0]);
        }
    }

    private void SetCurrentLevel(LevelDataModel levelData)
    {
        CurrentLevelData = levelData;
    }

    public void ToLevel(LevelDataModel levelData)
    {
        CleanupService cleanupService = ServiceLocator.Instance.Get<CleanupService>();
        cleanupService.UnloadTitleScreen();

        ConsoleLog.Log(LogCategory.General, $"Load level {levelData.LevelNumber}: {levelData.Title}");

        PreviousScene = SceneType.Title;
        SetCurrentLevel(levelData);

        SceneManager.LoadScene("Level");
    }

    public void ToLevelSelection()
    {
        CleanupService cleanupService = ServiceLocator.Instance.Get<CleanupService>();
        cleanupService.UnloadLevel();

        PreviousScene = SceneType.Level;

        SetCurrentLevel(null);
        SceneManager.LoadScene("Title");
    }

    public void SaveNewHighScore(int highScore)
    {
        UserData.Levels[CurrentLevelData.LevelNumber - 1].SetHighScore(highScore);
        _dataHandler.SaveUserLevelData();
    }
}
