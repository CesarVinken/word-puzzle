using UnityEngine;

public class NotImplementedException : System.Exception
{
    public NotImplementedException(string message)
    {
        Debug.LogError($"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>NOT IMPLEMENTED:</color> {message}");
    }

    public NotImplementedException(string typeString, string foundType)
    {
        Debug.LogError($"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>NOT IMPLEMENTED:</color> Could not find an implementation for the {typeString} <color={ColourUtility.GetHexadecimalColour(ColourType.SelectedBackground)}>'{foundType}'</color>");
    }

    public NotImplementedException(string missingImplementationType, string typeString, string foundType)
    {
        Debug.LogError($"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>NOT IMPLEMENTED:</color> Could not find a {missingImplementationType} for the {typeString} <color={ColourUtility.GetHexadecimalColour(ColourType.SelectedBackground)}>'{foundType}'</color>");
    }
}
