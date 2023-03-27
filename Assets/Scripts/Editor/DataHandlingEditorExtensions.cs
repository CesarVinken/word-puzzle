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
}