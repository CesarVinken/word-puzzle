using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    public static LevelUIController Instance;

    [SerializeField] private LevelView _levelView;
    [SerializeField] private CelebrationView _celebrationScreen;

    private ILevelScreenView _currentView;

    public void Awake()
    {
        if (_levelView == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _levelView");
        }
        if (_celebrationScreen == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _celebrationScreen");
        }

        Instance = this;
    }

    private void Start()
    {
        if (GameManager.Instance.PreviousScene != SceneType.None)
        {
            Setup();
            Initialise();
        }

        SetCurrentView(_levelView);
    }

    public void Setup()
    {
        _levelView.Setup();
        _celebrationScreen.Setup();
    }

    public void Initialise()
    {
        _levelView.Initialise();
        _celebrationScreen.Initialise();

    }

    private void Unload()
    {
        GameFlowManager.Instance.Unload();

        _levelView.Unload();
    }

    public void ToLevelSelection()
    {
        Unload();
        GameManager.Instance.ToLevelSelection();
    }

    public void ToCelebration()
    {
        SetCurrentView(_celebrationScreen);
    }

    private void SetCurrentView(ILevelScreenView newView)
    {
        if (_currentView != null)
        {
            _currentView.Hide();
        }

        _currentView = newView;
        _currentView.Show();
    }

    public void ShowEndGamePanel()
    {
        _levelView.ShowEndGamePanel();
    }
}
