using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Allows the setting of the user defined table type name for table valued parameters
    /// </summary>
    public class Schema : Attribute
    {
        public String Value { get; set; }

        public Schema(String value)
        {
            Value = value;
        }
    }
}