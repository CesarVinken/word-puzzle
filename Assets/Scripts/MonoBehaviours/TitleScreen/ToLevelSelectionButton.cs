using UnityEngine;
using UnityEngine.UI;

public class ToLevelSelectionButton : MonoBehaviour, ITitleScreenButton
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

    public void OnClick()
    {
        TitleScreenController.Instance.ToLevelSelectionView();
    }
}

