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

        _foundFormableWord = Check(openTiles);
       
        return _foundFormableWord;
    }

    private bool Check(List<CharacterTile> tiles)
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

            if (GeneratePermutations(subset, 0, subset.Count - 1))
            {
                return true;
            }
        }
        return false;
    }

    private bool GeneratePermutations(List<CharacterTile> tiles, int leftIndex, int rightIndex)
    {
        if (leftIndex == rightIndex)
        {
            string wordAttempt = string.Join("", tiles.Select(c => c.CharacterTileData.Character));
            char firstCharacter = wordAttempt[0];
            List<string> words = GameManager.Instance.WordDictionary[firstCharacter];

            if (words.Contains(wordAttempt))
            {
                ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
                return true;
            }
        }
        else
        {
            for (int i = leftIndex; i <= rightIndex; i++)
            {
                Swap(tiles, leftIndex, i);
                if (GeneratePermutations(tiles, leftIndex + 1, rightIndex)) return true;
                Swap(tiles, leftIndex, i);
            }
        }

        return false;
    }

    private void Swap(List<CharacterTile> tiles, int index1, int index2)
    {
        CharacterTile temp = tiles[index1];
        tiles[index1] = tiles[index2];
        tiles[index2] = temp;
    }
}
