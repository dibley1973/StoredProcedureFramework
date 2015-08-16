﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DbContextExtensions2
    {
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
            this DbContext context,
            IStoredProcedure<TReturnType, TParameterType> procedure,
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
                procedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}