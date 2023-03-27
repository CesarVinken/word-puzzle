using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;

public class DictionaryCleanupHandler
{
    public static void CleanUp()
    {
        string[] rawWords = GetWordsData();
        string[] filteredWords = FilterWords(rawWords);
        WriteFilteredWordsData(filteredWords);
    }

    private static string[] GetWordsData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Words", "RawWords.txt");

        if (!File.Exists(filePath))
        {
            ConsoleLog.Warning(LogCategory.General, $"Could not finding the RawWords file. Returning");
            return new string[] { };
        }

        string jsonContent = File.ReadAllText(filePath);

        string[] words = jsonContent.Split("\n");

        return words;
    }

    private static string[] FilterWords(string[] unfilteredWords)
    {
        int maximumCharacters = 7;
        string[] filteredWords = unfilteredWords.Where(w => w.Length <= maximumCharacters).ToArray();

        for (int i = 0; i < filteredWords.Length; i++)
        {
            filteredWords[i] = filteredWords[i].Replace("\r", "");
        }

        ConsoleLog.Log(LogCategory.General, $"Added {filteredWords.Length} words to the filtered word list.");

        return filteredWords;
    }

    private static void WriteFilteredWordsData(string[] words)
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets", "Words"));

        string filePath = Path.Combine(Application.streamingAssetsPath, "Words", "Words");
        string jsonString = JsonConvert.SerializeObject(words);

        File.WriteAllText(filePath, jsonString);
    }
}
