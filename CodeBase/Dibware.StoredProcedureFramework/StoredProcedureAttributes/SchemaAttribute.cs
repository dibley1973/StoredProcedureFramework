using System;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    /// Allows the setting of the user defined table type name for table valued parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | 
        AttributeTargets.Struct | 
        AttributeTargets.Property)]
    public class SchemaAttribute : Attribute
    {
        public String Value { get; private set; }

        public SchemaAttribute(String value)
        {
            Value = value;
        }
    }
}