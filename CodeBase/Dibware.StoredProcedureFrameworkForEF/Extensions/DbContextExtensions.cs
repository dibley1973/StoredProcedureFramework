using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Generics;
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

        private static string GetDefaultStoredProcedureName(Type contextType, PropertyInfo propertyInfo)
        {
            var propertyInfoName = propertyInfo.Name;
            return contextType.GetPropertyName(propertyInfoName);
        }

        private static Maybe<string> GetOverriddenStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        {
            Maybe<NameAttribute> finderResult =
                new PropertyNameAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureName(Type storedProcedurePropertyType)
        {
            Maybe<NameAttribute> finderResult =
                new TypeNameAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo)
        {
            Maybe<SchemaAttribute> finderResult =
               new PropertySchemaAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureSchemaName(Type storedProcedurePropertyType)
        {
            Maybe<SchemaAttribute> finderResult =
                new TypeSchemaAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static string GetStoredProcedureName(Type contextType, PropertyInfo storedProcedurePropertyInfo)
        {
            string defaultProcedureName = GetDefaultStoredProcedureName(contextType,
                storedProcedurePropertyInfo);

            Maybe<string> overriddenProcedureNameResult =
                GetOverriddenStoredProcedureName(storedProcedurePropertyInfo)
                    .Or(GetOverriddenStoredProcedureName(storedProcedurePropertyInfo.PropertyType));

            string procedureName = overriddenProcedureNameResult.SingleOrDefault(defaultProcedureName);

            return procedureName;
        }

        private static void InitializeStoredProcedureProperty(DbContext context, Type contextType, PropertyInfo storedProcedurePropertyInfo)
        {
            var constructorInfo = storedProcedurePropertyInfo.PropertyType.GetConstructor(new[] { contextType });
            if (constructorInfo == null) return;

            object procedure = constructorInfo.Invoke(new object[] { context });
            SetStoredProcedureName(contextType, storedProcedurePropertyInfo, procedure);
            SetStoredProcedureSchemaName(storedProcedurePropertyInfo, procedure);

            storedProcedurePropertyInfo.SetValue(context, procedure);
        }

        private static void SetStoredProcedureName(Type contextType,
            PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            var name = GetStoredProcedureName(contextType, storedProcedurePropertyInfo);

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
            Maybe<string> overriddenProcedureSchemaResult =
                GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo)
                .Or(GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo.PropertyType));

            if (overriddenProcedureSchemaResult.HasItem)
            {
                ((StoredProcedureBase)procedure).SetSchemaName(overriddenProcedureSchemaResult.Single());
            }
        }

        #endregion
    }
}