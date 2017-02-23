using System;
using System.IO;

namespace WizardLib.Archive
{
    public abstract class ArchiveWriter : IDisposable
    {
        public abstract void Add(Stream filetoAdd, string fileName, DateTime modificationTime);

        public virtual void SetBaseStream(Stream outputStream)
        {
            throw new NotImplementedException();
        }

        public abstract void AddDirectory(string directoryName, DateTime modificationTime);

        /// <summary>
        /// Disposal hook
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing){ }

        public void CreateArchive( string pathToArchive )
        {
            if( !Directory.Exists(pathToArchive) )
                throw new FileNotFoundException( "The path " + pathToArchive + " does not exist" );

            foreach (string filePath in Directory.GetFiles(pathToArchive, "*.*", SearchOption.AllDirectories))
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    Add(fs, CleanRelativePathName(pathToArchive, filePath), File.GetCreationTime(filePath));  
                }
            }

            foreach (string dirPath in Directory.GetDirectories(pathToArchive, "*.*", SearchOption.AllDirectories))
            {
                AddDirectory(CleanRelativePathName(pathToArchive, dirPath), Directory.GetCreationTime(dirPath));
            }
        }

        public void Add(Stream filetoAdd, string fileName)
        {
            Add( filetoAdd, fileName, DateTime.Now );
        }

        public void AddDirectory(string directoryName)
        {
            AddDirectory(directoryName, DateTime.Now);
        }
           
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }

        private string CleanRelativePathName(string rootPath, string pathName)
        {
            return pathName.Replace(rootPath, "").Replace('\\', '/').TrimStart('/');
        }

    }
}
