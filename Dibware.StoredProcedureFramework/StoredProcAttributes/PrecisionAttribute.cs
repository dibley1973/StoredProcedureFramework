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
        public Byte Value { get; set; }

        public PrecisionAttribute(Byte value)
        {
            Value = value;
        }
    }
}