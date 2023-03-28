using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormedWordCharacterTile : MonoBehaviour
{
    public FormedWordCharacter FormedWordCharacter { get; private set; }
 
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _tileSprite;


    public void Setup(FormedWordCharacter formedWordCharacter)
    {
        if(_image == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find image component on {gameObject.name}");
        }
        if (_text == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find text component on {gameObject.name}");
        }

        if (_tileSprite == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _tileSprite on {gameObject.name}");
        }

        FormedWordCharacter = formedWordCharacter;

        SetContent(FormedWordCharacter);
    }

    public void SetContent(FormedWordCharacter formedWordCharacter)
    {
        FormedWordCharacter = formedWordCharacter;

        if (FormedWordCharacter == null)
        {
            SetText("");
            _image.sprite = null;
            _image.color = ColourUtility.GetColour(ColourType.DisabledGray);
        }
        else
        {
            SetText(FormedWordCharacter.Character);
            _image.sprite = _tileSprite;
            _image.color = ColourUtility.GetColour(ColourType.Empty);
        }
    }

    private void SetText(string text)
    {
        _text.text = text;
    }
}
