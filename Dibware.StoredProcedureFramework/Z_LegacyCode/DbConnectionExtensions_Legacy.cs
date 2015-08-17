
namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DbConnectionExtensions_Legacy
    {

        //public static List<T> ExecuteStoredProcedure<T>(
        //    this DbConnection connection,
        //    string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters = null,
        //    int? commandTimeout = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null)
        //{
        //    Type outputType = typeof(T);

        //    // Validate arguments
        //    if (procedureName == null) throw new ArgumentNullException("procedureName");
        //    if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

        //    // Create a results list
        //    List<T> results = new List<T>();

        //    // do some stuff....
        //    //GetResultsList(connection, procedureName, outputType, procedureParameters, commandTimeout, commandBehavior, transaction, results);

        //    // Return the results
        //    return results;
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
        //    this DbConnection connection,
        //    string procedureName,
        //    Type outputType,
        //    IEnumerable<SqlParameter> procedureParameters = null,
        //    int? commandTimeout = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null)
        //{
        //    // Validate arguments
        //    if (procedureName == null) throw new ArgumentNullException("procedureName");
        //    if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
        //    if (outputType == null) throw new ArgumentNullException("outputType");

        //    // Create a results list
        //    List<object> results = new List<object>();

        //    GetResultsList(connection, procedureName, outputType, procedureParameters, commandTimeout, commandBehavior, transaction, results);

        //    // Return the results
        //    return results;
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
        //public static ResultList<TR> ExecuteStoredProcedure<TR>(
        //    this DbConnection connection,
        //    string procedureName,
        //    Type outputType,
        //    IEnumerable<SqlParameter> procedureParameters = null,
        //    int? commandTimeout = null,
        //    CommandBehavior commandBehavior = CommandBehavior.Default,
        //    SqlTransaction transaction = null) where TR : class
        //{
        //    // Validate arguments
        //    if (procedureName == null) throw new ArgumentNullException("procedureName");
        //    if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
        //    if (outputType == null) throw new ArgumentNullException("outputType");

        //    // Create a results list
        //    //List<object> results = new List<object>();

        //    //GetResultsList(connection, procedureName, outputType, procedureParameters, commandTimeout, commandBehavior, transaction, results);

        //    //Type outputType = typeof(TR);

        //    // Return the results
        //    return GetResultsList<TR>(connection, procedureName, procedureParameters, commandTimeout, commandBehavior, transaction);
        //}


        //private static ResultList<TR> GetResultsList<TR>(DbConnection connection, string procedureName,
        //    IEnumerable<SqlParameter> procedureParameters, int? commandTimeout, CommandBehavior commandBehavior,
        //    SqlTransaction transaction)
        //    where TR : class
        //{
        //    Type outputType = typeof(TR);

        //    List<object> results = new List<object>();

        //    GetResultsList(connection, procedureName, outputType,
        //        procedureParameters, commandTimeout, commandBehavior, transaction,
        //        results);

        //    ResultList<TR> resultList = new ResultList<TR>(results);

        //    return resultList;
        //}

        //private static void GetResultsList(DbConnection connection, string procedureName, Type outputType,
        //    IEnumerable<SqlParameter> procedureParameters, int? commandTimeout, CommandBehavior commandBehavior, SqlTransaction transaction,
        //    List<object> results)
        //{
        //    // A flag to track whether we opened the connection or not
        //    bool connectionWasOpen = (connection.State == ConnectionState.Open);

        //    try
        //    {
        //        // Open the connection if it is not
        //        if (!connectionWasOpen) connection.Open();

        //        // Create a command to execute the stored procedure
        //        using (DbCommand command = connection.CreateCommand())
        //        {
        //            // Command to execute is our stored procedure
        //            command.Transaction = transaction;
        //            command.CommandText = procedureName;
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Assign command timeout value, if one was provided
        //            if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;

        //            // Transfer any parameters
        //            if (procedureParameters != null)
        //            {
        //                LoadCommandParameters(procedureParameters, command);
        //            }

        //            // Populate a DataReder by calling the command
        //            DbDataReader reader = command.ExecuteReader(commandBehavior);

        //            // Get properties to save for the current destination type
        //            PropertyInfo[] props = outputType.GetMappedProperties();

        //            // Process the result set
        //            while (reader.Read())
        //            {
        //                AddRecord(outputType, results, reader, props);
        //            }

        //            // Close the reader
        //            reader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = string.Format(
        //            ExceptionMessages.ErrorReadingStoredProcedure,
        //            procedureName,
        //            ex.Message);
        //        throw new Exception(message, ex);
        //    }
        //    finally
        //    {
        //        if (connectionWasOpen) connection.Close();
        //    }
        //}

        //private static void AddRecord(Type outputType, List<object> results, DbDataReader reader, PropertyInfo[] props)
        //{
        //    // create an object to hold this result
        //    ConstructorInfo constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
        //    if (constructorInfo != null)
        //    {
        //        object item = constructorInfo.Invoke(new object[0]);
        //        if (item != null)
        //        {
        //            // Copy data elements by parameter name from result to destination object
        //            reader.ReadRecord(item, props);

        //            // add newly populated item to our output list
        //            results.Add(item);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Loads the command parameters.
        ///// </summary>
        ///// <param name="sqlParameters">The SQLParameters to load command with.</param>
        ///// <param name="command">The command.</param>
        //private static void LoadCommandParameters(IEnumerable<SqlParameter> sqlParameters, DbCommand command)
        //{
        //    // Clear any existing command parameters
        //    if (command.Parameters.Count > 0) command.Parameters.Clear();

        //    // add the specified parameters
        //    foreach (SqlParameter p in sqlParameters)
        //    {
        //        command.Parameters.Add(p);
        //    }
        //}
    }
}
