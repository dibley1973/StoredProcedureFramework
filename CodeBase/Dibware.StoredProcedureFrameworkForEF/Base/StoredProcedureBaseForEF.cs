using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    /// <summary>
    /// Expands upon functionality of the <see cref="Dibware.StoredProcedureFramework.Base.StoredProcedureBase{TReturn, TParameters}"/> 
    /// class by adding support for execution from DbContext by providing
    /// constructors which take a dbcontext.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class StoredProcedureBaseForEf<TReturn, TParameters>
        : StoredProcedureFramework.Base.StoredProcedureBase<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEf{TReturn, TParameters}"/>
        /// class without parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">
        /// The DBContext in which this insatcne will be executed in.
        /// </param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected StoredProcedureBaseForEf(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEf{TReturn, TParameters}"/>
        /// class with parameters.
        /// </summary>
        /// <param name="context">The DBContext in which this insatcne will be executed in.</param>
        /// <param name="parameters">The parameters or null if no paremeters are required.</param>
        /// <exception cref="System.ArgumentNullException">
        /// The DBContext in which this insatcne will eb executed in.
        /// </exception>
        protected StoredProcedureBaseForEf(DbContext context, TParameters parameters)
            : base(parameters)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEf{TReturn, TParameters}"/>
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="context">The DBContext in which this insatcne will be executed in.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected StoredProcedureBaseForEf(DbContext context,
            string procedureName,
            TParameters parameters)
            : base(procedureName, parameters)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBaseForEf{TReturn, TParameters}"/>
        /// class with parameters, schema name and procedure name.
        /// </summary>
        /// <param name="context">The DBContext in which this insatcne will be executed in.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        protected StoredProcedureBaseForEf(DbContext context,
            string schemaName,
            string procedureName,
            TParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
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
        /// <remarks>
        /// TODO: Consider breaking this method out into the following:
        ///     ExecuteFor(TParameters parameters)
        ///     ExecuteForWithCommandBehavior(TParameters parameters, CommandBehavior commandBehavior)
        ///     ExecuteForWithTimeoutOverride(TParameters parameters, int? commandTimeoutOverride)
        ///     ExecuteForWithTransaction(TParameters parameters, SqlTransaction transaction)
        /// ...etc.
        /// </remarks>
        public TReturn ExecuteFor(
            TParameters parameters,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            SetParameters(parameters);
            EnsureIsFullyConstructed();

            return Execute(commandTimeout, commandBehavior, transaction);
        }

        /// <summary>
        /// Executes this instance against the DbContext which this instance was
        /// constructred with.
        /// </summary>
        /// <param name="commandTimeoutOverride">The command timeout. [optional]</param>
        /// <param name="commandBehavior">The command behavior. [optional]</param>
        /// <param name="transaction">The transaction. [optional]</param>
        /// <returns></returns>
        /// <remarks>
        /// TODO: Consider breaking this method out into the following:
        ///     Execute()
        ///     ExecuteWithCommandBehavior(CommandBehavior commandBehavior)
        ///     ExecuteWithTimeoutOverride(int? commandTimeoutOverride)
        ///     ExecuteWithTransaction(SqlTransaction transaction)
        /// ...etc.
        /// </remarks>
        public TReturn Execute(
            int? commandTimeoutOverride = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            IStoredProcedure<TReturn, TParameters> storedProcedure = this;

            if (storedProcedure.Parameters == null) SetParameters(new TParameters());

            return _context.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeoutOverride,
                commandBehavior,
                transaction);
        }

        #endregion

        #region Fields

        private readonly DbContext _context;

        #endregion
    }
}
