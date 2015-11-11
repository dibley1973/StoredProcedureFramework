using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    /// Defines the direction of data flow for the property/parameter.
    /// </summary>
    public class DirectionAttribute : Attribute
    {
        public ParameterDirection Value { get; set; }

        public DirectionAttribute(ParameterDirection direction)
        {
            Value = direction;
        }
    }
}