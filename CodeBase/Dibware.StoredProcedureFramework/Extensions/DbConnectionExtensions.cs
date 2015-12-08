using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Dibware.StoredProcedureFramework.Extensions
{
    /// <summary>
    /// Extension methods for the DbConnection object
    /// </summary>
    public static class DbConnectionExtensions
    {
        // TODO: Remove as only a test directly references this!
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
            var commandBuilder = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(connection, procedureName);

            // TODO: Investigate overloaded method rather than optional parameters
            // as that would allow this to be much neater
            if (procedureParameters != null) commandBuilder.WithParameters(procedureParameters);
            if (commandTimeout.HasValue) commandBuilder.WithCommandTimeout(commandTimeout.Value);
            if (transaction != null) commandBuilder.WithTransaction(transaction);

            commandBuilder.BuildCommand();
            return commandBuilder.Command;
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
            storedProcedure.EnsureIsFullyConstructed();

            string procedureName = storedProcedure.GetTwoPartName();
            var procedureSqlParameters = BuildProcedureParametersIfTheyExist(storedProcedure);

            //TResultSetType results = ExecuteStoredProcedure<TResultSetType>(
            //    connection,
            //    procedureName,
            //    procedureSqlParameters,
            //    commandTimeoutOverride,
            //    commandBehavior,
            //    transaction);

            // TODO: complete implementation and remove static call above this!
            TResultSetType results;
            using (var procedureExecuter = new StoredProcedureExecuter<TResultSetType>(connection, procedureName))
            {
                if (commandTimeoutOverride.HasValue) procedureExecuter.WithCommandTimeoutOverride(commandTimeoutOverride.Value);
                if (procedureSqlParameters != null) procedureExecuter.WithParameters(procedureSqlParameters);
                if (transaction != null) procedureExecuter.WithTransaction(transaction);
                results = procedureExecuter
                    .WithCommandBehavior(commandBehavior)
                    .Execute()
                    .Results;
            }

            new OutputParameterValueProcessor<TResultSetType, TParameterType>(
                procedureSqlParameters,
                storedProcedure).Processs();

            return results;
        }

        //[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        //private static TResultSetType ExecuteStoredProcedure<TResultSetType>(
        //    this DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters = null,
        //    int? commandTimeoutOverride = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null)
        //    where TResultSetType : class, new()
        //{
        //    if (procedureName == null) throw new ArgumentNullException("procedureName");
        //    if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

        //    bool connectionWasOpen = (connection.State == ConnectionState.Open);

        //    try
        //    {
        //        TResultSetType results;

        //        if (!connectionWasOpen) connection.Open();



        //        //// Create a command to execute the stored storedProcedure...
        //        //using (DbCommand command = connection.CreateStoredProcedureCommand(
        //        //    procedureName,
        //        //    procedureParameters,
        //        //    commandTimeoutOverride,
        //        //    transaction))
        //        //{
        //        //    results = ExecuteCommand<TResultSetType>(commandBehavior, command);
        //        //}


        //        using (var executer = new StoredProcedureExecuter<TResultSetType>(connection, procedureName))
        //        {
        //            if (commandTimeoutOverride.HasValue) executer.WithCommandTimeoutOverride(commandTimeoutOverride.Value);
        //            if (procedureParameters != null) executer.WithParameters(procedureParameters);
        //            if (transaction != null) executer.WithTransaction(transaction);
        //            executer.Execute();
        //            results = executer.Results;
        //        }


        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        // We want to add a slightly more informative message to the
        //        // exception before rethrowing it
        //        var message = string.Format(
        //            ExceptionMessages.ErrorReadingStoredProcedure,
        //            procedureName,
        //            ex.Message);

        //        Type exceptionType = ex.GetType();

        //        // Option 1: Edit the actual message field insode the exception and rethrow
        //        var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);
        //        if (fieldInfo != null) fieldInfo.SetValue(ex, message);
        //        throw;

        //        // Option 2: Create a new instance of the same type as the caught
        //        // exception with a new message, and throw that
        //        //throw (Exception)Activator.CreateInstance(exceptionType, message, ex);
        //    }
        //    finally
        //    {
        //        if (!connectionWasOpen) connection.Close();  // Close connection if it arrived closed
        //    }
        //}

        #region methods : private or protected

        private static ICollection<SqlParameter> BuildProcedureParametersIfTheyExist<TResultSetType, TParameterType>(
            IStoredProcedure<TResultSetType, TParameterType> storedProcedure)
            where TResultSetType : class, new()
            where TParameterType : class
        {
            var sqlParameterBuilder = new StoredProcedureSqlParameterBuilder<TResultSetType, TParameterType>(storedProcedure);

            sqlParameterBuilder.BuildSqlParameters();
            var result = sqlParameterBuilder.SqlParameters;

            return result;
        }

        //private static TResultSetType ExecuteCommand<TResultSetType>(
        //    CommandBehavior commandBehavior,
        //    DbCommand command)
        //    where TResultSetType : class, new()
        //{
        //    var procedureHasNoReturnType =
        //        (typeof(TResultSetType) == typeof(NullStoredProcedureResult));

        //    var results = procedureHasNoReturnType
        //        ? ExecuteCommandWithNoReturnType<TResultSetType>(command)
        //        : ExecuteCommandWithResultSet<TResultSetType>(commandBehavior, command);

        //    return results;
        //}

        //private static TResultSetType ExecuteCommandWithResultSet<TResultSetType>(
        //    CommandBehavior commandBehavior,
        //    DbCommand command)
        //    where TResultSetType : class, new()
        //{
        //    TResultSetType resultSet = new TResultSetType();
        //    Type resultSetType = typeof(TResultSetType);

        //    string resultSetTypeName = resultSetType.Name;

        //    // Populate a DataReder by calling the command
        //    using (DbDataReader reader = command.ExecuteReader(commandBehavior))
        //    {
        //        bool isSingleRecordSet = resultSetType.ImplementsICollectionInterface();
        //        if (isSingleRecordSet)
        //        {
        //            IList recordSetDtoList = (IList)new TResultSetType();
        //            ReadRecordSet(reader, recordSetDtoList);
        //            resultSet = (TResultSetType)recordSetDtoList;
        //        }
        //        else
        //        {
        //            var recordSetIndex = 0;

        //            PropertyInfo[] resultSetTypePropertyInfos = resultSetType.GetMappedProperties();

        //            bool readerContainsAnotherResult;
        //            do
        //            {
        //                var recordSetPropertyName = resultSetTypePropertyInfos[recordSetIndex].Name;
        //                IList recordSetDtoList = GetRecordSetDtoList(resultSetType, recordSetPropertyName, resultSet);
        //                EnsureRecorsetListIsInstantiated(recordSetDtoList, resultSetTypeName, recordSetPropertyName);
        //                ReadRecordSet(reader, recordSetDtoList);

        //                recordSetIndex++;
        //                readerContainsAnotherResult = reader.NextResult();
        //            }
        //            while (readerContainsAnotherResult);
        //        }
        //        reader.Close();
        //    }
        //    return resultSet;
        //}



        //private static TResultSetType ExecuteCommandWithNoReturnType<TResultSetType>(DbCommand command)
        //    where TResultSetType : class, new()
        //{
        //    command.ExecuteNonQuery();
        //    return null;
        //}

        //private static void ReadRecordSet(DbDataReader reader, IList recordSetDtoList)
        //{
        //    Type dtoListItemType = recordSetDtoList.GetType().GetGenericArguments()[0];
        //    PropertyInfo[] dtoListItemTypePropertyInfo = dtoListItemType.GetMappedProperties();

        //    while (reader.Read())
        //    {
        //        AddRecordToResults(dtoListItemType, recordSetDtoList, reader, dtoListItemTypePropertyInfo);
        //    }
        //}

        //private static IList GetRecordSetDtoList<TResultSetType>(
        //    Type resultSetType,
        //    string recordSetPropertyName,
        //    TResultSetType resultSet)
        //    where TResultSetType : class, new()
        //{
        //    PropertyInfo recordSetPropertyInfo = resultSetType.GetProperty(recordSetPropertyName);
        //    IList recordSetDtoList = (IList)recordSetPropertyInfo.GetValue(resultSet);
        //    return recordSetDtoList;
        //}

        //private static void EnsureRecorsetListIsInstantiated(
        //    IList dtoList,
        //    string resultSetTypeName,
        //    string listPropertyName)
        //{

        //    if (dtoList == null)
        //    {
        //        string errorMessage = string.Format(
        //           ExceptionMessages.RecordSetListNotInstatiated,
        //           resultSetTypeName,
        //           listPropertyName);

        //        throw new NullReferenceException(errorMessage);
        //    }
        //}

        //private static void AddRecordToResults(
        //    Type outputType,
        //    IList results,
        //    DbDataReader reader,
        //    PropertyInfo[] dtoListItemTypePropertyInfos)
        //{
        //    var constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
        //    bool noConstructorDefined = (constructorInfo == null);
        //    if (noConstructorDefined) return;

        //    //TODO: Investigate FastActivator
        //    // Even at 2M records there is still neglidgable difference between
        //    // standard Activator and FastActivator
        //    //var item = FastActivator.CreateInstance(outputType);
        //    //var item = FastActivator2.CreateInstance(outputType);

        //    var item = Activator.CreateInstance(outputType);
        //    reader.ReadRecord(item, dtoListItemTypePropertyInfos);
        //    results.Add(item);
        //}

        #endregion
    }
}