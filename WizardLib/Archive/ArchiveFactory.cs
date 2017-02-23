using System;
using System.IO;
using WizardLib.Compression;

namespace WizardLib.Archive
{
    /// <summary>
    /// A static factory to create an object that will allow the archiving of data
    /// </summary>
    public static class ArchiveFactory
    {
        /// <summary>
        /// Supported types of archive
        /// </summary>
        public enum Type
        {
            Tar,
            TarGz,
            TarBz2,
            Zip
        }


        /// <summary>
        /// Instantiate a class that can read a archive type
        /// </summary>
        /// <param name="archiveType">Type of archive to read</param>
        /// <param name="packagedData">The contents of packaged data</param>
        /// <exception cref="NotSupportedException">if there is not a iterator for a specified archive type</exception>
        /// <returns>ArchiveIterator to allow an archive to be traversed</returns>
        public static ArchiveIterator Reader(Type archiveType, Stream packagedData)
        {
            if (archiveType == Type.Tar)
                return new SharpZipTarArchiveIterator(packagedData);
            if (archiveType == Type.TarGz)
                return new SharpZipTarArchiveIterator(CompressionFactory.Reader(CompressionFactory.Type.Gz, packagedData));
            if (archiveType == Type.TarBz2)
                return new SharpZipTarArchiveIterator(CompressionFactory.Reader(CompressionFactory.Type.Bz2, packagedData));
            if (archiveType == Type.Zip)
                return new DotNetZipZipIterator(packagedData);

            throw new NotSupportedException(String.Format("Type: {0} is not supported by ArchiveIterator", archiveType));
        }

        /// <summary>
        /// Instantiate a class that can write a archive type
        /// </summary>
        /// <param name="archiveType">Type of archive to write</param>
        /// <param name="targetPackage">The placed where the packaged data will be stored</param>
        /// <exception cref="NotSupportedException">if there is not a writer for a specified archive type</exception>
        /// <returns>ArchiveWriter to allow an archive to be written</returns>
        public static ArchiveWriter Writer(Type archiveType, Stream targetPackage)
        {
            if (archiveType == Type.Tar)
                return new SharpZipTarArchiveWriter(targetPackage);
            if (archiveType == Type.Zip)
                return new DotNetZipZipWriter(targetPackage);

            throw new NotSupportedException( String.Format( "Type: {0} is not supported by ArchiveWriter", archiveType ) );
        }
    }
}
