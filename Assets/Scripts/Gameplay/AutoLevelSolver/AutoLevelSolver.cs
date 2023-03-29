using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoLevelSolver
{
    bool _foundFormableWord = false;

    // Get all "open tiles". These are the possible first letters for the new word.
    // Run through words in the dictionaries for this letters, checking for the possible words, using other open tiles and children
    public bool FormableWordsLeft()
    {
        _foundFormableWord = false;
        ConsoleLog.Warning(LogCategory.General, $"[[[[[[[[[[[[ CHECK FORMABLE WORS ]]]]]]]]]]]]");
        List<CharacterTile> notUsedTiles = CharacterTileHandler.Tiles.Where(t => t.State != CharacterTileState.Used).ToList();
        List<CharacterTile> openTiles = notUsedTiles.Where(t => t.State == CharacterTileState.Open).ToList();

        Check(openTiles);
       
        return _foundFormableWord;
    }

    private void Check(List<CharacterTile> tiles)
    {
        int numberOfSubsets = 1 << tiles.Count;
        for (int i = 0; i < numberOfSubsets; i++)
        {
            List<CharacterTile> subset = new List<CharacterTile>();
            for (int j = 0; j < tiles.Count; j++)
            {
                if ((i & (1 << j)) != 0)
                {
                    subset.Add(tiles[j]);
                }
            }
            GeneratePermutations(subset, 0, subset.Count - 1);
            
            if (_foundFormableWord)
            {
                return;
            }
        }
    }

    private void GeneratePermutations(List<CharacterTile> tiles, int leftIndex, int rightIndex)
    {
        if (leftIndex == rightIndex)
        {
            string wordAttempt = string.Join("", tiles.Select(c => c.CharacterTileData.Character));
            char firstCharacter = wordAttempt[0];
            List<string> words = GameManager.Instance.WordDictionary[firstCharacter];
         //   ConsoleLog.Log(LogCategory.General, $"{wordAttempt}");

            if (words.Contains(wordAttempt))
            {
                _foundFormableWord = true;

                ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
                return;
            }
        }
        else
        {
            for (int i = leftIndex; i <= rightIndex; i++)
            {
                Swap(tiles, leftIndex, i);
                GeneratePermutations(tiles, leftIndex + 1, rightIndex);
                Swap(tiles, leftIndex, i);
            }
        }
    }

    private void Swap(List<CharacterTile> tiles, int index1, int index2)
    {
        CharacterTile temp = tiles[index1];
        tiles[index1] = tiles[index2];
        tiles[index2] = temp;
    }

    //private void Check(List<CharacterTileDataModel> tiles, string wordAttempt, int depth)
    //{
    //    for (int i = 0; i < tiles.Count; i++)
    //    {
    //        List<CharacterTileDataModel> tilesCopy = tiles.Select(p => new CharacterTileDataModel(p.Id, p.TilePosition, p.Character, p.TileChildren)).ToList();
    //        tilesCopy.RemoveAt(0);
    //        string wordAttemptCopy = new string(wordAttempt.ToCharArray());
    //        wordAttemptCopy += tiles[0].Character;
    //        ConsoleLog.Log(LogCategory.General, $"depth {depth}. going over {i}: {tiles[i].Character}. wordAttempt: {wordAttemptCopy}");

    //        if (tilesCopy.Count > 0)
    //        {
    //            Check(tilesCopy, wordAttemptCopy, depth++);
    //        }
    //    }




    //    //for (int i = tiles.Count - 1; i >= 0; i--)
    //    //{
    //    //    string wordAttemptCopy = new string(wordAttempt.ToCharArray());
    //    //    wordAttemptCopy += tiles[tiles.Count - 1].CharacterTileData.Character;
    //    //    //  tiles.Remove(tiles[i]);
    //    //    List<CharacterTile> updatedList = new List<CharacterTile>();
    //    //    for (int j = 0; j < tiles.Count - 1; j++)
    //    //    {
    //    //        updatedList.Add(tiles[j]);
    //    //    }
    //    //    ConsoleLog.Log(LogCategory.General, $"updated tiles length is {updatedList.Count}. formed word is {wordAttempt}");
    //    //    if (updatedList.Count == 0) return;

    //    //    Check(updatedList, wordAttemptCopy);
    //    //}
    //    //for (int i = 0; i < tiles.Count; i++)
    //    //{
    //    //    string wordAttemptCopy = new string(wordAttempt.ToCharArray());
    //    //    wordAttemptCopy += tiles[0].CharacterTileData.Character;
    //    //    //  tiles.Remove(tiles[i]);
    //    //    List<CharacterTile> updatedList = new List<CharacterTile>();
    //    //    for (int j = 1; j < tiles.Count; j++)
    //    //    {
    //    //        updatedList.Add(tiles[j]);
    //    //    }
    //    //    ConsoleLog.Log(LogCategory.General, $"updated tiles length is {updatedList.Count}. formed word is {wordAttempt}");
    //    //    if (updatedList.Count == 0) return;

    //    //    Check(updatedList, wordAttemptCopy);
    //    //}


    //    //for (int i = tiles.Count - 1; i >= 0; i--)
    //    //{
    //    //  //  wordAttempt += tiles[i].CharacterTileData.Character;
    //    //  ////  tiles.Remove(tiles[i]);
    //    //  //  ConsoleLog.Log(LogCategory.General, $"tiles length is {tiles.Count}. formed word is {wordAttempt}");
    //    //  //  List<CharacterTile> updatedList = new List<CharacterTile>();
    //    //  //  for (int i = 0; i < length; i++)
    //    //  //  {

    //    //  //  }
    //    //  //  if (tiles.Count == 0) return;

    //    //  //  Check(tiles, wordAttempt);
    //    //}
    //}
}
