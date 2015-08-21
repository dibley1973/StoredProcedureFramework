using System;
using System.IO;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    ///// <summary>
    ///// Stream to File output
    ///// </summary>
    //public class StreamToFileAttribute : StreamOutputAttribute
    //{
    //    public String FileNameField { get; set; }

    //    public String Location { get; set; }

    //    /// <summary>
    //    /// Create the file stream using location attribute data and filename in returned data
    //    /// </summary>
    //    /// <param name="t"></param>
    //    /// <returns></returns>
    //    internal Stream CreateStream(object t)
    //    {
    //        String filename = Location;
    //        var tp = t.GetType();
    //        var p = tp.GetProperty(FileNameField);
    //        if (null != p)
    //        {
    //            var name = p.GetValue(t, null);
    //            if (null != name)
    //                filename = Path.Combine(filename, name.ToString());
    //        }

    //        return new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
    //    }

    //    public StreamToFileAttribute()
    //    {
    //    }
    //}
}
