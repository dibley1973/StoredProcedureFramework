using Dibware.Helpers.Validation;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Contracts;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework
{
    public class StoredProcedure
        : StoredProcedure<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public StoredProcedure(StoredProcedureContext context)
            : base(context)
        { }
    }

    public class StoredProcedure<TReturn>
       : StoredProcedure<TReturn, NullStoredProcedureParameters>
       where TReturn : class, new()
    {
        public StoredProcedure(StoredProcedureContext context)
            : base(context)
        { }
    }

    public class StoredProcedure<TReturn, TParameters>
        : StoredProcedureBase<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
    {
        private readonly StoredProcedureContext _context;

        public StoredProcedure(StoredProcedureContext context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
        }

        public StoredProcedureContext Context
        {
            get { return _context; }
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
    }
}