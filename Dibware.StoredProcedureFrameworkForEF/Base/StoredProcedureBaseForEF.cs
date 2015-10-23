using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    /// <summary>
    /// Expands upon functionality of the <see cref="Dibware.StoredProcedureFramework.Base.StoredProcedureBase{TReturn, TParameters}"/> 
    /// class by adding support for execution from DbContext by providing
    /// constructors which take a dbcontext.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class StoredProcedureBaseForEF<TReturn, TParameters>
        : StoredProcedureFramework.Base.StoredProcedureBase<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class
    {
        #region Fields

        private readonly DbContext _context;

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> class.
        ///// Sets the procedure name to match the stored procedure class.
        ///// </summary>
        ///// <param name="context">The context.</param>
        //private StoredProcedureBase(DbContext context)
        //    : base()
        //{
        //    _context = context;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEF{TReturn, TParameters}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBaseForEF(DbContext context, TParameters parameters)
            : base(parameters)
        {
            // Validate arguments
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEF{TReturn, TParameters}" />
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected StoredProcedureBaseForEF(DbContext context,
            string procedureName,
            TParameters parameters)
            : base(procedureName, parameters)
        {
            // Validate arguments
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEF{TReturn, TParameters}" />
        /// class with parameters, schema name and procedure name.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected StoredProcedureBaseForEF(DbContext context,
            string schemaName,
            string procedureName,
            TParameters parameters)
            : base(procedureName, parameters)
        {
            // Validate arguments
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        #endregion

        #region Members

        /// <summary>
        /// Executes this instance against the DbContext which this instance was
        /// constructred with.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="commandTimeout">The command timeout. [optional]</param>
        /// <param name="commandBehavior">The command behavior. [optional]</param>
        /// <param name="transaction">The transaction. [optional]</param>
        /// <returns></returns>
        public TReturn ExecuteFor(
            TParameters parameters,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            // Validate arguments
            if (parameters == null) throw new ArgumentNullException("parameters");

            SetParameters(parameters);
            EnsureFullyConstructed();

            return Execute(commandTimeout, commandBehavior, transaction);
        }

        /// <summary>
        /// Executes this instance against the DbContext which this instance was
        /// constructred with.
        /// </summary>
        /// <param name="commandTimeout">The command timeout. [optional]</param>
        /// <param name="commandBehavior">The command behavior. [optional]</param>
        /// <param name="transaction">The transaction. [optional]</param>
        /// <returns></returns>
        public TReturn Execute(
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            IStoredProcedure<TReturn, TParameters> storedProcedure = this;
            TReturn result = _context.ExecuteStoredProcedure<TReturn, TParameters>(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);

            return result;
        }

       
        #endregion
    }
}
