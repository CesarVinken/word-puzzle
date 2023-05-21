using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour, IUIComponent
{
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
    }

    private void Start()
    {
        UIComponentLocator.Instance.Register<LevelUIController>(this);

        Setup();
        Initialise();

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

    public void Unload()
    {
        _levelView.Unload();
        UIComponentLocator.Instance.Deregister<LevelUIController>();
    }

    public void ToLevelSelection()
    {
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
