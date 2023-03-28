using UnityEngine;

public class ColourUtility
{
    public static Color GetColour(ColourType colourType)
    {
        switch (colourType)
        {
            case ColourType.Empty:
                return Color.white;
            case ColourType.ErrorRed:
                return new Color(200f / 255f, 0, 0);
            case ColourType.SelectedBackground:
                return new Color(231f / 255f, 247f / 255f, 177f / 255f);
            case ColourType.DisabledGray:
                return new Color(145f / 255f, 145f / 255f, 145f / 255f);
            default:
                new NotImplementedException("ColourType", colourType.ToString());
                return Color.black;
        }
    }

    public static string GetHexadecimalColour(ColourType colourType)
    {
        switch (colourType)
        {
            case ColourType.Black:
                return "#000000";
            case ColourType.Empty:
                return "#FFFFFF";
            case ColourType.ErrorRed:
                return "#C60000";
            case ColourType.SelectedBackground:
                return "#69FDFF";
            case ColourType.DisabledGray:
                return "#919191";
            default:
                new NotImplementedException("ColourType", colourType.ToString());
                return "#000000";
        }
    }
}
