using UnityEditor;
using UnityEngine;

public class DataHandlingEditorExtensions : Editor
{
    // create a user data json file in which all game progress is cleared. Only the first level is unlocked
    [MenuItem("Extensions/Reset User Data")]
    public static void ResetUserData()
    {
        UserDataWriter.Reset();
    }

    // Take the long words list, take out all words longer than 7 characters and make words data usable
    [MenuItem("Extensions/Clean Up Dictionary Data")]
    public static void CleanUpDictionaryData()
    {
        DictionaryCleanupHandler.CleanUp();
    }

    // takes given data and looks if a valid word can be formed
    [MenuItem("Extensions/Form Word Checker")]
    public static void CheckForFormableWord()
    {
        WordFormChecker.Execute();
    }

    // find the highest possible score and the moves required to get there
    [MenuItem("Extensions/Auto Level Solver")]
    public static void CheckLevelResolve()
    {
        AutoLevelSolver.Execute();
    }
}
