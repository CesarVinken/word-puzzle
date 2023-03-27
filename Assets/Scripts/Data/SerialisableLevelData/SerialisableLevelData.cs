using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class SerialisableLevelData
{
    [JsonProperty("title")]
    public string Title = "";
    [JsonProperty("tiles")]
    public SerialisableCharacterTile[] CharacterTiles = new SerialisableCharacterTile[] { };

    public LevelDataModel Deserialise()
    {
        List<CharacterTileDataModel> characterTileDatas = DeserialiseTiles();

        return new LevelDataModel(Title, characterTileDatas);
    }

    private List<CharacterTileDataModel> DeserialiseTiles()
    {
        List<CharacterTileDataModel> characterTileDatas = new List<CharacterTileDataModel>();

        for (int i = 0; i < CharacterTiles.Length; i++)
        {
            SerialisableCharacterTile serialisableCharacterTile = CharacterTiles[i];
            CharacterTileDataModel characterTileData = serialisableCharacterTile.Deserialise();

            characterTileDatas.Add(characterTileData);
        }

        return characterTileDatas;
    }
}
