using Dibware.StoredProcedureFramework.Helpers;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;

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
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return (T)attribute;
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

        /// <summary>
        /// SQL database type from attribute or CLR type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static SqlDbType GetColumnSqlDbTypefromAttributeOrClr(this PropertyInfo instance)
        {
            return new ColumnSqlDbTypeHelper(instance)
                .Build()
                .SqlDbType;
        }
    }
}