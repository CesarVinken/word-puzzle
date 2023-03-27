using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameDataModel GameData { get; private set; }
    public UserGameDataModel UserData { get; private set; }
    
    private DataHandler _dataHandler;

    private void Awake()
    {
        Instance = this;

        _dataHandler = new DataHandler();
    }

    public void Start()
    {
        // todo make async
        _dataHandler.Initialise();
        GameData = _dataHandler.GetGameData();
        UserData = _dataHandler.GetUserData();
    }
}
