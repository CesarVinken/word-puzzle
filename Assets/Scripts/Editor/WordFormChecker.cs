using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class WordFormChecker
{
    public Dictionary<char, List<string>> WordDictionary { get; private set; }
    public Dictionary<int, CharacterTile> TilesById { get; private set; } = new Dictionary<int, CharacterTile>();
    private DataHandler _dataHandler;
    private bool _foundValidWord = false;

    //public List<CharacterTile> AllTileData = new List<CharacterTile>()
    //{
    //    new CharacterTileDataModel(1, new Vector3(), "a", new List<int>() {  }),
    //    new CharacterTileDataModel(2, new Vector3(), "b", new List<int>() { 4, 1 }),
    //    new CharacterTileDataModel(3, new Vector3(), "c", new List<int>() { 1 }),
    //    new CharacterTileDataModel(4, new Vector3(), "e", new List<int>() {  })
    //};

    public List<CharacterTile> AllTileData = new List<CharacterTile>();

    public static void Execute()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        WordFormChecker wordFormChecker = new WordFormChecker();
        wordFormChecker.Initialise();
        List<CharacterTile> startingList = wordFormChecker.GetStartingList();
        wordFormChecker.CheckCombinations(startingList, new List<CharacterTile>());
        stopwatch.Stop();
        ConsoleLog.Log(LogCategory.General, $"Check took {stopwatch.ElapsedMilliseconds} milliseconds");
    }


    public void Initialise()
    {
        //AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(1, new Vector3(), "a", new List<int>() { })));
        //AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(2, new Vector3(), "b", new List<int>() { 4, 1})));
        //AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(3, new Vector3(), "c", new List<int>() { 1 })));
        //AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(4, new Vector3(), "e", new List<int>() { })));

        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(17, new Vector3(), "c", new List<int>() { 2, 3, 9 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(9, new Vector3(), "b", new List<int>() { 6, 2 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(3, new Vector3(), "a", new List<int>() { 12, 2 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(2, new Vector3(), "i", new List<int>() { })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(12, new Vector3(), "c", new List<int>() { })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(6, new Vector3(), "h", new List<int>() { })));
        
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(18, new Vector3(), "b", new List<int>() { 1, 8, 11 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(8, new Vector3(), "e", new List<int>() { 10, 1 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(11, new Vector3(), "t", new List<int>() { 0, 1 })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(1, new Vector3(), "a", new List<int>() { })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(10, new Vector3(), "d", new List<int>() { })));
        AllTileData.Add(new CharacterTile().WithCharacterTileData(new CharacterTileDataModel(0, new Vector3(), "t", new List<int>() { })));
 

        for (int i = 0; i < AllTileData.Count; i++)
        {
            TilesById.Add(AllTileData[i].Id, AllTileData[i]);
        }

        _dataHandler = new DataHandler();
        WordDictionary = _dataHandler.GetDictionaryData();

        AddParents();

    }

    private void AddParents()
    {
        for (int i = 0; i < AllTileData.Count; i++)
        {
            CharacterTile parentTile = AllTileData[i];
            for (int j = 0; j < parentTile.TileChildren.Count; j++)
            {
                int childId = parentTile.TileChildren[j].Id;
                CharacterTile childTile = TilesById[childId];
                childTile.AddParentTile(parentTile);
            }
        }
    }

    private List<CharacterTile> GetStartingList()
    {
        List<CharacterTile> startingList = new List<CharacterTile>();
        for (int i = 0; i < AllTileData.Count; i++)
        {
            CharacterTile tile = AllTileData[i];

            if (tile.TileParents.Count == 0)
            {
                startingList.Add(tile);
            }
        }

        return startingList;
    }

    private void CheckCombinations(List<CharacterTile> tilesToCheck, List<CharacterTile> currentSubset)
    {
        CheckValidity(currentSubset);

        if (_foundValidWord) return;
        if (currentSubset.Count >= 7) return;

        for (int i = 0; i < tilesToCheck.Count; i++)
        {
            CharacterTile tile = tilesToCheck[i];

            List<CharacterTile> updatedCurrentSubset = new List<CharacterTile>(currentSubset.Count);
            updatedCurrentSubset.AddRange(currentSubset);
            updatedCurrentSubset.Add(tile);

            string wordAttempt = string.Join("", updatedCurrentSubset.Select(c => c.CharacterTileData.Character)).ToUpper(); // does not need ToUpper in game
                                                                                                           //   ConsoleLog.Log(LogCategory.General, $"we add '{tile.Character}' and the full charCombo is now: {wordAttempt}");

            List<CharacterTile> updatedTilesToCheck = new List<CharacterTile>(tilesToCheck.Count);
            updatedTilesToCheck.AddRange(tilesToCheck);
            updatedTilesToCheck.Remove(tile);
            updatedTilesToCheck = AddAvailableChildren(updatedTilesToCheck, tile, updatedCurrentSubset);

            CheckCombinations(updatedTilesToCheck, updatedCurrentSubset);

            if (_foundValidWord) return;
        }
    }

    private List<CharacterTile> AddAvailableChildren(List<CharacterTile> tilesToCheck, CharacterTile currentTile, List<CharacterTile> currentSubset)
    {
        for (int k = 0; k < currentTile.TileChildren.Count; k++)
        {
            bool blockedByParent = false;
            CharacterTile child = TilesById[currentTile.TileChildren[k].Id];
            List<CharacterTile> parents = child.TileParents;

            for (int l = 0; l < parents.Count; l++)
            {
                CharacterTile parent = parents[l];
                if (child == parent) continue;
                if (parent.State == CharacterTileState.Used) continue;

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
                //    ConsoleLog.Warning(LogCategory.General, $"add {child.Character}");
                tilesToCheck.Add(child);
            }
        }
        return tilesToCheck;
    }

    private void CheckValidity(List<CharacterTile> subset)
    {
        if (subset.Count == 0) return;

        string wordAttempt = string.Join("", subset.Select(c => c.CharacterTileData.Character)).ToUpper(); // does not need ToUpper in game
        char firstCharacter = wordAttempt[0];
        List<string> words = WordDictionary[firstCharacter];

        if (words.Contains(wordAttempt))
        {
            _foundValidWord = true;
            ConsoleLog.Warning(LogCategory.General, $"WE FOUND THE FORMABLE WORD {wordAttempt}");
        }
    }
}
