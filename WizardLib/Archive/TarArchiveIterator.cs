using System;
using System.IO;
using ICSharpCode.SharpZipLib.Tar;

namespace WizardLib.Archive
{

    public class SharpZipTarArchiveIterator : ArchiveIterator
    {
        private TarInputStream tarStream;
        private TarEntry tarEntry;
        private bool disposed;

        public SharpZipTarArchiveIterator()
        {
            tarStream = null;
            disposed = true;
        }

        public SharpZipTarArchiveIterator(Stream tarFile)
        {
            tarStream = new TarInputStream(tarFile);
            disposed = false;
        }

        public override void SetBaseStream(Stream stream)
        {
            tarStream = new TarInputStream(stream);
            disposed = false;
        }

        ~SharpZipTarArchiveIterator()
        {
            Dispose();
        }

        public override bool HasNext()
        {
            tarEntry = tarStream.GetNextEntry();

            if (tarEntry == null)
                return false;

            return true;
        }

        public override string CurrentFileName()
        {
            if (tarEntry == null)
                return String.Empty;

            return tarEntry.Name;
        }

        public override long CurrentFileSize()
        {
            if (tarEntry == null)
                return 0;

            return tarEntry.Size;
        }

        public override DateTime CurrentFileModificationTime()
        {
            if (tarEntry == null)
                return new DateTime();

            return tarEntry.ModTime;
        }

        public override bool IsDirectory()
        {
            if (tarEntry == null)
                return false;

            return tarEntry.IsDirectory;
        }

        public override void ExtractCurrentFile(Stream extractedFileContents)
        {
            if (IsDirectory())
                return;

            tarStream.CopyEntryContents(extractedFileContents);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(disposing)
            {
                if(!disposed)
                {
                    if (tarStream != null)
                        tarStream.Dispose();
                    disposed = true;
                }
                
            }
        }
    }
}
