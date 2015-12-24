using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    /// Define the SqlDbType for the parameter corresponding to this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbTypeAttribute : Attribute
    {
        public SqlDbType Value { get; private set; }

        public DbTypeAttribute(SqlDbType type)
        {
            Value = type;
        }
    }
}