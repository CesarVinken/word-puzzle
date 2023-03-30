using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class AutoLevelSolver
{
    bool _foundFormableWord = false;
    int _checkedCombinations = 0;

    // Get all "open tiles". These are the possible first letters for the new word.
    // Run through words in the dictionaries for this letters, checking for the possible words, using other open tiles and children
    public bool FormableWordsLeft()
    {
        _foundFormableWord = false;
        _checkedCombinations = 0;

        ConsoleLog.Warning(LogCategory.General, $"[[[[[[[[[[[[ CHECK FORMABLE WORS ]]]]]]]]]]]]");
        List<CharacterTile> notUsedTiles = CharacterTileHandler.Tiles.Where(t => t.State != CharacterTileState.Used).ToList();
        List<CharacterTile> openTiles = notUsedTiles.Where(t => t.State == CharacterTileState.Open).ToList();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        CheckCombinations(openTiles, new List<CharacterTile>());

        stopwatch.Stop();
        ConsoleLog.Log(LogCategory.General, $"Checked {_checkedCombinations} words. Check took {stopwatch.ElapsedMilliseconds} milliseconds");

        return _foundFormableWord;
    }

    private void CheckCombinations(List<CharacterTile> tilesToCheck, List<CharacterTile> currentSubset)
    {
        CheckValidity(currentSubset);

        if (_foundFormableWord) return;
        if (currentSubset.Count >= 7) return;

        for (int i = 0; i < tilesToCheck.Count; i++)
        {
            CharacterTile tile = tilesToCheck[i];

            List<CharacterTile> updatedCurrentSubset = new List<CharacterTile>(currentSubset.Count);
            updatedCurrentSubset.AddRange(currentSubset);
            updatedCurrentSubset.Add(tile);

            string wordAttempt = string.Join("", updatedCurrentSubset.Select(c => c.CharacterTileData.Character)).ToUpper(); // does not need ToUpper in game
        //    ConsoleLog.Log(LogCategory.General, $"we add '{tile.CharacterTileData.Character}' and the full charCombo is now: {wordAttempt}");

            List<CharacterTile> updatedTilesToCheck = new List<CharacterTile>(tilesToCheck.Count);
            updatedTilesToCheck.AddRange(tilesToCheck);
            updatedTilesToCheck.Remove(tile);
            updatedTilesToCheck = AddAvailableChildren(updatedTilesToCheck, tile, updatedCurrentSubset);

            CheckCombinations(updatedTilesToCheck, updatedCurrentSubset);

            if (_foundFormableWord) return;
        }
    }

    private List<CharacterTile> AddAvailableChildren(List<CharacterTile> tilesToCheck, CharacterTile currentTile, List<CharacterTile> currentSubset)
    {
        List<CharacterTile> checkedTiles = new List<CharacterTile>();
        for (int k = 0; k < currentTile.TileChildren.Count; k++)
        {
            bool blockedByParent = false;
            CharacterTile child = GameFlowManager.Instance.TilesById[currentTile.CharacterTileData.TileChildren[k]];

            if (tilesToCheck.Contains(child)) continue;
            if (currentSubset.Contains(child)) continue;
            if (currentTile == child) continue;
            if (child.State == CharacterTileState.Used) continue;

            List<CharacterTile> parents = new List<CharacterTile>();
            foreach (KeyValuePair<int, CharacterTile> item in GameFlowManager.Instance.TilesById)
            {
                CharacterTile possibleParent = item.Value;

                // a child cannot be its own parent
                if (child == possibleParent) continue;

                // check if the child tile is also the child of another tile. That parent tile needs to be part of the subset too.
                if (!possibleParent.TileChildren.Contains(child))
                {
                    continue;
                }
            //    ConsoleLog.Log(LogCategory.General, $"{possibleParent.CharacterTileData.Character} is  a parent of {child.CharacterTileData.Character}");

                if (currentSubset.FirstOrDefault(t => t.Id == possibleParent.Id) == null)
                {
              //      ConsoleLog.Warning(LogCategory.General, $"{possibleParent.CharacterTileData.Character} ({possibleParent.Id}) is the parent of {child.CharacterTileData.Character} ({child.Id}), but the parent is not part of the subset");
                    blockedByParent = true;
                    break;
                }
            }

            if (!blockedByParent)
            {
            //    ConsoleLog.Warning(LogCategory.General, $"add {child.CharacterTileData.Character}");
                tilesToCheck.Add(child);
            }
        }
        return tilesToCheck;
    }

    private void CheckValidity(List<CharacterTile> subset)
    {
        if (subset.Count == 0) return;

        _checkedCombinations++;
        string wordAttempt = string.Join("", subset.Select(c => c.CharacterTileData.Character)).ToUpper(); // does not need ToUpper in game
        char firstCharacter = wordAttempt[0];
        List<string> words = GameManager.Instance.WordDictionary[firstCharacter];

        if (words.Contains(wordAttempt))
        {
            _foundFormableWord = true;
            ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
        }
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




    //private bool GeneratePermutations(List<CharacterTile> subset, int leftIndex, int rightIndex)
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
    //            List<CharacterTile> childTiles = subset[i].TileChildren;

    //            ConsoleLog.Log(LogCategory.General, $"{subset[i].CharacterTileData.Character} has {childTiles.Count} children");
    //            for (int j = 0; j < childTiles.Count; j++)
    //            {
    //                CharacterTile childTile = childTiles[j];

    //                if (subset.Contains(childTile)) continue;

    //                List<CharacterTile> parentTiles = childTile.TileParents;
    //                int safeParentTiles = 0;
    //                for (int k = 0; k < parentTiles.Count; k++)
    //                {
    //                    CharacterTile parentTile = parentTiles[k];
    //                    if(parentTile.State == CharacterTileState.Used)
    //                    {
    //                        ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} was already used");
    //                        safeParentTiles++;
    //                        continue;
    //                    }

    //                    if (subset.Contains(parentTile))
    //                    {
    //                        ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} is part of the subset already");
    //                        safeParentTiles++;
    //                        continue;
    //                    }
    //                    ConsoleLog.Log(LogCategory.General, $"parentTile {parentTile.CharacterTileData.Character} {parentTile.Id} is not safe :(");

    //                }
    //                ConsoleLog.Log(LogCategory.General, $"The child {childTile.CharacterTileData.Character} ({childTile.Id}) has {safeParentTiles} out of {parentTiles.Count} free parent tiles");
    //                // if their parents are safe, then add this child tile
    //                if(safeParentTiles == parentTiles.Count)
    //                {
    //                    subset.Add(childTile);
    //                }
    //            }

    //            Swap(subset, leftIndex, i);
    //            if (GeneratePermutations(subset, leftIndex + 1, rightIndex)) return true;
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
