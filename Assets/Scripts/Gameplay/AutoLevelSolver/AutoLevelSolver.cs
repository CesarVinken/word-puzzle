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

    private bool GeneratePermutations(List<CharacterTile> subset, int leftIndex, int rightIndex)
    {
        //////////////
        string s = "";
        for (int i = 0; i < subset.Count; i++)
        {
            s += subset[i].CharacterTileData.Character;
        }
        ConsoleLog.Log(LogCategory.General, $"this subset contains {subset.Count} tiles: {s}");
        //////////////

        if (leftIndex == rightIndex)
        {
            string wordAttempt = string.Join("", subset.Select(c => c.CharacterTileData.Character));
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
                List<CharacterTile> childTiles = subset[i].TileChildren;

                ConsoleLog.Log(LogCategory.General, $"{subset[i].CharacterTileData.Character} has {childTiles.Count} children");
                for (int j = 0; j < childTiles.Count; j++)
                {
                    CharacterTile childTile = childTiles[j];

                    if (subset.Contains(childTile)) continue;

                    List<CharacterTile> parentTiles = childTile.TileParents;
                    int safeParentTiles = 0;
                    for (int k = 0; k < parentTiles.Count; k++)
                    {
                        CharacterTile parentTile = parentTiles[k];
                        if(parentTile.State == CharacterTileState.Used)
                        {
                            ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} was already used");
                            safeParentTiles++;
                            continue;
                        }

                        if (subset.Contains(parentTile))
                        {
                            ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} is part of the subset already");
                            safeParentTiles++;
                            continue;
                        }
                        ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} is not safe :(");

                    }
                    ConsoleLog.Log(LogCategory.General, $"The child {childTile.CharacterTileData.Character} ({childTile.Id}) has {safeParentTiles} out of {parentTiles.Count} free parent tiles");
                    // if their parents are safe, then add this child tile
                    if(safeParentTiles == parentTiles.Count)
                    {
                        subset.Add(childTile);
                    }
                }

                Swap(subset, leftIndex, i);
                if (GeneratePermutations(subset, leftIndex + 1, rightIndex)) return true;
                Swap(subset, leftIndex, i);
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

    //private bool Check(List<CharacterTile> tiles)
    //{
    //    int numberOfSubsets = 1 << tiles.Count;
    //    for (int i = 0; i < numberOfSubsets; i++)
    //    {
    //        List<CharacterTile> subset = new List<CharacterTile>();
    //        for (int j = 0; j < tiles.Count; j++)
    //        {
    //            if ((i & (1 << j)) != 0)
    //            {
    //                subset.Add(tiles[j]);
    //            }
    //        }

    //        if (GeneratePermutations(subset, 0, subset.Count - 1))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //private bool GeneratePermutations(List<CharacterTile> subset, int leftIndex, int rightIndex, List<CharacterTile> usedTiles = null)
    //{
    //    //////////////
    //    string s = "";
    //    for (int i = 0; i < subset.Count; i++)
    //    {
    //        s += subset[i].CharacterTileData.Character;
    //    }
    //    ConsoleLog.Log(LogCategory.General, $"this subset contains {subset.Count} tiles: {s}");
    //    //////////////

    //    if (leftIndex == rightIndex)
    //    {
    //        string wordAttempt = string.Join("", subset.Select(c => c.CharacterTileData.Character));
    //        char firstCharacter = wordAttempt[0];
    //        List<string> words = GameManager.Instance.WordDictionary[firstCharacter];

    //        if (words.Contains(wordAttempt))
    //        {
    //            ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
    //            return true;
    //        }
    //    }
    //    else
    //    {
    //        for (int i = leftIndex; i <= rightIndex; i++)
    //        {
    //            if (usedTiles != null && usedTiles.Contains(subset[i]))
    //            {

    //                ConsoleLog.Log(LogCategory.General, $"There are {usedTiles.Count} used tiles in this subset and it already contains tile {subset[i].CharacterTileData.Character} ({subset[i].Id})");
    //                continue; // Tile has already been used in this combination
    //            }

    //            // Check if all parent tiles are present in the combination
    //            //bool allParentsPresent = true;
    //            //foreach (CharacterTile parentTile in subset[i].TileParents)
    //            //{
    //            //    if (!subset.Take(i).Contains(parentTile))
    //            //    {

    //            //        ConsoleLog.Log(LogCategory.General, $"not all parent tiles of {subset[i].CharacterTileData.Character} ({subset[i].Id}) are present");
    //            //        allParentsPresent = false;
    //            //        break;
    //            //    }
    //            //}

    //            //if (!allParentsPresent)
    //            //{
    //            //    continue; // Parent tile not in the combination yet
    //            //}

    //            List<CharacterTile> childTiles = subset[i].TileChildren;

    //            if (childTiles.Count > 0)
    //            {
    //                // Create a new list with the current tile and all its parent tiles
    //                List<CharacterTile> newUsedTiles = new List<CharacterTile>();
    //                newUsedTiles.Add(subset[i]);
    //                //foreach (var parentTile in subset[i].TileParents)
    //                //{
    //                //    newUsedTiles.Add(parentTile);
    //                //}

    //                // Recursively generate permutations with the child tiles included
    //                if (GeneratePermutations(subset, leftIndex + 1, rightIndex, newUsedTiles))
    //                {
    //                    return true;
    //                }
    //            }

    //            Swap(subset, leftIndex, i);
    //            if (GeneratePermutations(subset, leftIndex + 1, rightIndex, usedTiles))
    //            {
    //                return true;
    //            }
    //            Swap(subset, leftIndex, i);
    //        }
    //    }

    //    return false;
    //}

    //private void Swap(List<CharacterTile> tiles, int index1, int index2)
    //{
    //    CharacterTile temp = tiles[index1];
    //    tiles[index1] = tiles[index2];
    //    tiles[index2] = temp;
    //}
}
