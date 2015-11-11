using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
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
        : StoredProcedureBase, IStoredProcedure<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
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

        #endregion

        #region IStoredProcedure<TReturn,TParameter> Members

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
        /// The object that represents the procedure parameters
        /// </summary>
        public TParameters Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region Properties

        public Type ParametersType
        {
            get { return typeof(TParameters); }
        }

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





        #endregion
    }
}
