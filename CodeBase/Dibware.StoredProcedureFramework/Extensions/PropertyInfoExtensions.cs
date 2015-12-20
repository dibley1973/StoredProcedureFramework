using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Extensions
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Get an attribute for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return (T)attributes;
        }

        /// <summary>
        /// Gets the name of the namefrom attribute or property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string GetNamefromAttributeOrPropertyName(this PropertyInfo instance)
        {
            return new PropertyNameHelper(instance)
                .Build()
                .Name;
        }

        // TODO: Consider moving this into dedicated
        public static SqlDbType GetColumnSqlDbTypefromAttributeOrClr(this PropertyInfo propertyInfo)
        {
            // The default type is the property CLR type, but override if ParameterDbTypeAttribute if available     
            ParameterDbTypeAttribute dbTypeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
            SqlDbType columnType = (dbTypeAttribute != null)
                ? dbTypeAttribute.Value
                : ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(propertyInfo.PropertyType);

            return columnType;
        }
    }
}