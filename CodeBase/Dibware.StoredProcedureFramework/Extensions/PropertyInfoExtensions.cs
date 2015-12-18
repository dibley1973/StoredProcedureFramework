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

        // TODO consider moving this into dedicated class
        public static string GetNamefromAttributeOrPropertyName(this PropertyInfo propertyInfo)
        {
            // Get the propery column name to property name mapping. The default 
            // name is property name, override of parameter name by attribute if available
            NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
            var name = (nameAttribute == null)
                ? propertyInfo.Name
                : nameAttribute.Value;
            return name;
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