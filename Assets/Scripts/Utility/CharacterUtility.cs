public class CharacterUtility
{
    public static int GetCharacterValue(string character)
    {
        switch (character.ToLower())
        {
            case "a":
            case "e":
            case "i":
            case "o":
            case "n":
            case "r":
            case "t":
            case "l":
            case "s":
            case "u":
                return 1;
            case "d":
            case "g":
                return 2;
            case "b":
            case "c":
            case "m":
            case "p":
                return 3;
            case "f":
            case "h":
            case "v":
            case "w":
            case "y":
                return 4;
            case "k":
                return 5;
            case "j":
            case "x":
                return 8;
            case "q":
            case "z":
                return 10;
            default:
                throw new NotImplementedException("character", character);
        }
    }
}
