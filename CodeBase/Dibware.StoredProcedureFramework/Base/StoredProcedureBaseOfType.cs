using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that all Stored proedures that have parameters
    /// should inherit from. Contains common stored procedure functionality.
    /// </summary>
    /// <typeparam name="TReturn">
    /// The type that will be returned by the stored procedure. For a stored 
    /// procedure which returns a single recordset this will generally be a List 
    /// of a type. for stored procedures which return multiple recordsets this will 
    /// be a class which contains one or more lists of objects.
    /// </typeparam>
    /// <typeparam name="TParameters">
    /// The type that represents the parameters for the stored procedure. This 
    /// can also be set as a <see cref="System.Object"/>.
    /// </typeparam>
    public abstract class StoredProcedureBase<TReturn, TParameters>
        : StoredProcedureBase, IStoredProcedure<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
    {
        #region Fields

        private TParameters _parameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> 
        /// class without parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected StoredProcedureBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> 
        /// class with parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters for the stored procedure or null if no paremeters are needed.
        /// </param>
        protected StoredProcedureBase(TParameters parameters)
        {
            string procedureName = GetType().Name;
            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName,
                procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">
        /// The parameters for the stored procedure or null if no paremeters are needed.
        /// </param>
        protected StoredProcedureBase(string procedureName,
            TParameters parameters)
        {
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName,
                procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters, schema name and procedure name.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">
        /// The parameters for the stored procedure or null if no paremeters are needed.
        /// </param>
        protected StoredProcedureBase(string schemaName,
            string procedureName, TParameters parameters)
        {
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            InitializeFromParameters(schemaName, procedureName, parameters);
        }

        /// <summary>
        /// Initializes this instance from paremeters. to be called from constructors
        /// </summary>
        private void InitializeFromParameters(string schemaName,
            string procedureName, TParameters parameters)
        {
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            SetSchemaName(schemaName);
            SetProcedureName(procedureName);
            SetParameters(parameters);
            TryInitializeFromAttributesInternal();
        }

        #endregion

        #region IStoredProcedure<TReturn,TParameter> Members

        /// <summary>
        /// Ensures this instance is fully construcuted.
        /// </summary>
        /// <exception cref="StoredProcedureConstructionException">
        /// this instance is not fully constrcuted
        /// </exception>
        public void EnsureIsFullyConstructed()
        {
            if (IsFullyConstructed()) return;

            string message = ExceptionMessages.StoredProcedureIsNotFullyConstructed;
            throw ExceptionHelper.CreateStoredProcedureConstructionException(message);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has null stored procedure parameters.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has null stored procedure parameters; otherwise, <c>false</c>.
        /// </value>
        public bool HasNullStoredProcedureParameters
        {
            get { return Parameters is NullStoredProcedureParameters; }
        }

        /// <summary>
        /// The object that represents the procedure parameters
        /// </summary>
        public TParameters Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the parameters.
        /// </summary>
        /// <value>
        /// The type of the parameters.
        /// </value>
        public Type ParametersType
        {
            get { return typeof(TParameters); }
        }

        /// <summary>
        /// Gets the type of object to be returned as the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public Type ReturnType
        {
            get { return typeof(TReturn); }
        }

        #endregion

        #region Methods : Public

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

        #region Methods : Private and protected

        private bool HasReturnType()
        {
            return (ReturnType != null);
        }

        protected void SetParameters(TParameters parameters)
        {
            _parameters = parameters;
        }

        #endregion
    }
}