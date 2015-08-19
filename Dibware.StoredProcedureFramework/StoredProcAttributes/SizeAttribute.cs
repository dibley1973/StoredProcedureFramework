using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Defines the size in bytes of returned data. 
    /// Should be used on output and return code parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SizeAttribute : Attribute
    {
        public Int32 Value { get; set; }

        public SizeAttribute(Int32 value)
        {
            Value = value;
        }
    }
}