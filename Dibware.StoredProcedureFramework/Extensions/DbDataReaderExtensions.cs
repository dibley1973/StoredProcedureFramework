using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.StoredProcAttributes;

namespace Dibware.StoredProcedureFramework.Extensions
{
    internal static class DbDataReaderExtensions
    {

        /// <summary>
        /// Read streamed data from SQL Server into a file or memory stream. If the target property for the data in object 't' is not
        /// a stream, then copy the data to an array or String.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <param name="p"></param>
        /// <param name="stream"></param>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private static void ReadFromStream(DbDataReader reader, object t, String name, PropertyInfo p, StreamOutputAttribute stream)
        {
            // handle streamed values
            Stream toStream = StreamHelpers.CreateStream(stream, t);
            try
            {
                using (Stream fromStream = reader.GetStream(reader.GetOrdinal(name)))
                {
                    fromStream.CopyTo(toStream);
                }

                // reset our stream position
                toStream.Seek(0, 0);

                // For array output, copy tostream to user's array and close stream since user will never see it
                if (p.PropertyType.Name.Contains("[]") || p.PropertyType.Name.Contains("Array"))
                {
                    Byte[] item = new Byte[toStream.Length];
                    toStream.Read(item, 0, (int)toStream.Length);
                    p.SetValue(t, item, null);
                    toStream.Close();
                }
                else if (p.PropertyType.Name.Contains("String"))
                {
                    StreamReader r = new StreamReader(toStream, ((StreamToMemoryAttribute)stream).GetEncoding());
                    String text = r.ReadToEnd();
                    p.SetValue(t, text, null);
                    r.Close();
                }
                else if (p.PropertyType.Name.Contains("Stream"))
                {
                    // NOTE: User will have to close the stream if they don't tell us to close file streams!
                    if (typeof(StreamToFileAttribute) == stream.GetType() && !((StreamToFileAttribute)stream).LeaveStreamOpen)
                    {
                        toStream.Close();
                    }

                    // pass our created stream back to the user since they asked for a stream output
                    p.SetValue(t, toStream, null);
                }
                else
                {
                    throw new Exception(String.Format("Invalid property type for property {0}. Valid types are Stream, byte or character arrays and String",
                        p.Name));
                }
            }
            catch (Exception)
            {
                // always close the stream on exception
                if (null != toStream)
                    toStream.Close();

                // pass the exception on
                throw;
            }
        }

        /// <summary>
        /// Read data for the current result row from a reader into a destination object, by the name
        /// of the properties on the destination object.
        /// </summary>
        /// <param name="reader">data reader holding return data</param>
        /// <param name="targetObject">object to populate</param>
        /// <param name="props">properties list to copy from result set row 'reader' to object 'targetObject'</param>
        /// <returns></returns>
        public static object ReadRecord(this DbDataReader reader, object targetObject, PropertyInfo[] props)
        {
            string name = "";

            // copy mapped properties
            foreach (PropertyInfo p in props)
            {
                try
                {
                    // default name is property name, override of parameter name by attribute
                    var attr = p.GetAttribute<NameAttribute>();
                    name = (null == attr) ? p.Name : attr.Value;

                    // see if we're being asked to stream this property
                    var stream = p.GetAttribute<StreamOutputAttribute>();
                    if (null != stream)
                    {
                        // if yes, then write to a stream
                        ReadFromStream(reader, targetObject, name, p, stream);
                    }
                    else
                    {
                        // get the requested value from the returned dataset and handle null values
                        var data = reader[name];
                        if (data is DBNull)
                            p.SetValue(targetObject, null, null);
                        else
                            p.SetValue(targetObject, reader[name], null);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(IndexOutOfRangeException))
                    {
                        // if the result set doesn'targetObject have this value, intercept the exception
                        // and set the property value to null / 0
                        p.SetValue(targetObject, null, null);
                    }
                    else
                    {
                        // tell the user *where* we had an exception
                        Exception outer = new Exception(String.Format("Exception processing return column {0} in {1}",
                            name, targetObject.GetType().Name), ex);

                        // something bad happened, pass on the exception
                        throw outer;
                    }
                }
            }

            return targetObject;
        }
    }
}
