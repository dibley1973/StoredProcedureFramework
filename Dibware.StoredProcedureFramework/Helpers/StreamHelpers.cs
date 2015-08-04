using System.IO;


namespace Dibware.StoredProcedureFramework.Helpers
{
    internal static class StreamHelpers
    {
        /// <summary>
        /// Create a Stream for saving large object data from the server, use the
        /// stream attribute data
        /// </summary>
        /// <param name="format"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        internal static Stream CreateStream(StoredProcAttributes.StreamOutput format, object t)
        {
            Stream output;

            if (typeof(StoredProcAttributes.StreamToFile) == format.GetType())
            {
                // File stream
                output = ((StoredProcAttributes.StreamToFile)format).CreateStream(t); ;

                // build name from location and name property
            }
            else
            {
                // Memory Stream
                output = ((StoredProcAttributes.StreamToMemory)format).CreateStream();
            }

            // if buffering was requested, overlay bufferedstream on our stream
            if (format.Buffered)
            {
                output = new BufferedStream(output);
            }

            return output;
        }
    }
}
