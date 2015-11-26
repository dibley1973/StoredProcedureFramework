using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        public static DbCommand CreateStoredProcedureCommand(
            this DbConnection connection,
            string procedureName,
            IEnumerable<SqlParameter> procedureParameters,
            int? commandTimeout = null,
            SqlTransaction transaction = null)
        {
            DbCommand command = connection.CreateCommand();
            PrepareCommand(procedureName, commandTimeout, transaction, command);

            // Transfer any parameters to the command
            if (procedureParameters != null)
            {
                LoadCommandParameters(procedureParameters, command);
            }

            return command;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static TResultSetType ExecuteStoredProcedure<TResultSetType, TParameterType>(
            this DbConnection connection,
            IStoredProcedure<TResultSetType, TParameterType> storedProcedure,
            int? commandTimeoutOverride = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            storedProcedure.EnsureFullyConstructed();
            string procedureName = storedProcedure.GetTwoPartName();

            // Prepare the parameters if any exist
            IEnumerable<SqlParameter> procedureParameters = 
                (storedProcedure.HasNullStoredProcedureParameters) 
                    ? null 
                    : BuildProcedureParameters(storedProcedure);

            TResultSetType results = ExecuteStoredProcedure<TResultSetType>(
                connection,
                procedureName,
                procedureParameters,
                commandTimeoutOverride,
                commandBehavior,
                transaction);
            ProcessOutputParms(procedureParameters, storedProcedure);

            return results;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static TResultSetType ExecuteStoredProcedure<TResultSetType>(
            this DbConnection connection,
            string procedureName,
            IEnumerable<SqlParameter> procedureParameters = null,
            int? commandTimeoutOverride = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TResultSetType : class, new()
        {
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            bool connectionWasOpen = (connection.State == ConnectionState.Open);

            try
            {
                TResultSetType results; 

                if (!connectionWasOpen) connection.Open();

                // Create a command to execute the stored storedProcedure...
                using (DbCommand command = connection.CreateStoredProcedureCommand(
                    procedureName,
                    procedureParameters,
                    commandTimeoutOverride,
                    transaction))
                {
                    results = ExecuteCommand<TResultSetType>(commandBehavior, command);
                }

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

                // Option 2: Create a new instance of the same type as the caught
                // exception with a new message, and throw that
                //throw (Exception)Activator.CreateInstance(exceptionType, message, ex);
            }
            finally
            {
                if (!connectionWasOpen) connection.Close();  // Close connection if it arrived closed
            }
        }

        #region methods : private or protected
        
        private static TResultSetType ExecuteCommand<TResultSetType>(
            CommandBehavior commandBehavior,
            DbCommand command)
            where TResultSetType : class, new()
        {
            var procedureHasNoReturnType =
                (typeof(TResultSetType) == typeof(NullStoredProcedureResult));

            var results = procedureHasNoReturnType
                ? ExecuteCommandWithNoReturnType<TResultSetType>(command)
                : ExecuteCommandWithResultSet<TResultSetType>(commandBehavior, command);

            return results;
        }

        private static TResultSetType ExecuteCommandWithResultSet<TResultSetType>(
            CommandBehavior commandBehavior,
            DbCommand command)
            where TResultSetType : class, new()
        {
            TResultSetType resultSet = new TResultSetType();
            Type resultSetType = typeof(TResultSetType);

            string resultSetTypeName = resultSetType.Name;

            // Populate a DataReder by calling the command
            using (DbDataReader reader = command.ExecuteReader(commandBehavior))
            {
                bool isSingleRecordSet = ImplementsICollection(resultSetType);
                if (isSingleRecordSet)
                {
                    IList recordSetDtoList = (IList)new TResultSetType();
                    ReadRecordSet(reader, recordSetDtoList);
                    resultSet = (TResultSetType)recordSetDtoList;
                }
                else
                {
                    var recordSetIndex = 0;

                    PropertyInfo[] resultSetTypePropertyInfos = resultSetType.GetMappedProperties();

                    bool readerContainsAnotherResult;
                    do
                    {
                        var recordSetPropertyName = resultSetTypePropertyInfos[recordSetIndex].Name;
                        IList recordSetDtoList = GetRecordSetDtoList(resultSetType, recordSetPropertyName, resultSet);
                        EnsureRecorsetListIsInstantiated(recordSetDtoList, resultSetTypeName, recordSetPropertyName);
                        ReadRecordSet(reader, recordSetDtoList);

                        recordSetIndex++;
                        readerContainsAnotherResult = reader.NextResult();
                    }
                    while (readerContainsAnotherResult);
                }
                reader.Close();
            }
            return resultSet;
        }

        private static bool ImplementsICollection(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            
            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static TResultSetType ExecuteCommandWithNoReturnType<TResultSetType>(DbCommand command)
            where TResultSetType : class, new()
        {
            command.ExecuteNonQuery();
            return null;
        }

        private static void ReadRecordSet(DbDataReader reader, IList recordSetDtoList)
        {
            Type dtoListItemType = recordSetDtoList.GetType().GetGenericArguments()[0];
            PropertyInfo[] dtoListItemTypePropertyInfo = dtoListItemType.GetMappedProperties();

            while (reader.Read())
            {
                AddRecordToResults(dtoListItemType, recordSetDtoList, reader, dtoListItemTypePropertyInfo);
            }
        }

        private static IList GetRecordSetDtoList<TResultSetType>(
            Type resultSetType,
            string recordSetPropertyName,
            TResultSetType resultSet)
            where TResultSetType : class, new()
        {
            PropertyInfo recordSetPropertyInfo = resultSetType.GetProperty(recordSetPropertyName);
            IList recordSetDtoList = (IList)recordSetPropertyInfo.GetValue(resultSet);
            return recordSetDtoList;
        }

        private static void EnsureRecorsetListIsInstantiated(
            IList dtoList,
            string resultSetTypeName,
            string listPropertyName)
        {

            if (dtoList == null)
            {
                string errorMessage = string.Format(
                   ExceptionMessages.RecordSetListNotInstatiated,
                   resultSetTypeName,
                   listPropertyName);

                throw new NullReferenceException(errorMessage);
            }
        }

        private static void AddRecordToResults(
            Type outputType,
            IList results,
            DbDataReader reader,
            PropertyInfo[] dtoListItemTypePropertyInfos)
        {
            var constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
            bool noConstructorDefined = (constructorInfo == null);
            if (noConstructorDefined) return;

            //TODO: Investigate FastActivator
            // Even at 2M records there is still neglidgable difference between
            // standard Activator and FastActivator
            //var item = FastActivator.CreateInstance(outputType);
            //var item = FastActivator2.CreateInstance(outputType);

            var item = Activator.CreateInstance(outputType);
            reader.ReadRecord(item, dtoListItemTypePropertyInfos);
            results.Add(item);
        }

        private static ICollection<SqlParameter> BuildProcedureParameters<TReturnType, TParameterType>(
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            var mappedProperties = procedure.Parameters.GetType().GetMappedProperties();
            var sqlParameters = SqlParameterHelper.CreateSqlParametersFromPropertyInfoArray(mappedProperties);

            // TODO: Investigate if we can set VARCHAR size from the value of 
            // the parameters, and where is best to perform this... 
            // Refer to Issue #1

            PopulateSqlParametersFromProperties(sqlParameters, mappedProperties, procedure);

            return sqlParameters;
        }

        private static void LoadCommandParameters(IEnumerable<SqlParameter> sqlParameters, DbCommand command)
        {
            bool parametersRequireClearing = (command.Parameters.Count > 0);
            if (parametersRequireClearing) command.Parameters.Clear();

            foreach (SqlParameter parameter in sqlParameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        private static void ProcessOutputParms<TReturnType, TParameterType>(IEnumerable<SqlParameter> procedureSqlParameters,
            IStoredProcedure<TReturnType, TParameterType> storedProcedure)
            where TReturnType : class
            where TParameterType : class
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            bool noParametersToProcess = (procedureSqlParameters == null || storedProcedure.HasNullStoredProcedureParameters);
            if(noParametersToProcess) return;

            var mappedProperties = typeof(TParameterType).GetMappedProperties();
            var outputParameters = procedureSqlParameters
                .Where(p => p.Direction != ParameterDirection.Input)
                .Select(p => p);
            TParameterType storedProcedureParameters = storedProcedure.Parameters;

            foreach (SqlParameter outputParameter in outputParameters)
            {
                SetOutputParameterValue(outputParameter, mappedProperties, storedProcedureParameters);
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
                String propertyName = sqlParameter.ParameterName;
                PropertyInfo mappedPropertyInfo = mappedProperties.FirstOrDefault(p => p.Name == propertyName);
                if (mappedPropertyInfo == null) throw new NullReferenceException("Mapped property not found");

                // Use the PropertyInfo to get the value from the parameters,
                // then validate the value and if validation passes, set it 
                object value = mappedPropertyInfo.GetValue(procedure.Parameters);
                ValidateValueIsInRangeForSqlParameter(sqlParameter, value);
                SetSqlParameterValue(value, sqlParameter);
            }
        }

        private static void PrepareCommand(string procedureName, 
            int? commandTimeout,
            SqlTransaction transaction,
            DbCommand command)
        {
            command.Transaction = transaction;
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;
            if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;
        }

        private static void SetOutputParameterValue<TParameterType>(SqlParameter outputParameter,
            PropertyInfo[] mappedProperties, TParameterType storedProcedureParameters) where TParameterType : class
        {
            String propertyName = outputParameter.ParameterName;
            PropertyInfo matchedPropertyInfo = mappedProperties.FirstOrDefault(p => p.Name == propertyName);
            if (matchedPropertyInfo != null)
                matchedPropertyInfo.SetValue(storedProcedureParameters, outputParameter.Value, null);
        }

        private static void SetSqlParameterValue(object value, SqlParameter sqlParameter)
        {
            bool parameterValueIsNull = (value == null);
            bool parameterValueIsTableValuedParameter = (sqlParameter.SqlDbType == SqlDbType.Structured);

            if (parameterValueIsNull)
            {
                sqlParameter.Value = DBNull.Value;
            }
            else if (!parameterValueIsTableValuedParameter)
            {
                sqlParameter.Value = value;
            }
            else
            {
                sqlParameter.Value = TableValuedParameterHelper.GetTableValuedParameterFromList((IList)value);
            }
        }

        private static void ValidateValueIsInRangeForSqlParameter(SqlParameter sqlParameter, object value)
        {
            if (sqlParameter.RequiresPrecisionAndScaleValidation()) ValidateDecimal(sqlParameter, value);
            
            if (sqlParameter.RequiresLengthValidation()) ValidateString(sqlParameter, value);
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
            if (value is string)
            {
                var validator = new SqlParameterStringValueValidator();
                validator.Validate((string)value, sqlParameter);
            }
            else if (value is char[])
            {
                // ... and validate it if it is
                var validator = new SqlParameterStringValueValidator();
                validator.Validate(new string((char[])value), sqlParameter);
            }
            else if (value == null)
            {
                bool parameterIsStringOrNullable = sqlParameter.IsStringOrIsNullable();
                if (parameterIsStringOrNullable) return;

                string message = string.Format(
                    "'{0}' parameter had a null value",
                    sqlParameter.ParameterName);
                throw new SqlNullValueException(message);
            }
            else
            {
                throw new SqlParameterInvalidDataTypeException(
                    sqlParameter.ParameterName,
                    typeof(string), value.GetType());
            }
        }

        #endregion
    }
}