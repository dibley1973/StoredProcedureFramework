using System;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    /// <summary>
    ///     Denotes that a property or class should be excluded from database mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class)]
    public class NotMappedAttribute : Attribute
    {
    }
}