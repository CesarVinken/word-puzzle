public interface IJsonFileWriter
{
    void WriteData<T>(T data, string fileName);
}