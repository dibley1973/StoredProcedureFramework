using System;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;

namespace Dibware.StoredProcedureFramework.Base
{
    public abstract class SqlFunctionBase<TReturn, TParameters>
        : SqlFunctionBase, ISqlFunction<TReturn, TParameters>
        where TReturn : new()
        where TParameters : class, new()
    {
        #region Fields

        private TParameters _parameters;

        #endregion

        #region Constructors

        //protected SqlFunctionBase()
        //    : this(null)
        //{ }

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

        protected SqlFunctionBase(string sqlFunctionName,
            TParameters parameters)
        {
            if (sqlFunctionName == null) throw new ArgumentNullException("sqlFunctionName");
            if (sqlFunctionName == string.Empty) throw new ArgumentOutOfRangeException("sqlFunctionName");

            InitializeFromParameters(StoredProcedureDefaults.DefaultSchemaName,
                sqlFunctionName, parameters);
        }

        protected SqlFunctionBase(string schemaName,
            string sqlFunctionName,
            TParameters parameters)
        {
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (sqlFunctionName == null) throw new ArgumentNullException("sqlFunctionName");
            if (sqlFunctionName == string.Empty) throw new ArgumentOutOfRangeException("sqlFunctionName");

            InitializeFromParameters(schemaName,
                sqlFunctionName, parameters);
        }

        /// <summary>
        /// Initializes this instance from paremeters. to be called from constructors
        /// </summary>
        private void InitializeFromParameters(string schemaName,
            string functionName, TParameters parameters)
        {
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (functionName == null) throw new ArgumentNullException("functionName");
            if (functionName == string.Empty) throw new ArgumentOutOfRangeException("functionName");

            SetSchemaName(schemaName);
            SetFunctionName(functionName);
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
        /// Gets a value indicating whether this instance has null Sql function parameters.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has null Sql function parameters; otherwise, <c>false</c>.
        /// </value>
        public bool HasNullSqlFunctionParameters
        {
            get { return Parameters is NullSqlFunctionParameters; }
        }

        /// <summary>
        /// The object that represents the Sql function parameters
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
        /// Determines if the function is fully constructed and in a valid 
        /// state which can be called and executed
        /// </summary>
        /// <returns></returns>
        private bool IsFullyConstructed()
        {
            return HasFunctionName() && HasReturnType();
        }

        #endregion

        #region Methods : Private and protected

        private bool HasReturnType()
        {
            return (ReturnType != null);
        }

        private void SetParameters(TParameters parameters)
        {
            _parameters = parameters;
        }

        #endregion

    }
}
