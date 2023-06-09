using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class DictionaryCleanupHandler
{
    public static async void CleanUp()
    {
        string rawWordsData = await GetWordsData();
        string[] filteredWords = FilterWords(rawWordsData);
        WriteFilteredWordsData(filteredWords);
    }

    private static async Task<string> GetWordsData()
    {
        using var client = new HttpClient();

        try
        {
            var url = "https://raw.githubusercontent.com/lorenbrichter/Words/master/Words/en.txt";
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); // throws an exception if the response status code is an error

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new System.Exception($"Failed to download the content: {ex.Message}");
        }
    }

    private static string[] FilterWords(string rawWordsData)
    {
        int maximumCharacters = 7;

        string[] unfilteredWords = rawWordsData.Split("\n");
        string[] filteredWords = unfilteredWords.Where(w => (w.Length <= maximumCharacters && !string.IsNullOrEmpty(w))).ToArray();

        for (int i = 0; i < filteredWords.Length; i++)
        {
            filteredWords[i] = filteredWords[i].Replace("\r", "");
            filteredWords[i] = filteredWords[i].ToUpper();
        }

        ConsoleLog.Log(LogCategory.General, $"Added {filteredWords.Length} words to the filtered word list.");

        return filteredWords;
    }

    private static void WriteFilteredWordsData(string[] words)
    {
        SerialisableWordDictionaryData serialisableWordDictionaryData = new SerialisableWordDictionaryData(words);

        Directory.CreateDirectory(Path.Combine(Application.dataPath, "StreamingAssets", "words"));

        string filePath = Path.Combine(Application.streamingAssetsPath, "words", "words.json");
        string jsonString = JsonConvert.SerializeObject(serialisableWordDictionaryData);

        File.WriteAllText(filePath, jsonString);
    }
}
