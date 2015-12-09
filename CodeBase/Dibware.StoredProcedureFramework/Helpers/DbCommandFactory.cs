using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Represents a factory that creates DbCommands
    /// </summary>
    /// <remarks>
    /// TODO: Note this class is not currently in use but will be once 
    /// overloaded methods have been implemented throughout
    /// </remarks>
    public static class DbCommandFactory
    {
        //public static DbCommand CreateStoredProcedureCommand(
        //    DbConnection connection,
        //    string procedureName)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithCommandTimeout(
        //    DbConnection connection,
        //    string procedureName,
        //    int commandTimeout)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithCommandTimeout(commandTimeout)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithTransaction(
        //    DbConnection connection,
        //    string procedureName,
        //    SqlTransaction transaction)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithTransaction(transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithCommandTimeoutAndTransaction(
        //    DbConnection connection,
        //    string procedureName,
        //    int commandTimeout,
        //    SqlTransaction transaction)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithCommandTimeout(commandTimeout)
        //        .WithTransaction(transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithParameters(
        //    DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithParameters(procedureParameters)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithParametersAndCommandtimeout(
        //    DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters,
        //    int commandTimeout)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithParameters(procedureParameters)
        //        .WithCommandTimeout(commandTimeout)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithParametersAndTransaction(
        //    DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters,
        //    SqlTransaction transaction)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithParameters(procedureParameters)
        //        .WithTransaction(transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //public static DbCommand CreateStoredProcedureCommandWithParametersCommandTimeoutAndTransaction(
        //    DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters,
        //    int commandTimeout,
        //    SqlTransaction transaction)
        //{
        //    return StoredProcedureDbCommandCreator
        //        .CreateStoredProcedureDbCommandCreator(connection, procedureName)
        //        .WithParameters(procedureParameters)
        //        .WithCommandTimeout(commandTimeout)
        //        .WithTransaction(transaction)
        //        .BuildCommand()
        //        .Command;
        //}
    }
}
