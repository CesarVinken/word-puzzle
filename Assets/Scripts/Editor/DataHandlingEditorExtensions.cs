using UnityEditor;
using UnityEngine;

public class DataHandlingEditorExtensions : Editor
{
    // give data, and check if there are any ways to form valid words
    [MenuItem("Data/LevelSolveChecker")]
    public static void CheckLevelResolve()
    {
        LevelSolveChecker.Execute();
    }

    // create a user data json file in which all game progress is cleared. Only the first level is unlocked
    [MenuItem("Data/Reset User Data")]
    public static void ResetUserData()
    {
        UserDataWriter.Reset();
    }

    // Take the long words list, take out all words longer than 7 characters and make words data usable
    [MenuItem("Data/Clean Up Dictionary Data")]
    public static void CleanUpDictionaryData()
    {
        DictionaryCleanupHandler.CleanUp();
    }
}
