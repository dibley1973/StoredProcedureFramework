using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Allows the setting of the user defined table type name for table valued parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SchemaAttribute : Attribute
    {
        public String Value { get; set; }

        public SchemaAttribute(String value)
        {
            Value = value;
        }
    }
}