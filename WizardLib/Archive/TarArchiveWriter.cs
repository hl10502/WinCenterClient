using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Tar;

namespace WizardLib.Archive
{

    public class SharpZipTarArchiveWriter : ArchiveWriter
    {
        private TarOutputStream tar = null;
        private const long bufferSize = 32*1024;
        protected bool disposed;

        public SharpZipTarArchiveWriter()
        {
            disposed = false;
        }

        public SharpZipTarArchiveWriter(Stream outputStream) : this()
        {
            tar = new TarOutputStream(outputStream);
        }

        public override void SetBaseStream(Stream outputStream)
        {
            tar = new TarOutputStream(outputStream);
            disposed = false;
        }

        public override void AddDirectory(string directoryName, DateTime modificationTime)
        {
            StringBuilder sb = new StringBuilder(directoryName);
            
            //Need to add a terminal front-slash to add a directory
            if (!directoryName.EndsWith("/"))
                sb.Append("/");
            TarEntry entry = TarEntry.CreateTarEntry(sb.ToString());
            entry.ModTime = modificationTime;

            tar.PutNextEntry(entry);
            tar.CloseEntry();
        }

        public override void Add(Stream filetoAdd, string fileName, DateTime modificationTime)
        {
            TarEntry entry = TarEntry.CreateTarEntry(fileName);
            entry.Size = filetoAdd.Length;
            entry.ModTime = modificationTime;

            tar.PutNextEntry( entry );
            byte[] buffer = new byte[bufferSize];
            int n;

            //You have to do this because if using a memory stream the pointer will be at the end it
            //it's just been read and this will cause nothing to be written out
            filetoAdd.Position = 0;

            while ((n = filetoAdd.Read(buffer, 0, buffer.Length)) > 0)
            {
                tar.Write(buffer, 0, n);
            }
            
            tar.Flush();
            tar.CloseEntry();
        }

        protected override void Dispose(bool disposing)
        {
            
            if( !disposed )
            {
                if( disposing )
                {
                   if (tar != null)
                    {
                        tar.Dispose();
                    }
                    disposed = true;
                }  
            }
            base.Dispose(disposing);   
        }
    }


}
