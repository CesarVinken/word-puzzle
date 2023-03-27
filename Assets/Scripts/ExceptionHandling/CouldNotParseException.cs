using UnityEngine;

public class CouldNotParseException : System.Exception
{
    public CouldNotParseException(string message)
    {
        ConsoleLog.Error(LogCategory.General, $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>PARSE EXCEPTION:</color> {message}");
    }
}
