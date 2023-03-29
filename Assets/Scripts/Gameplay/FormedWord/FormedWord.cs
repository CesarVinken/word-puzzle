public class FormedWord
{
    public string Word { get; private set; }
    public int Value { get; private set; }

    public FormedWord(string word, int value)
    {
        Word = word;
        Value = value;
    }
}