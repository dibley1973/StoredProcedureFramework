﻿using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Extensions
{
    /// <summary>
    /// Extension methods for the DbConnection object
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Creates the stored procedure command.
        /// </summary>
        /// <param name="connection">The connection we are extending.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="procedureParameters">The procedure parameters.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private static DbCommand CreateStoredProcedureCommand(
            this DbConnection connection,
            string procedureName,
            IEnumerable<SqlParameter> procedureParameters,
            int? commandTimeout = null,
            SqlTransaction transaction = null)
        {
            DbCommand command = connection.CreateCommand();

            // Command to execute is our stored storedProcedure
            command.Transaction = transaction;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            // Assign command timeout value, if one was provided
            if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;

            // Transfer any parameters to the command
            if (procedureParameters != null)
            {
                LoadCommandParameters(procedureParameters, command);
            }

            return command;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
            this DbConnection connection,
            IStoredProcedure<TReturnType, TParameterType> storedProcedure,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            // Ensure the procedure is fully constructed
            storedProcedure.EnsureFullyConstructed();

            string procedureName = storedProcedure.GetTwoPartName();
            Type returnType = typeof(TReturnType);

            // Prepare the parameters if any exist
            IEnumerable<SqlParameter> procedureParameters =
                (storedProcedure.Parameters is NullStoredProcedureParameters) ?
                null :
                GetProcedureParameters(storedProcedure);

            // Populate results using an overload
            var results = ExecSproc<TReturnType>(
                connection,
                procedureName,
                returnType,
                procedureParameters,
                commandTimeout,
                commandBehavior,
                transaction);

            // return the results
            return results;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecSproc<TReturnType>(
            this DbConnection connection,
            string procedureName,
            Type outputType,
            IEnumerable<SqlParameter> procedureParameters = null,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null) where TReturnType : class
        {
            // Validate arguments
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (outputType == null) throw new ArgumentNullException("outputType");

            // A flag to track whether we opened the connection or not
            bool connectionWasOpen = (connection.State == ConnectionState.Open);

            try
            {
                // Create a result list
                List<TReturnType> results = new List<TReturnType>();

                // Open the connection if it is not
                if (!connectionWasOpen) connection.Open();

                // Create a command to execute the stored storedProcedure
                using (DbCommand command = connection.CreateStoredProcedureCommand(
                    procedureName,
                    procedureParameters,
                    commandTimeout,
                    transaction))
                {
                    // Populate a DataReder by calling the command
                    DbDataReader reader = command.ExecuteReader(commandBehavior);

                    // Get properties to save for the current destination type
                    PropertyInfo[] props = outputType.GetMappedProperties();

                    // Process the result set
                    while (reader.Read())
                    {
                        AddRecord(outputType, results, reader, props);
                    }

                    // Close the reader
                    reader.Close();
                }

                // Check if the procedure is defined as not to return anything
                if (typeof(TReturnType) == typeof(NullStoredProcedureResult))
                {
                    results = null;
                }

                // Return the results
                return results;
            }
            catch (Exception ex)
            {
                // We want to add a slightly more informative message to the
                // exception before rethrowing it
                var message = string.Format(
                    ExceptionMessages.ErrorReadingStoredProcedure,
                    procedureName,
                    ex.Message);

                // Option 1: Edit the actual message field insode the exception and rethrow
                //ex.GetType().GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ex, message);
                //throw ex;

                // Option 2: Create a new insatnce of the same type as the caught
                // exception with a new message, and throw that
                throw (Exception)Activator.CreateInstance(ex.GetType(), message, ex);
            }
            finally
            {
                if (connectionWasOpen) connection.Close();
            }
        }

        private static void AddRecord<TReturnType>(Type outputType, List<TReturnType> results, DbDataReader reader, PropertyInfo[] props)
        {
            // create an object to hold this result
            ConstructorInfo constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
            if (constructorInfo != null)
            {
                //TReturnType item = constructorInfo.Invoke(new TReturnType[0]);
                TReturnType item = Activator.CreateInstance<TReturnType>();
                if (item != null)
                {
                    // Copy data elements by parameter name from result to destination object
                    reader.ReadRecord(item, props);

                    // add newly populated item to our output list
                    results.Add(item);
                }
            }
        }

        private static IEnumerable<SqlParameter> GetProcedureParameters<TReturnType, TParameterType>(
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            // create mapped properties
            var mappedProperties = typeof(TParameterType).GetMappedProperties();

            // Create parameters from mapped properties
            var sqlParameters = mappedProperties.ToSqlParameters();

            // Populate parameters from storedProcedure parameters
            PopulateParameters(sqlParameters, mappedProperties, procedure);

            // Return parameters
            return sqlParameters;
        }

        /// <summary>
        /// Loads the command parameters.
        /// </summary>
        /// <param name="sqlParameters">The SQLParameters to load command with.</param>
        /// <param name="command">The command.</param>
        private static void LoadCommandParameters(IEnumerable<SqlParameter> sqlParameters, DbCommand command)
        {
            // Clear any existing command parameters
            if (command.Parameters.Count > 0) command.Parameters.Clear();

            // add the specified parameters
            foreach (SqlParameter p in sqlParameters)
            {
                command.Parameters.Add(p);
            }
        }

        private static void PopulateParameters<TReturnType, TParameterType>(
            ICollection<SqlParameter> sqlParameters,
            PropertyInfo[] mappedProperties,
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {

            // get the list of properties for this type
            //PropertyInfo[] mappedProperties = typeof(TParameterType).GetMappedProperties();

            // we want to write data back to properties for every non-input only parameter
            foreach (SqlParameter parm in sqlParameters
                .Where(p => p.Direction == ParameterDirection.Input)
                .Select(p => p))
            {
                // get the property name mapped to this parameter
                //String propname = storedProcedure.Parameters.Where(p => p.Key == parm.ParameterName).Select(p => p.Value).First();
                String propname = parm.ParameterName;

                // extract the matchingproperty and set its value
                PropertyInfo prop = mappedProperties.FirstOrDefault(p => p.Name == propname);
                if (prop != null) parm.Value = prop.GetValue(procedure.Parameters);
            }
        }
    }
}