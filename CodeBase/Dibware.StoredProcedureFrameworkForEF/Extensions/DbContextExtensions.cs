using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
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
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <typeparam name="TResultSetType">The type of the result set type.</typeparam>
        /// <typeparam name="TParameterType">The type of the parameter type.</typeparam>
        /// <param name="context">The DbContext to execute the stored procedure against.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="commandTimeoutOverride">The command timeout override.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        public static TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
               this DbContext context,
               IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
               int? commandTimeoutOverride = null,
               CommandBehavior commandBehavior = CommandBehavior.Default,
               SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            storedProcedure.EnsureIsFullyConstructed();

            DbConnection connection = context.Database.Connection;

            return connection.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeoutOverride,
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
            var contextType = context.GetType();
            var propertyInfos = contextType.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                bool propertyTypeIsStoredProcedureType = StoredProcedureBase.DoesTypeInheritsFromThis(propertyInfo.PropertyType);
                if (propertyTypeIsStoredProcedureType)
                {
                    InitializeStoredProcedureProperty(context, contextType, propertyInfo);
                }
            }
        }

        #region Methods : Private or protected

        private static string GetStoredProcedureNameFromStoredProcedurePropertyName(Type contextType, PropertyInfo propertyInfo)
        {
            var propertyInfoName = propertyInfo.Name;
            return contextType.GetPropertyName(propertyInfoName);
        }
        private static string GetStoredProcedureNameFromAttributeOnStoredProcedureProperty(PropertyInfo storedProcedurePropertyInfo)
        {
            string name = null; // TODO: Clean Code would advise we dont return a null, but instead throw an exception

            var propertyNameAttributeFinder = new PropertyNameAttributeFinder(storedProcedurePropertyInfo);
            propertyNameAttributeFinder.CheckForAttribute();
            if (propertyNameAttributeFinder.HasFoundAttribute)
            {
                name = propertyNameAttributeFinder.AttributeFound.Value;
            }
            return name;
        }

        // TODO: Consider using a class to get name attribute value
        private static string GetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(PropertyInfo storedProcedurePropertyInfo)
        {
            string name = null; // TODO: Clean Code would advise we dont return a null, but instead throw an exception
            
            var typeNameAttributeFinder = new TypeNameAttributeFinder(storedProcedurePropertyInfo.PropertyType);
            typeNameAttributeFinder.CheckForAttribute();
            if (typeNameAttributeFinder.HasFoundAttribute)
            {
                name = typeNameAttributeFinder.AttributeFound.Value;
            }
            //IEnumerable<NameAttribute> attributes = propertyType.GetCustomAttributes<NameAttribute>();
            //var attribute = attributes.FirstOrDefault();
            //if (attribute != null) name = attribute.Value;
           
            return name;
        }





        private static bool TryGetStoredProcedureNameFromAttributeOnStoredProcedureProperty(
            PropertyInfo storedProcedurePropertyInfo,
            ref string name)
        {
            var propertyNameAttributeFinder = new PropertyNameAttributeFinder(storedProcedurePropertyInfo);
            propertyNameAttributeFinder.CheckForAttribute();
            if (propertyNameAttributeFinder.HasFoundAttribute)
            {
                name = propertyNameAttributeFinder.AttributeFound.Value;
                return true;
            }

            return false;
        }

        private static bool TryGetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(
            PropertyInfo storedProcedurePropertyInfo,
            ref string name)
        {
            var typeNameAttributeFinder = new TypeNameAttributeFinder(storedProcedurePropertyInfo.PropertyType);
            typeNameAttributeFinder.CheckForAttribute();
            if (typeNameAttributeFinder.HasFoundAttribute)
            {
                name = typeNameAttributeFinder.AttributeFound.Value;
                return true;
            }

            return false;
        }


        private static string GetStoredProcedureSchemaNameFromAttributeOnStoredProcedureProperty(PropertyInfo storedProcedurePropertyInfo)
        {
            string schema = null; // TODO: Clean Code would advise we dont return a null, but instead throw an exception

            var propertySchemaAttributeFinder = new PropertySchemaAttributeFinder(storedProcedurePropertyInfo);
            propertySchemaAttributeFinder.CheckForAttribute();
            if (propertySchemaAttributeFinder.HasFoundAttribute)
            {
                schema = propertySchemaAttributeFinder.AttributeFound.Value;
            }

            return schema;
        }

        // TODO: Consider using a class to get schema attribute value
        private static string GetStoredProcedureSchemaNameFromAttributeOnStoredProcedurePropertyType(PropertyInfo storedProcedurePropertyInfo)
        {
            string schema = null; // TODO: Clean Code would advise we dont return a null, but instead throw an exception
            var propertyType = storedProcedurePropertyInfo.PropertyType;
            var attributes = propertyType.GetCustomAttributes<SchemaAttribute>();
            var attribute = attributes.FirstOrDefault();
            if (attribute != null) schema = attribute.Value;
            return schema;
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
            var name = (GetStoredProcedureNameFromAttributeOnStoredProcedureProperty(storedProcedurePropertyInfo)
                ?? GetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(storedProcedurePropertyInfo))
                ?? GetStoredProcedureNameFromStoredProcedurePropertyName(contextType, propertyInfo);

            // TODO: Maybe consider TryGet... here as it may be easier to read. Below is still untidy but could be neatened!
            //string name = null;
            //if (TryGetStoredProcedureNameFromAttributeOnStoredProcedureProperty(storedProcedurePropertyInfo, ref name))
            //{
            //    // do nothing name has been got
            //}
            //else if (TryGetStoredProcedureNameFromAttributeOnStoredProcedurePropertyType(storedProcedurePropertyInfo,
            //    ref name))
            //{
            //    // do nothing name has been got
            //}
            //else
            //{
            //    GetStoredProcedureNameFromStoredProcedurePropertyName(contextType, propertyInfo);
            //}

            if (name != null)
            {
                ((StoredProcedureBase)procedure).SetProcedureName(name);
            }
            else
            {
                throw new NullReferenceException("procedure name was not set");
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

        #endregion
    }
}