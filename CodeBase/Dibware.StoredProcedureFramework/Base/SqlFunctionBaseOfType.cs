using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Base
{
    public abstract class SqlFunctionBase<TReturn, TParameters>
        : SqlFunctionBase, ISqlFunction<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
    {
        #region Fields

        private TParameters _parameters;

        #endregion

        #region Constructors

        public SqlFunctionBase()
            : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlFunctionBase{TReturn, TParameters}"/> 
        /// class with parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters for the sql funcion or null if no paremeters are needed.
        /// </param>
        protected SqlFunctionBase(TParameters parameters)
        {
            string funtionName = GetType().Name;
            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName,
                funtionName, parameters);
        }

        public SqlFunctionBase(string sqlFunctionName,
            TParameters parameters)
        {
            if (sqlFunctionName == null) throw new ArgumentNullException("sqlFunctionName");
            if (sqlFunctionName == string.Empty) throw new ArgumentOutOfRangeException("sqlFunctionName");

            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName,
                sqlFunctionName, parameters);
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
            SetFunctionName(procedureName);
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
        public bool HasNullSqlFunctionParameters
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
            return HasFunctionName() && HasReturnType();
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
