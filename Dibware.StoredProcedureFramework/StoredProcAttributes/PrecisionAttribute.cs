using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Defines the  the maximum number of digits allowed for numeric data types. 
    /// Should be used on output and return code parameters.
    /// This attribute should not be set for nonnumeric data types.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property)]
    public class PrecisionAttribute : Attribute
    {
        public Int32 Value { get; set; }

        public PrecisionAttribute(Int32 value)
        {
            Value = value;
        }
    }
}