public interface IJsonFileWriter
{
    void SerialiseData<T>(T data, string fileName);
}