using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;

namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that all Stored proedures that have parameters
    /// should inherit from. Contains common stored procedure functionality.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class StoredProcedureBase<TReturn, TParameters>
        : IStoredProcedure<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class
    {
        #region Fields

        /// <summary>
        /// The object that represents the procedure parameters
        /// </summary>
        private TParameters _parameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(TParameters parameters)
        {
            // Validate arguments
            //if (parameters == null) throw new ArgumentNullException("parameters");

            string procedureName = GetType().Name;
            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName, 
                procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string procedureName,
            TParameters parameters)
        {
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            //if (parameters == null) throw new ArgumentNullException("parameters");

            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName, 
                procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters, schema name and procedure name.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string schemaName,
            string procedureName, TParameters parameters)
        {
            // Validate arguments
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            //if (parameters == null) throw new ArgumentNullException("parameters");

            InitializeFromParameters(schemaName, procedureName, parameters);
        }

        /// <summary>
        /// Initializes this instance from paremeters. to be called from constructors
        /// </summary>
        private void InitializeFromParameters(string schemaName,
            string procedureName, TParameters parameters)
        {
            // Validate arguments
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            //if (parameters == null) throw new ArgumentNullException("parameters");

            SetSchemaName(schemaName);
            SetProcedureName(procedureName);
            SetParameters(parameters);

            TryInitializeFromAttributesInternal();
        }

        /// <summary>
        /// Tries to initializes properties from attributes.
        /// </summary>
        private void TryInitializeFromAttributesInternal()
        {
            Type type = GetType();

            TrySetProcedureNameFromNameAttribute(type);
            //TrySetReturnTypeFromAttribute(type);
            TrySetSchemaNameFromSchemaAttribute(type);
        }

        #endregion

        #region IStoredProcedure<TReturn,TParameter> Members

        /// <summary>
        /// The object that represents the procedure parameters
        /// </summary>
        public TParameters Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Ensurefullies the construcuted.
        /// </summary>
        /// <exception cref="System.Exception">
        /// this instance is not fully constrcuted
        /// </exception>
        public void EnsureFullyConstructed()
        {
            if (!IsFullyConstructed())
            {
                string message = ExceptionMessages.StoredProcedureIsNotFullyConstructed;

                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    message);
            }
        }

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

        #endregion

        #region Properties

        public Type ParametersType
        {
            get { return typeof(TParameters); }
        }

        /// <summary>
        /// Gets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public Type ReturnType
        {
            get { return typeof(TReturn); }
        }

        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        public string SchemaName { get; private set; }

        #endregion

        #region Methods : Public


        /// <summary>
        /// Initializes from attributes.
        /// </summary>
        [ObsoleteAttribute("This method is obsolete. The code it ran is now called " +
                           "internally from this class's constructors. Please remove " +
                           "references to this method call as it will be removed in a" +
                           "later release. ", false)]
        public void InitializeFromAttributes()
        {
            //TryInitializeFromAttributesInternal();
        }

        /// <summary>
        /// Determines if the procedure is fully constructed and in a valid 
        /// state which can be called and executed
        /// </summary>
        /// <returns></returns>
        public bool IsFullyConstructed()
        {
            return HasProcedureName() && HasReturnType();
        }

        #endregion

        #region Methods : Private

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
        private bool HasProcedureName()
        {
            return !String.IsNullOrEmpty(ProcedureName);
        }

        /// <summary>
        /// Determines whether this instance has a return type.
        /// </summary>
        /// <returns></returns>
        private bool HasReturnType()
        {
            return (ReturnType != null);
        }


        protected void SetParameters(TParameters parameters)
        {
            _parameters = parameters;
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
