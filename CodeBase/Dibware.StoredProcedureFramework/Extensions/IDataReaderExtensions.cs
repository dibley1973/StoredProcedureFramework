using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.Extensions
{
    internal static class IDataReaderExtensions
    {
        /// <summary>
        /// Gets the data type of the specified column by name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">fieldName</exception>
        internal static Type GetFieldType(this IDataReader instance,
            string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");

            var ordinal = instance.GetOrdinal(fieldName);
            return instance.GetFieldType(ordinal);
        }
    }
}