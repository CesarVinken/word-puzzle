using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class FormableWordChecker
{
    bool _foundFormableWord = false;
    int _checkedCombinations = 0;

    // Get all "open tiles". These are the possible first letters for the new word.
    // Run through words in the dictionaries for this letters, checking for the possible words, using other open tiles and children
    public bool FindFormableWord()
    {
        _foundFormableWord = false;
        _checkedCombinations = 0;

        List<CharacterTile> notUsedTiles = CharacterTileHandler.Tiles.Where(t => t.CharacterTileData.State != CharacterTileState.Used).ToList();
        List<CharacterTile> openTiles = notUsedTiles.Where(t => t.CharacterTileData.State == CharacterTileState.Open).ToList();

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
            if (child.CharacterTileData.State == CharacterTileState.Used) continue; // normally not possible, but 

            List<CharacterTile> parents = child.TileParents;

            for (int i = 0; i < parents.Count; i++)
            {
                CharacterTile parent = parents[i];

                // a child cannot be its own parent
                if (child == parent) continue;

                if (parent.CharacterTileData.State == CharacterTileState.Used) continue;

                // the child can only be added if the parent already appears in the subset
                if (currentSubset.FirstOrDefault(t => t.Id == parent.Id) == null)
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
        string wordAttempt = string.Join("", subset.Select(c => c.CharacterTileData.Character));
        char firstCharacter = wordAttempt[0];
        List<string> words = GameManager.Instance.WordDictionary[firstCharacter];

        if (words.Contains(wordAttempt))
        {
            _foundFormableWord = true;
            ConsoleLog.Warning(LogCategory.General, $"we found the formable word '{wordAttempt}'");
        }
    }
}
