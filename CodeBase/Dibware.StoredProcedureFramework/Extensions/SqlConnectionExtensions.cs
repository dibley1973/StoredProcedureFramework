using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// Executes the SQL function procedure  and gets the results..
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="sqlFunction">The SQl function.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        public static TResultSetType ExecuteSqlScalarFunction<TResultSetType, TParameterType>(
            this SqlConnection connection,
            ISqlFunction<TResultSetType, TParameterType> sqlFunction,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : new()
            where TParameterType : class
        {
            if (sqlFunction == null) throw new ArgumentNullException("sqlFunction");

            sqlFunction.EnsureIsFullyConstructed();

            DbConnection dbConnection = connection;

            return dbConnection.ExecuteSqlScalarFunction(
                sqlFunction,
                commandTimeout,
                commandBehavior,
                transaction);
        }

        /// <summary>
        /// Executes the SQL function procedure  and gets the results..
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="sqlFunction">The SQl function.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        public static TResultSetType ExecuteSqlTableFunction<TResultSetType, TParameterType>(
            this SqlConnection connection,
            ISqlFunction<TResultSetType, TParameterType> sqlFunction,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (sqlFunction == null) throw new ArgumentNullException("sqlFunction");

            sqlFunction.EnsureIsFullyConstructed();

            DbConnection dbConnection = connection;

            return dbConnection.ExecuteSqlTableFunction(
                sqlFunction,
                commandTimeout,
                commandBehavior,
                transaction);
        }

        /// <summary>
        /// Executes the stored procedure  and gets the results..
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        public static TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
            this SqlConnection connection,
            IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            storedProcedure.EnsureIsFullyConstructed();

            DbConnection dbConnection = connection;

            return dbConnection.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}