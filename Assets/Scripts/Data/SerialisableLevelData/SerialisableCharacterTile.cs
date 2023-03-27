using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerialisableCharacterTile
{
    [JsonProperty("id")]
    public int Id;
    [JsonProperty("position")]
    public SerialisableCharacterTilePosition TilePosition;
    [JsonProperty("character")]
    public string Character;
    [JsonProperty("children")]
    public int[] TileChildren;

    public CharacterTileDataModel Deserialise()
    {
        Vector3 tilePosition = TilePosition.Deserialise();
        List<int> tileChildren = TileChildren.ToList();

        CharacterTileDataModel tileData = new CharacterTileDataModel(Id, tilePosition, Character, tileChildren);

        return tileData;
    }
}