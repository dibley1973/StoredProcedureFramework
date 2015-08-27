namespace Dibware.StoredProcedureFramework.Contracts
{
    /// <summary>
    /// Defines the expected members and type parameters of an object that
    /// represents a Stored procedure
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameter.</typeparam>
    public interface IStoredProcedure<in TReturn, out TParameters>
        where TReturn : class
        where TParameters : class
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        TParameters Parameters { get; }

        ///// <summary>
        ///// Gets the procedureName of the stored procedure
        ///// </summary>
        //string ProcedureName { get; }

        ///// <summary>
        ///// Gets (or privately sets) the schemaName this objects resides
        ///// </summary>
        //string SchemaName { get; }

        /// <summary>
        /// Ensurefullies the construcuted.
        /// </summary>
        /// <exception cref="System.Exception">
        /// this instance is not fully constrcuted
        /// </exception>
        void EnsureFullyConstructed();

        /// <summary>
        /// Gets the combined schema and procedure name.
        /// </summary>
        /// <returns></returns>
        string GetTwoPartName();
    }
}