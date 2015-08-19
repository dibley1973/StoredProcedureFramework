using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;

namespace Dibware.StoredProcedureFrameworkForEF
{
    /// <summary>
    /// Extension methods for the DbContext object
    /// </summary>
    public static class DbContextExtensions
    {
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
               this DbContext context,
               IStoredProcedure<TReturnType, TParameterType> storedProcedure,
               int? commandTimeout = null,
               CommandBehavior commandBehavior = CommandBehavior.Default,
               SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            // Ensure the procedure is fully constructed before going any further
            storedProcedure.EnsureFullyConstructed();

            // Get the context database connection...
            DbConnection connection = context.Database.Connection;

            // ... then return results from secondary extention method.
            return connection.ExecSproc(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}