using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;

namespace Dibware.StoredProcedureFramework.Base
{
    public abstract class StoredProcedureBase : IStoredProcedure
    {
        #region Properties

        /// <summary>
        /// Gets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public string ProcedureName
        {
            get { return _state.ProcedureName; }
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
            EnsureProcedureHasName();

            return String.Format(
                FormatStrings.TwoPartNameformat, SchemaName, ProcedureName);
        }

        /// <summary>
        /// Sets the procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">procedureName</exception>
        public void SetProcedureName(string procedureName)
        {
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            _state.ProcedureName = procedureName;
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
            return typeof(StoredProcedureBase).IsAssignableFrom(type);
        }

        #endregion

        #region Methods : Private / protected

        private void EnsureProcedureHasName()
        {
            if (HasProcedureName()) return;

            string message = ExceptionMessages.StoredProcedureDoesNotHaveName;
            throw ExceptionHelper.CreateStoredProcedureConstructionException(message);
        }

        protected bool HasProcedureName()
        {
            return !String.IsNullOrEmpty(ProcedureName);
        }

        protected void TryInitializeFromAttributesInternal()
        {
            Type type = GetType();

            TrySetProcedureNameFromNameAttribute(type);
            TrySetSchemaNameFromSchemaAttribute(type);
        }

        private void TrySetProcedureNameFromNameAttribute(Type type)
        {
            NameAttribute attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
            if (attribute != null)
            {
                SetProcedureName(attribute.Value);
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

        protected struct StoredProcedureBaseState
        {
            public string ProcedureName;
            public string SchemaName;
        }

        #endregion

        #region Fields

        private StoredProcedureBaseState _state;

        #endregion
    }
}