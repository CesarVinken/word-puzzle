using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerialisableWordDictionaryData
{
    public string[] Words;

    public SerialisableWordDictionaryData(string[] words)
    {
        Words = words;
    }
}
