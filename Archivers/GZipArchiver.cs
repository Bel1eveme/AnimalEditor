using Ionic.Zlib;

using CommonInterfaces.Plugins;

namespace Archivers
{
    public class GZipArchiver : IArchiver
    {
        private void StreamWrite(Stream source, Stream destination)
        {
            var buffer = new byte[2048];
            int n;
            while ((n = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, n);
        }

        public void Zip(MemoryStream decompressedStream, string fileName)
        {
            var compressedStream = File.Create(fileName);
            var compressor = new GZipStream(compressedStream, CompressionMode.Compress);
            StreamWrite(decompressedStream, compressor);

            compressor.Close();
            compressedStream.Close();
        }

        public MemoryStream Unzip(string fileName)
        {
            var decompressedStream = new MemoryStream();
            var compressedStream = File.OpenRead(fileName);

            var decompressor = new GZipStream(compressedStream, CompressionMode.Decompress);
            StreamWrite(decompressor, decompressedStream);

            compressedStream.Close();
            decompressor.Close();

            decompressedStream.Position = 0;
            return decompressedStream;
        }

        public string GetExtension()
        {
            return ".gzip";
        }
    }
}