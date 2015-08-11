using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Parameter name override. Default value for parameter name is the name of the
    /// property. This overrides that default with a user defined name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | 
        AttributeTargets.Struct | AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        public String Value { get; set; }

        public NameAttribute(String value)
        {
            Value = value;
        }
    }
}