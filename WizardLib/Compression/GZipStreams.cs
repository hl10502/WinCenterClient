using Ionic.Zlib;
using System.IO;

namespace WizardLib.Compression
{
    /// <summary>
    /// A class that can compress a bzip2 data stream type
    /// </summary>
    class DotNetZipGZipOutputStream : CompressionStream
    {
        public DotNetZipGZipOutputStream()
        {
            
        }

        public DotNetZipGZipOutputStream(Stream outputStream)
        {
            zipStream = new GZipStream(outputStream, CompressionMode.Compress);
        }

        public override void SetBaseStream(Stream outputStream)
        {
            zipStream = new GZipStream(outputStream, CompressionMode.Compress);
        }
    }

    /// <summary>
    /// A class that can decompress a bzip2 data stream type
    /// </summary>
    class DotNetZipGZipInputStream : CompressionStream
    {
        public DotNetZipGZipInputStream()
        {
            
        }

        public DotNetZipGZipInputStream(Stream inputStream)
        {
            zipStream = new GZipStream(inputStream, CompressionMode.Decompress);
        }

        public override void SetBaseStream(Stream inputStream)
        {
            zipStream = new GZipStream(inputStream, CompressionMode.Decompress);
        }

    }
}
