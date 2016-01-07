namespace Dibware.StoredProcedureFramework.Contracts
{
    /// <summary>
    /// Defines the expected members for a stored procedure
    /// </summary>
    public interface IStoredProcedure
    {
        ///// <summary>
        ///// Gets the name of the procedure.
        ///// </summary>
        ///// <value>
        ///// The name of the procedure.
        ///// </value>
        //string ProcedureName { get; }

        ///// <summary>
        ///// Gets the name of the schema.
        ///// </summary>
        ///// <value>
        ///// The name of the schema.
        ///// </value>
        //string SchemaName { get; }

        /// <summary>
        /// Gets the combined schema and procedure name.
        /// </summary>
        /// <returns></returns>
        string GetTwoPartName();

        ///// <summary>
        ///// Sets the procedure name.
        ///// </summary>
        ///// <param name="procedureName">Name of the procedure.</param>
        ///// <returns>
        ///// This instance
        ///// </returns>
        //void SetProcedureName(string procedureName);

        ///// <summary>
        ///// Sets the schema name.
        ///// </summary>
        ///// <param name="schemaName">The name of the schema.</param>
        ///// <returns></returns>
        //void SetSchemaName(string schemaName);
    }

    /// <summary>
    /// Defines the expected members and type parameters of an object that
    /// represents a Stored procedure
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameter.</typeparam>
    public interface IStoredProcedure<in TReturn, out TParameters>
        : IStoredProcedure
        where TReturn : class
        where TParameters : class
    {
        /// <summary>
        /// Gets a value indicating whether this instance has null stored procedure parameters.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has null stored procedure parameters; otherwise, <c>false</c>.
        /// </value>
        bool HasNullStoredProcedureParameters { get; }

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