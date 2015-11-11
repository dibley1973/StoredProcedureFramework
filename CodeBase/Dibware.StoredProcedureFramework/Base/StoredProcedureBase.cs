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
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        public string SchemaName { get; private set; }

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

            ProcedureName = procedureName;
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

            SchemaName = schemaName;
        }

        /// <summary>
        /// Setermines if teh specified type inherits from this object type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool DoesTypeInheritsFromThis(Type type)
        {
            bool result = typeof(StoredProcedureBase).IsAssignableFrom(type);
            return result;
        }

        #endregion

        #region Methods : Private / protected
        /// <summary>
        /// Ensures the procedure has a name.
        /// </summary>
        private void EnsureProcedureHasName()
        {
            if (!HasProcedureName())
            {
                string message = ExceptionMessages.StoredProcedureDoesNotHaveName;
                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    message);
            }
        }

        /// <summary>
        /// Determines if this instance has a procedure name
        /// </summary>
        /// <returns></returns>
        protected bool HasProcedureName()
        {
            return !String.IsNullOrEmpty(ProcedureName);
        }

        ///// <summary>
        ///// Determines whether this instance has a return type.
        ///// </summary>
        ///// <returns></returns>
        //protected abstract bool HasReturnType();

        /// <summary>
        /// Tries to initializes properties from attributes.
        /// </summary>
        protected void TryInitializeFromAttributesInternal()
        {
            Type type = GetType();

            TrySetProcedureNameFromNameAttribute(type);
            //TrySetReturnTypeFromAttribute(type);
            TrySetSchemaNameFromSchemaAttribute(type);
        }

        /// <summary>
        /// Tries the set procedure name from Name attribute.
        /// </summary>
        /// <param name="type">The type.</param>
        private void TrySetProcedureNameFromNameAttribute(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
            if (attribute != null)
            {
                SetProcedureName(attribute.Value);
            }
        }

        //private void TrySetReturnTypeFromAttribute(Type type)
        //{
        //    ReturnTypeAttribute attribute = Attribute.GetCustomAttribute(type, typeof(ReturnTypeAttribute)) as ReturnTypeAttribute;
        //    if (attribute != null)
        //    {
        //        SetReturnType(attribute.Returns);
        //    }
        //}

        /// <summary>
        /// Tries the set schema name from schema attribute.
        /// </summary>
        /// <param name="type">The type.</param>
        private void TrySetSchemaNameFromSchemaAttribute(Type type)
        {
            SchemaAttribute attribute = Attribute.GetCustomAttribute(type, typeof(SchemaAttribute)) as SchemaAttribute;
            if (attribute != null)
            {
                SetSchemaName(attribute.Value);
            }
        }

        #endregion
    }
}