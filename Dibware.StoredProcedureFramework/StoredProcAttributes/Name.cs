using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Parameter name override. Default value for parameter name is the name of the
    /// property. This overrides that default with a user defined name.
    /// </summary>
    public class Name : Attribute
    {
        public String Value { get; set; }

        public Name(String value)
        {
            Value = value;
        }
    }
}