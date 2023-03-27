using UnityEditor;
using UnityEngine;

public class DataHandlingEditorExtensions : Editor
{
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
