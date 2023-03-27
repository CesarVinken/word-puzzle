using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public static TitleScreenController Instance;

    [SerializeField] private MainView _titleView;
    [SerializeField] private LevelSelectionView _levelSelectionView;

    private ITitleScreenView _currentView;

    private void Awake()
    {
        if (_titleView == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _titleView");
        }
        if (_levelSelectionView == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find _levelSelectionView");
        }

        Instance = this;

        _titleView.Setup();
        _levelSelectionView.Setup();
    }

    private void Start()
    {
        _titleView.Initialise();
        SetCurrentView(_titleView);
    }

    public void ToLevel(LevelDataModel levelData)
    {
        GameManager.Instance.ToLevel(levelData);
    }

    public void ToLevelSelectionView()
    {
        _levelSelectionView.Initialise();
        SetCurrentView(_levelSelectionView);
    }

    private void SetCurrentView(ITitleScreenView newView)
    {
        if (_currentView != null)
        {
            _currentView.Hide();
        }

        _currentView = newView;
        _currentView.Show();
    }
}
