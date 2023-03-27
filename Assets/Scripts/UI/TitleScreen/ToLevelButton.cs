using UnityEngine;
using UnityEngine.UI;

public class ToLevelButton : MonoBehaviour, ITitleScreenButton
{
    [SerializeField] private Button _button;

    private LevelSelectionTile _levelSelectionTile;

    public void Setup(LevelSelectionTile levelSelectionTile)
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _levelSelectionTile = levelSelectionTile;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        TitleScreenController.Instance.ToLevel(_levelSelectionTile.LevelData);
    }
}
