using System;
using System.IO;

namespace WizardLib
{
    public class StreamUtilities
    {
        /// <summary>
        /// Perform a copy of the contents of one stream class to another in a buffered fashion
        /// 
        /// Buffer size is a hard-coded 2Mb
        /// </summary>
        /// <param name="inputData">Source data</param>
        /// <param name="outputData">Target stream</param>
        public static void BufferedStreamCopy(Stream inputData, Stream outputData)
        {
            if( inputData == null)
                throw new ArgumentNullException("inputData", "BufferedStreamCopy argument cannot be null");

            if (outputData == null)
                throw new ArgumentNullException("outputData", "BufferedStreamCopy argument cannot be null");

            const long bufferSize = 2*1024*1024;

            byte[] buffer = new byte[bufferSize];
            int n;
            while ((n = inputData.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputData.Write(buffer, 0, n);
            }

            outputData.Flush();
        }
    }
}
