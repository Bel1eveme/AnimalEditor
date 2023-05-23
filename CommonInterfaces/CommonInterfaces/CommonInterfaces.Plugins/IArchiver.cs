namespace CommonInterfaces.Plugins
{
    public interface IArchiver
    {

        void Zip(MemoryStream decompressedStream, string fileName);

        MemoryStream Unzip(string fileName);

        string GetExtension();
    }
}
