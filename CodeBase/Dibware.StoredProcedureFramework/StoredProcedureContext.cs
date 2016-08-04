using Dibware.Helpers.Validation;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Generics;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework
{
    /// <summary>
    /// An inheritable custom context 
    /// </summary>
    public class StoredProcedureContext : IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        #region Construction and Destruction

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFramework.StoredProcedureContext" />
        /// instance using the existing connection to connect to a database.
        /// The connection is not owned by this context and hence  will not be 
        /// disposed when this context is disposed.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        public StoredProcedureContext(SqlConnection existingConnection)
        {
            Guard.ArgumentIsNotNull(existingConnection, "existingConnection");

            _sqlConnection = existingConnection;
            ContextOwnsConnection = false;

            InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFramework.StoredProcedureContext" />
        /// instance using the existing connection to connect to a database.
        /// The connection will not be disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public StoredProcedureContext(SqlConnection existingConnection, bool contextOwnsConnection)
        {
            Guard.ArgumentIsNotNull(existingConnection, "existingConnection");

            _sqlConnection = existingConnection;
            ContextOwnsConnection = contextOwnsConnection;

            InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFramework.StoredProcedureContext" /> 
        /// instance using the given string as the name or connection string for 
        /// the database to which a connection will be made.  
        /// </summary>
        /// <param name="nameOrConnectionString"> Either the database name or a connection string. </param>
        public StoredProcedureContext(string nameOrConnectionString)
        {
            Guard.ArgumentIsNotNullOrEmpty(nameOrConnectionString, "nameOrConnectionString");

            _sqlConnection = new SqlConnection(nameOrConnectionString);

            ContextOwnsConnection = true;
            InitializeStoredProcedureProperties();
        }



        /// <summary>
        /// Finalizes an instance of the <see cref="StoredProcedureSqlConnection"/> class.
        /// </summary>
        ~StoredProcedureContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (IsDisposing) return;

            if (disposing)
            {
                IsDisposing = true;

                if (ContextOwnsConnection)
                {
                    // free other managed objects that implement
                    // IDisposable only
                    if (_sqlConnection != null) _sqlConnection.Dispose();
                }
            }

            // release any unmanaged objects
            // set the object references to null

            IsDisposed = true;
            IsDisposing = false;
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Properties

        public SqlConnection Connection
        {
            get { return _sqlConnection; }
        }

        /// <summary>
        /// Indicates whether this context owns the connection.
        /// </summary>
        /// <value>
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </value>
        private bool ContextOwnsConnection { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        private bool IsDisposed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is disposing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposing; otherwise, <c>false</c>.
        /// </value>
        private bool IsDisposing { get; set; }

        #endregion

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <typeparam name="TResultSetType">The type of the result set type.</typeparam>
        /// <typeparam name="TParameterType">The type of the parameter type.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="commandTimeoutOverride">The command timeout override.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        public TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
               IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
               int? commandTimeoutOverride = null,
               CommandBehavior commandBehavior = CommandBehavior.Default,
               SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            storedProcedure.EnsureIsFullyConstructed();

            return _sqlConnection.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeoutOverride,
                commandBehavior,
                transaction);
        }

        private Maybe<string> GetOverriddenStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        {
            Maybe<NameAttribute> finderResult =
                new PropertyNameAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private Maybe<string> GetOverriddenStoredProcedureName(Type storedProcedurePropertyType)
        {
            Maybe<NameAttribute> finderResult =
                new TypeNameAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private Maybe<string> GetOverriddenStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo)
        {
            Maybe<SchemaAttribute> finderResult =
               new PropertySchemaAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private Maybe<string> GetOverriddenStoredProcedureSchemaName(Type storedProcedurePropertyType)
        {
            Maybe<SchemaAttribute> finderResult =
                new TypeSchemaAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private string GetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        {
            Maybe<string> overriddenProcedureNameResult =
                GetOverriddenStoredProcedureName(storedProcedurePropertyInfo)
                    .Or(GetOverriddenStoredProcedureName(storedProcedurePropertyInfo.PropertyType));

            string defaultProcedureName = storedProcedurePropertyInfo.Name;
            string procedureName = overriddenProcedureNameResult.SingleOrDefault(defaultProcedureName);

            return procedureName;
        }

        /// <summary>
        /// Initialize all the stored procedure properties in this DbContext. 
        /// This should be called in the DbContext constructor.
        /// </summary>
        public void InitializeStoredProcedureProperties()
        {
            var contextType = GetType();
            var propertyInfos = contextType.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                bool propertyTypeIsStoredProcedureType = StoredProcedureBase.DoesTypeInheritsFromThis(propertyInfo.PropertyType);
                if (propertyTypeIsStoredProcedureType)
                {
                    InitializeStoredProcedureProperty(contextType, propertyInfo);
                }
            }
        }

        private void InitializeStoredProcedureProperty(Type contextType, PropertyInfo storedProcedurePropertyInfo)
        {
            var constructorInfo = storedProcedurePropertyInfo.PropertyType.GetConstructor(new[] { contextType });
            if (constructorInfo == null) return;

            object procedure = constructorInfo.Invoke(new object[] { this });
            SetStoredProcedureName(storedProcedurePropertyInfo, procedure);
            SetStoredProcedureSchemaName(storedProcedurePropertyInfo, procedure);

            storedProcedurePropertyInfo.SetValue(this, procedure, null);
        }

        private void SetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            var name = GetStoredProcedureName(storedProcedurePropertyInfo);

            if (name != null)
            {
                ((StoredProcedureBase)procedure).SetProcedureName(name);
            }
            else
            {
                throw new NullReferenceException("procedure name was not set");
            }
        }

        private void SetStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            Maybe<string> overriddenProcedureSchemaResult =
                GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo)
                .Or(GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo.PropertyType));

            if (overriddenProcedureSchemaResult.HasItem)
            {
                ((StoredProcedureBase)procedure).SetSchemaName(overriddenProcedureSchemaResult.Single());
            }
        }
    }
}
