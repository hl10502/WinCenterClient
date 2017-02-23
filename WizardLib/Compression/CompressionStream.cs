using System;
using System.IO;

namespace WizardLib.Compression
{
    /// <summary>
    /// Abstract base class for the compression stream class
    /// </summary>
    public abstract class CompressionStream : Stream
    {
        private Stream storedStream = null;
        protected Stream zipStream 
        { 
            set 
            { 
                disposed = false;
                storedStream = value;
            }

            private get { return storedStream; }
        }

        public virtual void SetBaseStream(Stream baseStream)
        {
            throw new NotImplementedException();
        }

        private bool disposed = true;

        protected CompressionStream()
        {
            zipStream = null;
            disposed = true;
        }

        /// <summary>
        /// Write *to* this stream *from* the source stream in a buffered manner analogous to Write()
        /// </summary>
        /// <param name="sourceStream">Stream get data from</param>
        public void BufferedWrite(Stream sourceStream)
        {
            StreamUtilities.BufferedStreamCopy(sourceStream, this);
        }

        /// <summary>
        /// Read *from* this stream and write to the targetStream in a buffered manner as per the Read()
        /// </summary>
        /// <param name="targetStream">Stream to put data into</param>
        public void BufferedRead(Stream targetStream)
        {
            StreamUtilities.BufferedStreamCopy(this, targetStream);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return zipStream.Read(buffer, offset, count);
        }

        public override long Position
        {
            get { return zipStream.Position; }
            set { zipStream.Position = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if( disposing )
            {
                if (!disposed)
                {
                    if (zipStream != null)
                    {
                        zipStream.Dispose();
                        zipStream = null;
                    }
                    disposed = true;
                }  
            }
            base.Dispose(disposing);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            zipStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return zipStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return zipStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return zipStream.CanWrite; }
        }

        public override void Flush()
        {
            zipStream.Flush();
        }

        public override long Length
        {
            get { return zipStream.Length; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return zipStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            zipStream.SetLength(value);
        }

    }  
}
