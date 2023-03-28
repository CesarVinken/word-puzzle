using System.Collections.Generic;
using UnityEngine;

public class FormedWordContainer : MonoBehaviour
{
    private FormedWordHandler _formedWordHandler;
    [SerializeField] List<FormedWordCharacterTile> _formedWordCharacters = new List<FormedWordCharacterTile>();

    public void Setup()
    {
        _formedWordHandler = new FormedWordHandler();

        for (int i = 0; i < _formedWordCharacters.Count; i++)
        {
            _formedWordCharacters[i].Setup(null);
        }
    }

    public void Initialise()
    {
        GameFlowManager.Instance.LetterPickEvent += OnPlayerMoveEvent;
        GameFlowManager.Instance.WordSubmitEvent += OnWordSubmitEvent;
    }

    public void RemoveLetter(FormedWordCharacterTile formedWordCharacterTile)
    {
        formedWordCharacterTile.SetContent(null);
    }

    public void OnPlayerMoveEvent(object sender, LetterPickEvent e)
    {
        CharacterTileDataModel characterTileData = e.LetterPickAction.CharacterTile.CharacterTileData;
        FormedWordCharacter formedWordCharacter = new FormedWordCharacter(characterTileData.Character);

        for (int i = 0; i < _formedWordCharacters.Count; i++)
        {
            FormedWordCharacterTile tile = _formedWordCharacters[i];
            if (tile.FormedWordCharacter == null)
            {
                tile.SetContent(formedWordCharacter);
                break;
            }
        }
    }

    public void OnWordSubmitEvent(object sender, WordSubmitEvent e)
    {
        for (int i = _formedWordCharacters.Count - 1; i >= 0; i--)
        {
            RemoveLetter(_formedWordCharacters[i]);
        }
    }
}
