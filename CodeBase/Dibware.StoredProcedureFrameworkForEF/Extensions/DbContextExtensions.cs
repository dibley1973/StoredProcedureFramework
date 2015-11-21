using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkForEF.Extensions
{
    /// <summary>
    /// Extension methods for the DbContext object
    /// </summary>
    public static class DbContextExtensions
    {
        public static TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
               this DbContext context,
               IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
               int? commandTimeout = null,
               CommandBehavior commandBehavior = CommandBehavior.Default,
               SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            // Ensure the procedure is fully constructed before going any further
            storedProcedure.EnsureFullyConstructed();

            // Get the context database connection...
            DbConnection connection = context.Database.Connection;

            // ... then return results from DbConnection's extention method.
            return connection.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }

        /// <summary>
        /// Initialize all the stored procedure properties in this DbContext. 
        /// This should be called in the DbContext constructor.
        /// </summary>
        /// <param name="context"></param>
        public static void InitializeStoredProcedureProperties(this DbContext context)
        {
            Type contextType = context.GetType();
            foreach (PropertyInfo propertyInfo in contextType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Type propertyType = propertyInfo.PropertyType;
                bool typeInheritsFromStoredProcedureBase = StoredProcedureBase.DoesTypeInheritsFromThis(propertyType);
                if (typeInheritsFromStoredProcedureBase)
                {
                    InitializeStoredProcedureProperty(context, contextType, propertyInfo);
                }
            }
        }

        private static string GetStoredProcedureNameFromStoredProcedurePropertyName(Type contextType, PropertyInfo propertyInfo)
        {
            var propertyInfoName = propertyInfo.Name;
            var name = contextType.GetPropertyName(propertyInfoName);
            return name;
        }

        private static string GetStoredProcedureNameFromAttributeOnStoredProcedureProperty(PropertyInfo storedProcedurePropertyInfo)
        {
            string name = null;
            var nameAttributes = storedProcedurePropertyInfo.GetCustomAttributes(typeof(NameAttribute));
            var nameAttribute = nameAttributes.FirstOrDefault() as NameAttribute;
            if (nameAttribute != null) name = nameAttribute.Value;
            return name;
        }

        private static string GetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(PropertyInfo storedProcedurePropertyInfo)
        {
            string name = null;
            var propertyType = storedProcedurePropertyInfo.PropertyType;
            var attributes = propertyType.GetCustomAttributes<NameAttribute>();
            var attribute = attributes.FirstOrDefault();
            if (attribute != null) name = attribute.Value;
            return name;
        }

        private static string GetStoredProcedureSchemaNameFromAttributeOnStoredProcedureProperty(PropertyInfo storedProcedurePropertyInfo)
        {
            string schemaName = null;
            var schemaNameAttributes = storedProcedurePropertyInfo.GetCustomAttributes(typeof(SchemaAttribute));
            var schemaNameAttribute = schemaNameAttributes.FirstOrDefault() as SchemaAttribute;
            if (schemaNameAttribute != null) schemaName = schemaNameAttribute.Value;
            return schemaName;
        }

        private static string GetStoredProcedureSchemaNameFromAttributeOnStoredProcedurePropertyType(PropertyInfo storedProcedurePropertyInfo)
        {
            string schemaName = null;
            var propertyType = storedProcedurePropertyInfo.PropertyType;
            var attributes = propertyType.GetCustomAttributes<SchemaAttribute>();
            var attribute = attributes.FirstOrDefault();
            if (attribute != null) schemaName = attribute.Value;
            return schemaName;
        }

        private static void InitializeStoredProcedureProperty(DbContext context, Type contextType, PropertyInfo propertyInfo)
        {
            var storedProcedurePropertyInfo = propertyInfo;
            var constructorInfo = storedProcedurePropertyInfo.PropertyType.GetConstructor(new[] { contextType });
            if (constructorInfo == null) return;

            object procedure = constructorInfo.Invoke(new object[] { context });
            SetStoredProcedureName(contextType, propertyInfo, storedProcedurePropertyInfo, procedure);
            SetStoredProcedureSchemaName(storedProcedurePropertyInfo, procedure);

            storedProcedurePropertyInfo.SetValue(context, procedure);
        }

        private static void SetStoredProcedureName(Type contextType, PropertyInfo propertyInfo,
            PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            var name = GetStoredProcedureNameFromAttributeOnStoredProcedureProperty(storedProcedurePropertyInfo);

            if (name == null)
            {
                name = GetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(storedProcedurePropertyInfo);
            }

            if (name == null)
            {
                name = GetStoredProcedureNameFromStoredProcedurePropertyName(contextType, propertyInfo);
            }

            if (name != null)
            {
                ((StoredProcedureBase) procedure).SetProcedureName(name);
            }
        }

        private static void SetStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            string schemaName = GetStoredProcedureSchemaNameFromAttributeOnStoredProcedureProperty(storedProcedurePropertyInfo) ??
                                GetStoredProcedureSchemaNameFromAttributeOnStoredProcedurePropertyType(storedProcedurePropertyInfo);

            if (schemaName != null)
            {
                ((StoredProcedureBase)procedure).SetSchemaName(schemaName);
            }
        }
    }
}