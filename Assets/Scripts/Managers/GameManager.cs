using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool LevelDirectlyFromInspector = false;
    public GameDataModel GameData { get; private set; }
    public UserGameDataModel UserData { get; private set; }

    private DataHandler _dataHandler;
    public LevelDataModel CurrentLevelData { get; private set; } = null;

    public Dictionary<char, List<string>> WordDictionary { get; private set; }
    public SceneType FirstScene {get; private set; } // the start-up scene. If we load the Level scene first in Unity we need to load a level

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _dataHandler = new DataHandler();

        DontDestroyOnLoad(this);

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Title":
                LevelDirectlyFromInspector = false;
                FirstScene = SceneType.Title;
                break;
            case "Level":
                LevelDirectlyFromInspector = true;
                FirstScene = SceneType.Level;
                break;
            default:
                throw new NotImplementedException("SceneName", sceneName);
        }
    }

    public void Start()
    {
        // todo make async
        _dataHandler.Initialise();
        GameData = _dataHandler.GetGameData();
        UserData = _dataHandler.GetUserData();
        WordDictionary = _dataHandler.GetDictionaryData();

        if(FirstScene == SceneType.Level) // this means this is a start up from the unity Level scene and we never selected a current level in the menu
        {
            SetCurrentLevel(GameData.Levels[0]);

            if (LevelUIController.Instance == null)
            {
                ConsoleLog.Error(LogCategory.General, $"Could not find the Instance of the LevelUIController");
            }

            LevelUIController.Instance.Setup();
            LevelUIController.Instance.Initialise();
        }
    }

    private void SetCurrentLevel(LevelDataModel levelData)
    {
        CurrentLevelData = levelData;
    }

    public void ToLevel(LevelDataModel levelData)
    {
        ConsoleLog.Log(LogCategory.General, $"Load level {levelData.LevelNumber}: {levelData.Title}");

        SetCurrentLevel(levelData);

        SceneManager.LoadScene("Level");
    }

    public void ToLevelSelection()
    {
        LevelDirectlyFromInspector = false;

        SetCurrentLevel(null);
        SceneManager.LoadScene("Title");
    }
}
