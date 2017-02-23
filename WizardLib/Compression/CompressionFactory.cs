using System.IO;
using System;

namespace WizardLib.Compression
{
    /// <summary>
    /// A static factory to create an object that will allow the archiving of data
    /// </summary>
    public static class CompressionFactory
    {
        /// <summary>
        /// Type of compressed stream
        /// </summary>
        public enum Type
        {
            Gz,
            Bz2
        }

        /// <summary>
        /// Instantiate a class that can decompress a data stream type
        /// </summary>
        /// <param name="compressionType">Type of compressed stream to read</param>
        /// <param name="compressedDataSource">The contents of compressed data</param>
        /// <exception cref="NotSupportedException">If there is not a compressor for a specified archive type</exception>
        /// <returns>CompressionStream to allow an read as a stream</returns>
        public static CompressionStream Reader(Type compressionType, Stream compressedDataSource)
        {
            if (compressionType == Type.Gz)
                return new DotNetZipGZipInputStream(compressedDataSource);

            if (compressionType == Type.Bz2)
                return new DotNetZipBZip2InputStream(compressedDataSource);

            throw new NotSupportedException(String.Format("Type: {0} is not supported by CompressionStream Reader", compressionType));
        }

        /// <summary>
        /// Instantiate a class that can compress a data stream type
        /// </summary>
        /// <param name="compressionType">Type of compressed stream to write</param>
        /// <param name="compressedDataTarget">The place where the compressed data will be put</param>
        /// <exception cref="NotSupportedException"> if there is not a compressor for a specified archive type</exception>
        /// <returns>CompressionStream to allow an write as a stream</returns>
        public static CompressionStream Writer(Type compressionType, Stream compressedDataTarget)
        {
            if (compressionType == Type.Gz)
                return new DotNetZipGZipOutputStream(compressedDataTarget);

            if (compressionType == Type.Bz2)
                return new DotNetZipBZip2OutputStream(compressedDataTarget);

            throw new NotSupportedException(String.Format("Type: {0} is not supported by CompressionStream Writer", compressionType));
        }
    }
}
