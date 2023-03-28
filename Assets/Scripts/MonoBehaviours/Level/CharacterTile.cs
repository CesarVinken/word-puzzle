using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTile : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _characterText;

    private CharacterTileDataModel _characterTileData;

    public void Setup(CharacterTileDataModel characterTileData)
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find button on {gameObject.name}");
        }
        if (_characterText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _character on {gameObject.name}");
        }

        _characterTileData = characterTileData;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise()
    {
        _characterText.text = _characterTileData.Character;
    }

    public void OnClick()
    {

        ConsoleLog.Log(LogCategory.General, $"Add {_characterText.text} to word");
    }
}
