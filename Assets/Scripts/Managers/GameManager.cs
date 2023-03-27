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
    public LevelDataModel _currentLevelData = null;

    private void Awake()
    {
        Instance = this;

        _dataHandler = new DataHandler();

        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        // todo make async
        _dataHandler.Initialise();
        GameData = _dataHandler.GetGameData();
        UserData = _dataHandler.GetUserData();
    }

    public void ToLevel(LevelDataModel levelData)
    {
        ConsoleLog.Log(LogCategory.General, $"Load level {levelData.LevelNumber}: {levelData.Title}");
        
        _currentLevelData = levelData;
        SceneManager.LoadScene("Level");
    }

    public void ToLevelSelection()
    {
        _currentLevelData = null;
        SceneManager.LoadScene("Title");
    }
}
