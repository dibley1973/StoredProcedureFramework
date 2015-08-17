using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class SqlConnectionExtensions
    {
        ///// <summary>
        ///// Executes the stored procedure  and gets the results..
        ///// </summary>
        ///// <param name="connection">The connection.</param>
        ///// <param name="storedProcedure">The stored procedure.</param>
        ///// <param name="commandTimeout">The command timeout.</param>
        ///// <param name="commandBehavior">The command behavior.</param>
        ///// <param name="transaction">The transaction.</param>
        ///// <returns></returns>
        ///// <exception cref="System.ArgumentNullException">storedProcedure</exception>
        //public static List<object> ExecuteStoredProcedure(
        //    this SqlConnection connection,
        //    StoredProcedure storedProcedure,
        //    int? commandTimeout = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null)
        //{
        //    // Validate arguments
        //    if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");
            
        //    storedProcedure.EnsureFullyConstrucuted();

        //    return ExecuteStoredProcedure(
        //        connection,
        //        storedProcedure.GetTwoPartName(),
        //        storedProcedure.ReturnType,
        //        storedProcedure.Parameters,
        //        commandTimeout,
        //        commandBehavior,
        //        transaction);
        //}


        ///// <summary>
        ///// Executes the stored procedure and gets the results.
        ///// </summary>
        ///// <param name="connection">This instance.</param>
        ///// <param name="procedureName">Name of the procedure.</param>
        ///// <param name="outputType">Type of the output.</param>
        ///// <param name="procedureParameters">The procedure parameters.</param>
        ///// <param name="commandTimeout">The command timeout.</param>
        ///// <param name="commandBehavior">The command behavior.</param>
        ///// <param name="transaction">The transaction.</param>
        ///// <exception cref="System.ArgumentNullException">
        ///// procedureName
        ///// or
        ///// outputType
        ///// </exception>
        ///// <exception cref="System.ArgumentOutOfRangeException">procedureName</exception>
        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //public static List<object> ExecuteStoredProcedure(
        //    this SqlConnection connection,
        //    string procedureName,
        //    Type outputType,
        //    IEnumerable<SqlParameter> procedureParameters = null,
        //    int? commandTimeout = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null)
        //{
        //    // Validate arguments
        //    if(procedureName == null) throw new ArgumentNullException("procedureName");
        //    if(procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
        //    if (outputType == null) throw new ArgumentNullException("outputType");

        //    // Downcast the connection to it's base and call through
        //    // to secondary extenstion method.
        //    DbConnection dbConnection = connection;
        //    return dbConnection.ExecuteStoredProcedure(
        //        procedureName,
        //        outputType,
        //        procedureParameters,
        //        commandTimeout,
        //        commandBehavior,
        //        transaction);
        //}


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
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
            this SqlConnection connection,
            IStoredProcedure<TReturnType, TParameterType> storedProcedure,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            storedProcedure.EnsureFullyConstrucuted();

            //string procedureName = storedProcedure.GetTwoPartName();
            //Type parameterType = typeof(TParameterType);
            //Type returnType = typeof(TReturnType);
            //DbConnection dbConnection = connection;

            //return dbConnection.ExecSproc<TReturnType>(
            //    procedureName,
            //    returnType,
            //    storedProcedure.Parameters,
            //    commandTimeout,
            //    commandBehavior,
            //    transaction);

            // Get the context database connection...
            DbConnection dbConnection = connection;

            // ... then return results  from secondary extention method.
            return dbConnection.ExecSproc(
                storedProcedure,
                commandTimeout,
                commandBehavior,
                transaction);
        }

        /// <summary>
        /// Executes the stored procedure and gets the results.
        /// </summary>
        /// <param name="connection">This instance.</param>
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
        public static List<TReturnType> ExecuteStoredProcedure<TReturnType, TParameterType>(
            this SqlConnection connection,
            string procedureName,
            Type outputType,
            IEnumerable<SqlParameter> procedureParameters = null,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            // Validate arguments
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (outputType == null) throw new ArgumentNullException("outputType");

            // Downcast the connection to it's base and call through
            // to secondary extenstion method.
            DbConnection dbConnection = connection;
            return dbConnection.ExecSproc<TReturnType>(
                procedureName,
                outputType,
                procedureParameters,
                commandTimeout,
                commandBehavior,
                transaction);
        }
    }
}