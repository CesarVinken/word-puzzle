using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTileMono : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _characterText;

    public CharacterTile CharacterTile { get; private set; }

    public void Setup(CharacterTileDataModel characterTileData)
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find button on {gameObject.name}");
        }
        if (_image == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find image on {gameObject.name}");
        }
        if (_characterText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _character on {gameObject.name}");
        }
        CharacterTile = new CharacterTile();
        CharacterTile.Setup(characterTileData, this);

        gameObject.name = $"CharacterTile {CharacterTile.Id} '{CharacterTile.CharacterTileData.Character}'";

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise(List<CharacterTile> tileChildren)
    {
        CharacterTile.Initialise(tileChildren);
        _characterText.text = CharacterTile.CharacterTileData.Character;
    }

    public void Block()
    {
        _image.color = ColourUtility.GetColour(ColourType.DisabledGray);

        gameObject.SetActive(true);
    }

    public void Open()
    {
        _image.color = ColourUtility.GetColour(ColourType.Empty);

        gameObject.SetActive(true);
    }

    public void Use()
    {
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (CharacterTile.State != CharacterTileState.Open) return;

        if (GameFlowManager.Instance.LetterPickActions.Count >= 7) return; // the player cannot do more moves if the number of moves equals the maximum word length

        ConsoleLog.Log(LogCategory.General, $"Add {_characterText.text} to word", LogPriority.Low);
        GameFlowManager.Instance.MoveHandler.UseTile(CharacterTile);
    }
}
