using Ionic.BZip2;

using CommonInterfaces.Plugins;

namespace Archivers
{
    public class BZip2Archiver : IArchiver
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
            var compressor = new BZip2OutputStream(compressedStream);
            StreamWrite(decompressedStream, compressor);

            compressor.Close();
            compressedStream.Close();
            
        }

        public MemoryStream Unzip(string fileName)
        {
            var decompressedStream = new MemoryStream();
            var compressedStream = File.OpenRead(fileName);

            var decompressor = new BZip2InputStream(compressedStream);
            StreamWrite(decompressor, decompressedStream);

            compressedStream.Close();
            decompressor.Close();

            decompressedStream.Position = 0;
            return decompressedStream;
        }

        public string GetExtension()
        {
            return ".bzip2";
        }
    }
}
