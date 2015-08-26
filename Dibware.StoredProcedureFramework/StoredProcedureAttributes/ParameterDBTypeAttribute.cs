using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    /// Define the SqlDbType for the parameter corresponding to this property.
    /// </summary>
    public class ParameterDbTypeAttribute : Attribute
    {
        public SqlDbType Value { get; set; }

        public ParameterDbTypeAttribute(SqlDbType type)
        {
            Value = type;
        }
    }
}