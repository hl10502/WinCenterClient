using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace WizardLib.Archive
{
    public class ExtractProgressChangedEventArgs : EventArgs
    {
        private readonly long bytesIn;
        private readonly long totalBytes;

        public ExtractProgressChangedEventArgs(long bytesTransferred, long totalBytesToTransfer)
        {
            bytesIn = bytesTransferred;
            totalBytes = totalBytesToTransfer;
        }

        public long BytesTransferred
        {
            get { return bytesIn; }
        }

        public long TotalBytesToTransfer
        {
            get { return totalBytes; }
        }
    }

    public class DotNetZipZipIterator : ArchiveIterator
    {
        private ZipFile zipFile = null;
        private IEnumerator<ZipEntry> enumerator = null;
        private ZipEntry zipEntry;
        private bool disposed;

        public event EventHandler<ExtractProgressChangedEventArgs> CurrentFileExtractProgressChanged;
        public event EventHandler<EventArgs> CurrentFileExtractCompleted;

        public DotNetZipZipIterator()
        {
            disposed = false;
        }

        void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            switch (e.EventType)
            {
                case ZipProgressEventType.Extracting_EntryBytesWritten:
                    {
                        EventHandler<ExtractProgressChangedEventArgs> handler = CurrentFileExtractProgressChanged;
                        if (handler != null)
                            handler(this, new ExtractProgressChangedEventArgs(e.BytesTransferred, e.TotalBytesToTransfer));
                    }
                    break;
                case ZipProgressEventType.Extracting_AfterExtractEntry:
                    {
                        EventHandler<EventArgs> handler = CurrentFileExtractCompleted;
                        if (handler != null)
                            handler(this, e);
                    }
                    break;
            }
        }

        public DotNetZipZipIterator(Stream inputStream) : this()
        {
            Initialise(inputStream);
        }

        private void Initialise(Stream zipStream)
        {
            try
            {
                zipFile = ZipFile.Read(zipStream);
            }
            catch (ZipException e)
            {
                throw new ArgumentException("Cannot read input as a ZipFile", "zipStream", e);
            }
            
            enumerator = zipFile.GetEnumerator();
            zipFile.ExtractProgress += zipFile_ExtractProgress;
        }

        public override void SetBaseStream(Stream inputStream)
        {
            Initialise(inputStream);
            disposed = false;
        }

        ~DotNetZipZipIterator()
        {
            Dispose();
        }

        public override bool HasNext()
        {
            if (enumerator != null && enumerator.MoveNext())
            {
                zipEntry = enumerator.Current;
                return true;
            }
            return false;
        }

        public override string CurrentFileName()
        {
            if (zipEntry == null)
                return String.Empty;

            return zipEntry.FileName;
        }

        public override long CurrentFileSize()
        {
            if (zipEntry == null)
                return 0;

            return zipEntry.UncompressedSize;
        }

        public override DateTime CurrentFileModificationTime()
        {
            if (zipEntry == null)
                return new DateTime();

            return zipEntry.LastModified;
        }

        public override bool IsDirectory()
        {
            if (zipEntry == null)
                return false;

            return zipEntry.IsDirectory;
        }

        public override void ExtractCurrentFile(Stream extractedFileContents)
        {
            if (IsDirectory())
                return;

            zipEntry.Extract(extractedFileContents);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if(disposing)
            {
                if(!disposed)
                {
                    if (zipFile != null)
                    {
                        zipFile.ExtractProgress -= zipFile_ExtractProgress;
                        zipFile.Dispose();
                    }

                    disposed = true;
                }               
            }
        }
    }
}
