using System;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    /// Defines the  the maximum number of digits allowed for numeric data types. 
    /// Should be used on output and return code parameters.
    /// This attribute should not be set for nonnumeric data types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrecisionAttribute : Attribute
    {
        public Byte Value { get; private set; }

        public PrecisionAttribute(Byte value)
        {
            Value = value;
        }
    }
}