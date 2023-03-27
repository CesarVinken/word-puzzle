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
            case ColourType.YellowDark:
                return new Color(241f / 255f, 206f / 255f, 49f / 255f);
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
            case ColourType.Blue:
                return "#0013C1";
            case ColourType.Cyan:
                return "#48D1AC";
            case ColourType.Empty:
                return "#FFFFFF";
            case ColourType.ErrorRed:
                return "#C60000";
            case ColourType.Magenta:
                return "#C02BA6";
            case ColourType.Orange:
                return "#EC762E";
            case ColourType.SelectedBackground:
                return "#69FDFF";
            case ColourType.Yellow:
                return "#FFF26C";
            case ColourType.YellowDark:
                return "#F1CE31";
            default:
                new NotImplementedException("ColourType", colourType.ToString());
                return "#000000";
        }
    }
}
