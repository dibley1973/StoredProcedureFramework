namespace Dibware.StoredProcedureFramework.Contracts
{
    public interface ISqlFunction
    {
        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        /// <value>
        /// The name of the function.
        /// </value>
        string FunctionName { get; }

        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        string SchemaName { get; }

        /// <summary>
        /// Gets the combined schema and function name.
        /// </summary>
        /// <returns></returns>
        string GetTwoPartName();

        /// <summary>
        /// Sets the function name.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>
        /// This instance
        /// </returns>
        void SetFunctionName(string functionName);

        /// <summary>
        /// Sets the schema name.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns></returns>
        void SetSchemaName(string schemaName);
    }

    /// <summary>
    /// Defines the expected members and type parameters of an object that
    /// represents a Sql function
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameter.</typeparam>
    /// <remarks>
    /// TODO: refactor as shares similarities with 
    /// <see cref="IStoredProcedure{TReturn,TParameters}"/>
    /// </remarks>
    public interface ISqlFunction<in TReturn, out TParameters>
        : ISqlFunction
        
        where TParameters : class
    {
        /// <summary>
        /// Gets a value indicating whether this instance has null sql function parameters.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has null sql function parameters; otherwise, <c>false</c>.
        /// </value>
        bool HasNullSqlFunctionParameters { get; }

        /// <summary>
        /// Ensurefullies the construcuted.
        /// </summary>
        /// <exception cref="System.Exception">
        /// this instance is not fully constrcuted
        /// </exception>
        void EnsureIsFullyConstructed();

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        TParameters Parameters { get; }
    }
}