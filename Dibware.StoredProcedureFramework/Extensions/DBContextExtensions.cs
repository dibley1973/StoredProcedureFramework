using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DBContextExtensions
    {
        /// <summary>
        /// Executes the stored procedure and gets the results.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="outputType">Type of the output.</param>
        /// <param name="procedureParameters">The procedure parameters.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="transaction">The transaction.</param>
        /// <exception cref="System.ArgumentNullException">
        /// procedureName
        /// or
        /// outputType
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">procedureName</exception>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<object> ExecuteStoredProcedure(
            this DbContext context,
            string procedureName,
            Type outputType,
            IEnumerable<SqlParameter> procedureParameters = null,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            // Validate arguments
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (outputType == null) throw new ArgumentNullException("outputType");

            // Get the context database connection and call through
            // to secondary extenstion method.
            DbConnection connection = context.Database.Connection;
            return connection.ExecuteStoredProcedure(
                procedureName,
                outputType,
                procedureParameters,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}
