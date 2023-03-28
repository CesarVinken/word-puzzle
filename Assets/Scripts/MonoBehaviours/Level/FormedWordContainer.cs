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
        GameFlowManager.Instance.PlayerMoveEvent += OnPlayerMoveEvent;
    }

    public void RemoveLetter()
    {
        ConsoleLog.Log(LogCategory.General, $"TODO");
    }

    public void OnPlayerMoveEvent(object sender, PlayerMoveEvent e)
    {
        CharacterTileDataModel characterTileData = e.PlayerMove.CharacterTile.CharacterTileData;
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
}
