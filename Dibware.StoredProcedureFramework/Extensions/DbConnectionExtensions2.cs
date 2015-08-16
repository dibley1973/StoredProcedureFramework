using Dibware.StoredProcedureFramework.Contracts;
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
    public static class DbConnectionExtensions2
    {
        public static DbCommand CreateStoredProcedureCommand(
            this DbConnection connection,
            string procedureName,
            IEnumerable<SqlParameter> procedureParameters,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
        {
            DbCommand command = connection.CreateCommand();

            // Command to execute is our stored procedure
            command.Transaction = transaction;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            // Assign command timeout value, if one was provided
            if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;

            // Transfer any parameters
            if (procedureParameters != null)
            {
                LoadCommandParameters(procedureParameters, command);
            }

            return command;
        }


        public static List<IReturnType> ExecuteStoredProcedure1<TProcedure>(TProcedure procedure1)
            where TProcedure : IStoredProcedure2<IReturnType, IParameterType>
        {

            return new List<IReturnType>();

        }


        public static List<IReturnType> ExecuteStoredProcedure<TProcedure>(TProcedure procedure1)
            where TProcedure : IStoredProcedure<IReturnType, IParameterType>
        {

            return new List<IReturnType>();

        }



        //public static List<TReturnType> ExecuteStoredProcedure2<TProcedure>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure2<TReturnType, TParameterType>
        //    where TReturnType : class
        //    where TParameterType : class
        //{

        //    return new List<IReturnType>();

        //}

        //public static void ExecuteStoredProcedure2<TProcedure>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void ExecuteStoredProcedure3<IStoredProcedure<TReturnType, TParameterType>>(IStoredProcedure procedure)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        //public static void ExecSproc<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void DoAction3<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void DoAction<TReturnType, TParameterType>
        //    (IStoredProcedure<TReturnType, TParameterType> procedure)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
            this DbConnection connection,
            IStoredProcedure<TReturnType, TParameterType> procedure,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            string procedureName = procedure.GetTwoPartName();
            Type parameterType = typeof(TParameterType);
            Type returnType = typeof(TReturnType);

            // Prepare the parameters if any exist
            IEnumerable<SqlParameter> procedureParameters =
                (procedure.Parameters is NullStoredProcedureParameters) ?
                null :
                GetProcedureParameters(procedure);

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

                // Create a command to execute the stored procedure
                using (DbCommand command = connection.CreateStoredProcedureCommand(
                    procedureName,
                    procedureParameters,
                    commandTimeout,
                    commandBehavior,
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

                return results;

                //// Create a command to execute the stored procedure
                //using (DbCommand command = connection.CreateCommand())
                //{
                //    // Command to execute is our stored procedure
                //    command.Transaction = transaction;
                //    command.CommandText = procedureName;
                //    command.CommandType = CommandType.StoredProcedure;

                //    // Assign command timeout value, if one was provided
                //    if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;

                //    // Transfer any parameters
                //    if (procedureParameters != null)
                //    {
                //        LoadCommandParameters(procedureParameters, command);
                //    }

                //    // Populate a DataReder by calling the command
                //    DbDataReader reader = command.ExecuteReader(commandBehavior);

                //    // Get properties to save for the current destination type
                //    PropertyInfo[] mappedProperties = outputType.GetMappedProperties();

                //    // Process the result set
                //    while (reader.Read())
                //    {
                //        AddRecord(outputType, results, reader, mappedProperties);
                //    }

                //    // Close the reader
                //    reader.Close();
                //}
            }
            catch (Exception ex)
            {
                var message = string.Format(
                    ExceptionMessages.ErrorReadingStoredProcedure,
                    procedureName,
                    ex.Message);
                throw new Exception(message, ex);
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

        //public static void DoAction4<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        private static IEnumerable<SqlParameter> GetProcedureParameters<TReturnType, TParameterType>(
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            // create mapped properties
            var mappedProperties = typeof(TParameterType).GetMappedProperties();

            // Create parameters from mapped properties
            var sqlParameters = mappedProperties.ToSqlParameters();

            // Populate parameters from procedure parameters
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
                //String propname = procedure.Parameters.Where(p => p.Key == parm.ParameterName).Select(p => p.Value).First();
                String propname = parm.ParameterName;

                // extract the matchingproperty and set its value
                PropertyInfo prop = mappedProperties.FirstOrDefault(p => p.Name == propname);
                parm.Value = prop.GetValue(procedure.Parameters);


                //prop.SetValue(procedure.Parameters, parm.Value, null);
            }
            //throw new NotImplementedException();
        }

    }
}