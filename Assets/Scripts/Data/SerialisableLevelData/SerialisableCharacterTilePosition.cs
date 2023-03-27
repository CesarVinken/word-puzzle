using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class SerialisableCharacterTilePosition
{
    [JsonProperty("x")]
    public float X;
    [JsonProperty("y")]
    public float Y;
    [JsonProperty("z")]
    public float Z;

    public Vector3 Deserialise()
    {
        return new Vector3(X, Y, Z);
    }
}