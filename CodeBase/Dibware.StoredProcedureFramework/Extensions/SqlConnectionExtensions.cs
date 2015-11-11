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
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            // Ensure the procedure is fully constructed before going any further
            storedProcedure.EnsureFullyConstructed();

            // Downcast the connection to it's base so we can call 
            // through to it's extenstion method.
            DbConnection dbConnection = connection;

            // Return the results of the call through to the 
            // extension method on the Dbconnection
            return dbConnection.ExecuteStoredProcedure(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}