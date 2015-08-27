using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;

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

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecuteStoredProcedure<TReturnType, TParameterType>(
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
                BuildProcedureParameters(storedProcedure);

            // Populate results using an overload
            var results = ExecuteStoredProcedure<TReturnType>(
                connection,
                procedureName,
                returnType,
                procedureParameters,
                commandTimeout,
                commandBehavior,
                transaction);

            // Process any output parameters
            ProcessOutputParms(procedureParameters, storedProcedure);

            // return the results
            return results;
        }

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecuteStoredProcedure<TReturnType>(
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
                List<TReturnType> results; // = new List<TReturnType>();

                // Open the connection if it is not
                if (!connectionWasOpen) connection.Open();

                // Create a command to execute the stored storedProcedure
                using (DbCommand command = connection.CreateStoredProcedureCommand(
                    procedureName,
                    procedureParameters,
                    commandTimeout,
                    transaction))
                {
                    // Check the type of the ReturnType to determine if the 
                    // procedure is defined as not to return anything
                    if (typeof(TReturnType) == typeof(NullStoredProcedureResult))
                    {
                        results = null;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        results = new List<TReturnType>();

                        // Populate a DataReder by calling the command
                        using (DbDataReader reader = command.ExecuteReader(commandBehavior))
                        {
                            // Get properties to save for the current destination type
                            PropertyInfo[] props = outputType.GetMappedProperties();

                            // Process the result set line by line
                            while (reader.Read())
                            {
                                AddRecordToResults(outputType, results, reader, props);
                            }

                            // Close the reader
                            reader.Close();
                        }
                    }
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

                Type exceptionType = ex.GetType();

                // Option 1: Edit the actual message field insode the exception and rethrow
                var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null) fieldInfo.SetValue(ex, message);
                throw;

                // Option 2: Create a new insatnce of the same type as the caught
                // exception with a new message, and throw that
                //throw (Exception)Activator.CreateInstance(exceptionType, message, ex);
            }
            finally
            {
                if (connectionWasOpen) connection.Close();
            }
        }

        //public static void ValidateParemeters<TReturnType, TParameterType>(
        //    this DbConnection connection,
        //    IStoredProcedure<TReturnType, TParameterType> storedProcedure)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // Validate arguments
        //    if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

        //    // Ensure the procedure is fully constructed
        //    storedProcedure.EnsureFullyConstructed();

        //    // Get the procedure name
        //    string procedureName = storedProcedure.GetTwoPartName();

        //    // Prepare the parameters if any exist
        //    IEnumerable<SqlParameter> procedureParameters =
        //        (storedProcedure.Parameters is NullStoredProcedureParameters) ?
        //        null :
        //        BuildProcedureParameters(storedProcedure);

        //    // Pass through to overloaded method
        //    ValidateParemeters(connection, procedureName, procedureParameters);
        //}

        //public static void ValidateParemeters(
        //    this DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters)
        //{
        //    // Validate arguments
        //    if (string.IsNullOrEmpty(procedureName)) throw new ArgumentNullException("procedureName");
        //    if (procedureParameters == null) throw new ArgumentNullException("procedureParameters");

        //    // A flag to track whether we opened the connection or not
        //    bool connectionWasOpen = (connection.State == ConnectionState.Open);

        //    try
        //    {
        //        // Open the connection if it is not
        //        if (!connectionWasOpen) connection.Open();

        //        // Create a command to execute the stored storedProcedure
        //        using (DbCommand command = connection.CreateStoredProcedureCommand(
        //            procedureName,
        //            procedureParameters))
        //        {
        //            // Derive the parameters from teh stored procedure
        //            SqlCommandBuilder.DeriveParameters(command);
        //            connection.Database.
        //            DbCommandBuilder.D

        //            // Check the derived parameters against the 

                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //// We want to add a slightly more informative message to the
        //        //// exception before rethrowing it
        //        //var message = string.Format(
        //        //    ExceptionMessages.ErrorReadingStoredProcedure,
        //        //    procedureName,
        //        //    ex.Message);

        //        //Type exceptionType = ex.GetType();

        //        //// Option 1: Edit the actual message field insode the exception and rethrow
        //        //var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);
        //        //if (fieldInfo != null) fieldInfo.SetValue(ex, message);
        //        //throw;

        //        //// Option 2: Create a new insatnce of the same type as the caught
        //        //// exception with a new message, and throw that
        //        ////throw (Exception)Activator.CreateInstance(exceptionType, message, ex);
        //    }
        //    finally
        //    {
        //        if (connectionWasOpen) connection.Close();
        //    }
        //}



        /// <summary>
        /// Adds the record to results.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <param name="outputType">Type of the output.</param>
        /// <param name="results">The results.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="props">The props.</param>
        private static void AddRecordToResults<TReturnType>(Type outputType, List<TReturnType> results, DbDataReader reader, PropertyInfo[] props)
        {
            // Providing we have a constructor...
            ConstructorInfo constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
            if (constructorInfo == null) return;

            // ...create an object to hold this result

            //TReturnType item = constructorInfo.Invoke(new TReturnType[0]);
            //TODO: Investigate FastActivator
            TReturnType item = Activator.CreateInstance<TReturnType>();
            if (item == null) return;

            // Providing we created an object
            // Copy data elements by parameter name from result to destination object
            reader.ReadRecord(item, props);

            // add newly populated item to our output list
            results.Add(item);
        }

        private static ICollection<SqlParameter> BuildProcedureParameters<TReturnType, TParameterType>(
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            // create mapped properties
            var mappedProperties = typeof(TParameterType).GetMappedProperties();

            // Create parameters from mapped properties
            //var sqlParameters = mappedProperties.ToSqlParameters();
            var sqlParameters = SqlParameterHelper.CreateSqlParametersFromPropertyInfoArray(mappedProperties);

            // Populate parameters from storedProcedure parameters
            PopulateSqlParametersFromProperties(sqlParameters, mappedProperties, procedure);

            // Return parameters
            return sqlParameters;
        }



        private static void ProcessOutputParms<TReturnType, TParameterType>(IEnumerable<SqlParameter> procedureParameters,
            IStoredProcedure<TReturnType, TParameterType> storedProcedure)
            where TReturnType : class
            where TParameterType : class
        {
            // Validate arguments
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");
            
            // If the procdeure has no parameters then bug out of this method
            if (storedProcedure.Parameters is NullStoredProcedureParameters || 
                procedureParameters == null) return;

            // create mapped properties
            var mappedProperties = typeof(TParameterType).GetMappedProperties();

            // we want to write data back to properties for every non-input only parameter
            foreach (SqlParameter sqlParameter in procedureParameters
                .Where(p => p.Direction != ParameterDirection.Input)
                .Select(p => p))
            {
                // get the property name mapped to this parameter
                //String propertyName = MappedParams.Where(p => p.Key == sqlParameter.ParameterName).Select(p => p.Value).First();
                String propertyName = sqlParameter.ParameterName;

                // extract the matchingproperty and set its value
                PropertyInfo propertyInfo = mappedProperties.Where(p => p.Name == propertyName).FirstOrDefault();
                if (propertyInfo != null) propertyInfo.SetValue(storedProcedure.Parameters, sqlParameter.Value, null);
            }
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

        private static void PopulateSqlParametersFromProperties<TReturnType, TParameterType>(
            ICollection<SqlParameter> sqlParameters,
            PropertyInfo[] mappedProperties,
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            // Get all input type parameters
            foreach (SqlParameter sqlParameter in sqlParameters
                .Where(p => p.Direction == ParameterDirection.Input)
                .Select(p => p))
            {
                // Get the property name mapped to this parameter
                String propertyName = sqlParameter.ParameterName;

                // Extract the matching property...
                PropertyInfo propertyInfo = mappedProperties.FirstOrDefault(p => p.Name == propertyName);
                if (propertyInfo == null) throw new NullReferenceException("Mapped property not found");

                // Use the PropertyInfo to get the value from teh parameters,
                // then validate the value and if validation passes, set it 
                var value = propertyInfo.GetValue(procedure.Parameters);
                ValidateParameterValueIsInRange(sqlParameter, value);
                if (value == null) value = DBNull.Value; /* Handle null values */
                sqlParameter.Value = value;
            }
        }

        private static void ValidateParameterValueIsInRange(SqlParameter sqlParameter, object value)
        {
            // For parameters that have precision and scale we need to validate the value
            if (sqlParameter.RequiresPrecisionAndScaleValidation())
            {
                ValidateDecimal(sqlParameter, value);
            }

            if (sqlParameter.RequiresLengthValidation())
            {
                ValidateString(sqlParameter, value);
            }
        }

        private static void ValidateDecimal(SqlParameter sqlParameter, object value)
        {
            if (value is decimal)
            {
                var validator = new SqlParameterDecimalValueValidator();
                validator.Validate((decimal)value, sqlParameter);
            }
            else
            {
                throw new SqlParameterInvalidDataTypeException(
                    sqlParameter.ParameterName,
                    typeof(decimal), value.GetType());
            }
        }

        private static void ValidateString(SqlParameter sqlParameter, object value)
        {
            // Check if the value type is valid type... 
            if (value is string)
            {
                // ... and validate it if it is
                var validator = new SqlParameterStringValueValidator();
                validator.Validate((string)value, sqlParameter);
            }
            else if (value is char[])
            {
                // ... and validate it if it is
                var validator = new SqlParameterStringValueValidator();
                validator.Validate(new string((char[])value), sqlParameter);
            }
            else
            {
                // Throw a wobbler!
                throw new SqlParameterInvalidDataTypeException(
                    sqlParameter.ParameterName,
                    typeof(string), value.GetType());
            }
        }
    }
}