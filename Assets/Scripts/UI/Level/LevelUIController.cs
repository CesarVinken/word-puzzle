using TMPro;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    public static LevelUIController Instance;

    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TemporaryVictoryButton _temporaryVictoryButton;

    public void Awake()
    {
        if(_levelNameText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find level name text on {gameObject.name}");
        }

        Instance = this;

        _temporaryVictoryButton.Setup();
    }

    public void Start()
    {
        _levelNameText.text = GameManager.Instance._currentLevelData.Title;
    }

    public void ToLevelSelection()
    {
        GameManager.Instance.ToLevelSelection();
    }
}
