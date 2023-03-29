using System.Collections.Generic;
using UnityEngine;

public class FormedWordHandler
{
    private List<FormedWordCharacterTile> _formedWordCharacters = new List<FormedWordCharacterTile>();

    public FormedWordHandler(List<FormedWordCharacterTile> formedWordCharacters)
    {
        _formedWordCharacters = formedWordCharacters;
    }

    public void FillNextSlot(CharacterTile pickedCharacterTile)
    {
        ConsoleLog.Warning(LogCategory.General, $"FillNextSlot");
        CharacterTileDataModel characterTileData = pickedCharacterTile.CharacterTileData;
        FormedWordCharacter formedWordCharacter = new FormedWordCharacter(characterTileData.Character);

        for (int i = 0; i < _formedWordCharacters.Count; i++)
        {
            FormedWordCharacterTile tile = _formedWordCharacters[i];
            if (tile.FormedWordCharacter == null)
            {
                ConsoleLog.Warning(LogCategory.General, $"SetContent");
                tile.SetContent(formedWordCharacter);
                break;
            }
        }
    }

    public void RemoveFormedWord()
    {
        for (int i = _formedWordCharacters.Count - 1; i >= 0; i--)
        {
            RemoveLetter(_formedWordCharacters[i]);
        }
    }

    public void RemoveLastLetter()
    {
        for (int i = _formedWordCharacters.Count - 1; i >= 0; i--)
        {
            if (_formedWordCharacters[i].FormedWordCharacter != null)
            {
                RemoveLetter(_formedWordCharacters[i]);
                break;
            }
        }
    }

    private void RemoveLetter(FormedWordCharacterTile formedWordCharacterTile)
    {
        formedWordCharacterTile.SetContent(null);
    }
}
