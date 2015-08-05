using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Define the SqlDbType for the parameter corresponding to this property.
    /// </summary>
    public class ParameterTypeAttribute : Attribute
    {
        public SqlDbType Value { get; set; }

        public ParameterTypeAttribute(SqlDbType type)
        {
            Value = type;
        }
    }
}