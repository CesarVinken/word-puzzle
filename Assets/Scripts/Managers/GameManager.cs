using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameDataModel GameData { get; private set; }
    public UserGameDataModel UserData { get; private set; }
    
    private DataHandler _dataHandler;
    public LevelDataModel CurrentLevelData { get; private set; } = null;

    private SceneType _firstScene; // the start-up scene. If we load the Level scene first in Unity we need to load a level

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
                _firstScene = SceneType.Title;
                break;
            case "Level":
                _firstScene = SceneType.Level;
                break;
            default:
                throw new NotImplementedException("SceneName", sceneName);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Start()
    {
        // todo make async
        _dataHandler.Initialise();
        GameData = _dataHandler.GetGameData();
        UserData = _dataHandler.GetUserData();

        if(_firstScene == SceneType.Level)
        {
            SetCurrentLevel(GameData.Levels[0]);

            if (LevelUIController.Instance == null)
            {
                ConsoleLog.Error(LogCategory.General, $"Could not find the Instance of the LevelUIController");
            }

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
        SetCurrentLevel(null);
        SceneManager.LoadScene("Title");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Do something when the scene is loaded
        Debug.Log("Scene " + scene.name + " loaded in mode " + mode.ToString());
    }
}
