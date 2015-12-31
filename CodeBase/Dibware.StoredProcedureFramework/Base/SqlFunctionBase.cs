using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;

namespace Dibware.StoredProcedureFramework.Base
{
    public abstract class SqlFunctionBase
    {
        #region Properties

        /// <summary>
        /// Gets the name of the sql function.
        /// </summary>
        /// <value>
        /// The name of the sql function.
        /// </value>
        public string FunctionName
        {
            get { return _state.FunctionName; }
        }

        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        public string SchemaName
        {
            get { return _state.SchemaName; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the name of the two part.
        /// </summary>
        /// <returns></returns>
        public string GetTwoPartName()
        {
            EnsureFunctionHasName();

            return String.Format(
                FormatStrings.TwoPartNameformat, SchemaName, FunctionName);
        }

        /// <summary>
        /// Sets the sql function name.
        /// </summary>
        /// <param name="sqlFunctionName">Name of the SQL function.</param>
        /// <exception cref="System.ArgumentNullException">sqlFunctionName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">sqlFunctionName</exception>
        public void SetFunctionName(string sqlFunctionName)
        {
            // Validate argument
            if (sqlFunctionName == null) throw new ArgumentNullException("sqlFunctionName");
            if (sqlFunctionName == string.Empty) throw new ArgumentOutOfRangeException("sqlFunctionName");

            _state.FunctionName = sqlFunctionName;
        }

        /// <summary>
        /// Sets the schema name.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">schemaName</exception>
        public void SetSchemaName(string schemaName)
        {
            // Validate argument
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");

            _state.SchemaName = schemaName;
        }

        /// <summary>
        /// Determines if the specified type inherits from this object type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool DoesTypeInheritsFromThis(Type type)
        {
            return typeof(SqlFunctionBase).IsAssignableFrom(type);
        }

        #endregion

        #region Methods : Private / protected

        private void EnsureFunctionHasName()
        {
            if (HasFunctionName()) return;

            string message = ExceptionMessages.SqlFunctionDoesNotHaveName;
            throw ExceptionHelper.CreateSqlFunctionConstructionException(message);
        }

        protected bool HasFunctionName()
        {
            return !String.IsNullOrEmpty(FunctionName);
        }

        protected void TryInitializeFromAttributesInternal()
        {
            Type type = GetType();

            TrySetFunctionNameFromNameAttribute(type);
            TrySetSchemaNameFromSchemaAttribute(type);
        }

        private void TrySetFunctionNameFromNameAttribute(Type type)
        {
            NameAttribute attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
            if (attribute != null)
            {
                SetFunctionName(attribute.Value);
            }
        }

        private void TrySetSchemaNameFromSchemaAttribute(Type type)
        {
            SchemaAttribute attribute = Attribute.GetCustomAttribute(type, typeof(SchemaAttribute)) as SchemaAttribute;
            if (attribute != null)
            {
                SetSchemaName(attribute.Value);
            }
        }

        #endregion

        #region State Structure

        protected struct SqlFunctionBaseState
        {
            public string FunctionName;
            public string SchemaName;
        }

        #endregion

        #region Fields

        private SqlFunctionBaseState _state;

        #endregion
    }
}