using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class LevelSolveChecker
{
    public Dictionary<char, List<string>> WordDictionary { get; private set; }
    private DataHandler _dataHandler;

    public static void Execute()
    {

        LevelSolveChecker levelSolveChecker = new LevelSolveChecker();
        levelSolveChecker.Check();
    }

    private void Check()
    {
        List<CharacterTileDataModel> tileData = new List<CharacterTileDataModel>()
        {
            new CharacterTileDataModel(1, new Vector3(), "a", new List<int>()),
            new CharacterTileDataModel(2, new Vector3(), "b", new List<int>()),
            new CharacterTileDataModel(3, new Vector3(), "c", new List<int>(4))
            new CharacterTileDataModel(4, new Vector3(), "e", new List<int>())
        };

        _dataHandler = new DataHandler();
        WordDictionary = _dataHandler.GetDictionaryData();

        ConsoleLog.Log(LogCategory.General, $"Loaded dictionary");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        CheckCombinations(tileData, "");

        stopwatch.Stop();
        ConsoleLog.Log(LogCategory.General, $"Check took {stopwatch.ElapsedMilliseconds} milliseconds");
    }


    private void CheckCombinations(List<CharacterTileDataModel> tileData, string wordAttempt) 
    {
        TryAllPermutationsOfSubset(tileData);
        for (int i = 0; i < tileData.Count; i++)
        {
            CharacterTileDataModel tile = tileData[i];

         //   ConsoleLog.Log(LogCategory.General, $"check {tile.Character} ({tile.Id}). Word attempt {wordAttempt}");

            List<CharacterTileDataModel> updatedTiles = new List<CharacterTileDataModel>(tileData.Count);
            updatedTiles.AddRange(tileData);
            updatedTiles.Remove(tile);
            CheckCombinations(updatedTiles, wordAttempt + tile.Character);
        }   
    }

    private void TryAllPermutationsOfSubset(List<CharacterTileDataModel> subset)
    {
        foreach (List<CharacterTileDataModel> permutation in Permutations(subset))
        {
            CheckValidity(permutation);
        }

    }

    private List<List<T>> Permutations<T>(List<T> list)
    {
        if (list.Count == 1)
            return new List<List<T>> { list };

        List<List<T>> permutations = new List<List<T>>();
        foreach (T item in list)
        {
            List<T> remainingList = list.Where(i => !i.Equals(item)).ToList();
            foreach (List<T> permutation in Permutations(remainingList))
            {
                permutations.Add(new List<T> { item }.Concat(permutation).ToList());
            }
        }

        return permutations;
    }


    private void CheckValidity(List<CharacterTileDataModel> subset)
    {
        if (subset.Count == 0) return;

        string wordAttempt = string.Join("", subset.Select(c => c.Character)).ToUpper(); // does not need ToUpper in game
        char firstCharacter = wordAttempt[0];
        List<string> words = WordDictionary[firstCharacter];

        if (words.Contains(wordAttempt))
        {
            ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
        }
    }
}
