using Dibware.StoredProcedureFramework.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DbContextExtensions2
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
            // Get the context database connection...
            DbConnection connection = context.Database.Connection;

            // ... then return results  from secondary extention method.
            return connection.ExecSproc(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}