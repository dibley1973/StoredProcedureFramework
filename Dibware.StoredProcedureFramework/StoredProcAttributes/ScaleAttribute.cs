using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Defines the maximum number of digits that can appear to 
    /// the right of the decimal point for numeric data types. 
    /// This value must be less than or equal to the precision.
    /// Can be used on output and return code parameters.
    /// This attribute should not be set for nonnumeric data types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ScaleAttribute : Attribute
    {
        public Byte Value { get; set; }

        public ScaleAttribute(Byte value)
        {
            Value = value;
        }
    }
}