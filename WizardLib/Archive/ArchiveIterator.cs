using System;
using System.IO;

namespace WizardLib.Archive
{
    /// <summary>
    /// A base abstract class to iterate over an archived file type
    /// </summary>
    public abstract class ArchiveIterator : IDisposable
    {
        /// <summary>
        /// Helper function to extract all contents of this iterating class to a path
        /// </summary>
        /// <param name="pathToExtractTo">The path to extract the archive to</param>
        /// <exception cref="ArgumentNullException">If null path is passed in</exception>
        /// <exception cref="NullReferenceException">If while combining path and current file name a null arises</exception>
        public void ExtractAllContents( string pathToExtractTo )
        {
            if( String.IsNullOrEmpty(pathToExtractTo) )
                throw new ArgumentNullException();

            while( HasNext() )
            {
                //Make the file path from the details in the archive making the path windows friendly
                string conflatedPath = Path.Combine(pathToExtractTo, CurrentFileName()).Replace('/', Path.DirectorySeparatorChar);
                
                //Create directories - empty ones will be made too
                Directory.CreateDirectory( Path.GetDirectoryName(conflatedPath) );

                //If we have a file extract the contents
                if( !IsDirectory() )
                {
                    using (FileStream fs = File.Create(conflatedPath))
                    {
                       ExtractCurrentFile(fs); 
                    }
                }
            }
        }

        /// <summary>
        /// Hook to allow the base stream to be wrapped by this classes archive mechanism
        /// </summary>
        /// <param name="stream">base stream</param>
        public virtual void SetBaseStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public abstract bool HasNext();
        public abstract void ExtractCurrentFile(Stream extractedFileContents);
        public abstract string CurrentFileName();
        public abstract long CurrentFileSize();
        public abstract DateTime CurrentFileModificationTime();
        public abstract bool IsDirectory();

        /// <summary>
        /// Dispose hook - overload and clean up IO
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing){}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }
    }
}
