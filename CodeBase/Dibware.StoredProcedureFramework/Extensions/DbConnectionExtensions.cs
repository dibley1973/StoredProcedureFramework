using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Dibware.StoredProcedureFramework.Extensions
{
    /// <summary>
    /// Extension methods for the DbConnection object
    /// </summary>
    public static class DbConnectionExtensions
    {
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static TResultSetType ExecuteSqlFunction<TResultSetType, TParameterType>(
            this DbConnection instance,
            ISqlFunction<TResultSetType, TParameterType> sqlFunction,
            int? commandTimeoutOverride = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (sqlFunction == null) throw new ArgumentNullException("sqlFunction");
            sqlFunction.EnsureIsFullyConstructed();

            var sqlFunctionParameters =
                new SqlFunctionSqlParameterBuilder<TResultSetType, TParameterType>(sqlFunction)
                    .BuildSqlParameters()
                    .SqlParameters;
            string procedureFullName = sqlFunction.GetTwoPartName();
            bool withParametersSupplied = (sqlFunctionParameters != null);
            bool withCommandTimoutSupplied = (commandTimeoutOverride.HasValue);
            bool exeuteWithinTransaction = (transaction != null);
            TResultSetType results;

            using (var procedureExecuter = new SqlFunctionExecuter<TResultSetType>(instance, procedureFullName))
            {
                if (withCommandTimoutSupplied) procedureExecuter.WithCommandTimeoutOverride(commandTimeoutOverride.Value);
                if (withParametersSupplied) procedureExecuter.WithParameters(sqlFunctionParameters);
                if (exeuteWithinTransaction) procedureExecuter.WithTransaction(transaction);
                results = procedureExecuter
                    .WithCommandBehavior(commandBehavior)
                    .Execute()
                    .Results;
            }

            // TODO: Investigate if needed!
            //new OutputParameterValueProcessor<TResultSetType, TParameterType>(
            //    procedureSqlParameters,
            //    sqlFunction).Processs();

            return results;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
            this DbConnection instance,
            IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
            int? commandTimeoutOverride = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");
            storedProcedure.EnsureIsFullyConstructed();

            var procedureSqlParameters =
                new StoredProcedureSqlParameterBuilder<TResultSetType, TParameterType>(storedProcedure)
                    .BuildSqlParameters()
                    .SqlParameters;
            string procedureFullName = storedProcedure.GetTwoPartName();
            bool withParametersSupplied = (procedureSqlParameters != null);
            bool withCommandTimoutSupplied = (commandTimeoutOverride.HasValue);
            bool exeuteWithinTransaction = (transaction != null);
            TResultSetType results;

            using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
            {
                if (withCommandTimoutSupplied) procedureExecuter.WithCommandTimeoutOverride(commandTimeoutOverride.Value);
                if (withParametersSupplied) procedureExecuter.WithParameters(procedureSqlParameters);
                if (exeuteWithinTransaction) procedureExecuter.WithTransaction(transaction);
                results = procedureExecuter
                    .WithCommandBehavior(commandBehavior)
                    .Execute()
                    .Results;
            }

            new OutputParameterValueProcessor<TResultSetType, TParameterType>(
                procedureSqlParameters,
                storedProcedure).Processs();

            return results;
        }

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithParameters<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    ICollection<SqlParameter> procedureSqlParameters,
        //    string procedureFullName)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    if(procedureSqlParameters == null) throw new ArgumentNullException("procedureSqlParameters");

        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithParameters(procedureSqlParameters)
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .Execute()
        //            .Results;
        //    }
        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithParametersAndCommandTimeout<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    ICollection<SqlParameter> procedureSqlParameters,
        //    string procedureFullName,
        //    int commandTimeoutOverride)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    if (procedureSqlParameters == null) throw new ArgumentNullException("procedureSqlParameters");

        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithParameters(procedureSqlParameters)
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithCommandTimeoutOverride(commandTimeoutOverride)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithParametersAndTransaction<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    ICollection<SqlParameter> procedureSqlParameters,
        //    string procedureFullName,
        //    SqlTransaction transaction)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    if (procedureSqlParameters == null) throw new ArgumentNullException("procedureSqlParameters");

        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithParameters(procedureSqlParameters)
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithTransaction(transaction)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithParametersCommandTimeoutAndTransaction<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    ICollection<SqlParameter> procedureSqlParameters,
        //    string procedureFullName,
        //    int commandTimeoutOverride,
        //    SqlTransaction transaction)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    if (procedureSqlParameters == null) throw new ArgumentNullException("procedureSqlParameters");

        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithParameters(procedureSqlParameters)
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithCommandTimeoutOverride(commandTimeoutOverride)
        //            .WithTransaction(transaction)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithoutParameters<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    string procedureFullName)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .Execute()
        //            .Results;
        //    }
        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithoutParametersButWithCommandTimeout<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    string procedureFullName,
        //    int commandTimeoutOverride)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithCommandTimeoutOverride(commandTimeoutOverride)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithoutParametersButWithTransaction<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    string procedureFullName,
        //    SqlTransaction transaction)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithTransaction(transaction)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedureWithoutParametersButWithCommandTimeoutAndTransaction<TResultSetType, TParameterType>(
        //    this DbConnection instance,
        //    string procedureFullName,
        //    int commandTimeoutOverride,
        //    SqlTransaction transaction)
        //    where TResultSetType : class, new()
        //    where TParameterType : class
        //{
        //    TResultSetType results;
        //    using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(instance, procedureFullName))
        //    {
        //        results = procedureExecuter
        //            .WithCommandBehavior(CommandBehavior.Default)
        //            .WithCommandTimeoutOverride(commandTimeoutOverride)
        //            .WithTransaction(transaction)
        //            .Execute()
        //            .Results;
        //    }

        //    return results;
        //}
    }
}