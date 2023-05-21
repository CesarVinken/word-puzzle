using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuButton : MonoBehaviour, ILevelUIButton
{
    [SerializeField] private Button _button;

    public void Setup()
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Could not find button on {gameObject.name}");
        }

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {

    }

    public void OnClick()
    {
        LevelUIController levelUIController = UIComponentLocator.Instance.Get<LevelUIController>();
        levelUIController.ToLevelSelection();
    }
}
