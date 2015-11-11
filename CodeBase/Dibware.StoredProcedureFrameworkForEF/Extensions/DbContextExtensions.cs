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
            foreach (PropertyInfo propertyInfo in contextType.GetProperties())
            {
                Type propertyType = propertyInfo.PropertyType;
                bool typeInheritsFromStoredProcedureBase = StoredProcedureBase.DoesTypeInheritsFromThis(propertyType);
                if (typeInheritsFromStoredProcedureBase)
                {
                    InitializeStoredProcedureProperty(context, propertyInfo, contextType);
                }
            }
        }

        private static void InitializeStoredProcedureProperty(DbContext context, PropertyInfo propertyInfo, Type contextType)
        {
            var storedProcedurePropertyInfo = propertyInfo;
            var constructorInfo = storedProcedurePropertyInfo.PropertyType.GetConstructor(new[] { contextType });
            if (constructorInfo == null) return;

            object procedure = constructorInfo.Invoke(new object[] { context });
            string name = null;

            // try and get name attribute of property
            var nameAttributes = storedProcedurePropertyInfo.GetCustomAttributes(typeof(NameAttribute));
            var nameAttribute = nameAttributes.FirstOrDefault() as NameAttribute;
            if (nameAttribute != null)
            {
                name = nameAttribute.Value;
                //((StoredProcedureBase)procedure).SetProcedureName(nameAttribute.Value);
            }

            // try get name attribute of object
            var propertyType = storedProcedurePropertyInfo.PropertyType;
            var attributes = propertyType.GetCustomAttributes<NameAttribute>();
            var attribute = attributes.FirstOrDefault();
            if (attribute != null) name = attribute.Value;

            if (name == null)
            {
                // Get property name
                var propertyInfoName = propertyInfo.Name;
                name = contextType.GetPropertName(propertyInfoName);
                //var name = contextType.GetMember(propertyInfoName).First().Name;

                //((StoredProcedureBase)procedure).SetProcedureName(name);
            }

            if (name != null)
            {
                ((StoredProcedureBase)procedure).SetProcedureName(name);
            }

            storedProcedurePropertyInfo.SetValue(context, procedure);
        }
    }
}