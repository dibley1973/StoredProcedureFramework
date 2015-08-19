using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Stream to MemoryStream, Array or String
    /// </summary>
    public class StreamToMemoryAttribute : StreamOutputAttribute
    {
        public String Encoding { get; set; }

        /// <summary>
        /// Create Memory Stream
        /// </summary>
        /// <returns></returns>
        internal Stream CreateStream()
        {
            return new MemoryStream();
        }

        /// <summary>
        /// Resolve Encoding for conversion of MemoryStream to String
        /// </summary>
        /// <returns></returns>
        internal Encoding GetEncoding()
        {
            var method = typeof(Encoding).GetMethod(Encoding);
            return (Encoding)typeof(Encoding).InvokeMember(Encoding,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.IgnoreCase, null, null, null);
        }

        public StreamToMemoryAttribute()
        {
            if (String.IsNullOrEmpty(Encoding))
            {
                Encoding = "Default";
            }
        }
    }
}