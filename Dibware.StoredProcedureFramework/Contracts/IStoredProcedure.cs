
namespace Dibware.StoredProcedureFramework.Contracts
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    public interface IStoredProcedure2<in TReturn, in TParameter>
        where TReturn : class
        where TParameter : class
    {
        //TReturn ReturnType { get; }

        //TParameter Parameters { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameter.</typeparam>
    public interface IStoredProcedure<out TReturn, out TParameters>
        where TReturn : class
        where TParameters : class
    {
        //TReturn ReturnType { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        TParameters Parameters { get; }

        /// <summary>
        /// Gets the procedureName of the stored procedure
        /// </summary>
        string ProcedureName { get; }

        /// <summary>
        /// Gets (or privately sets) the schemaName this objects resides
        /// </summary>
        string SchemaName { get; }

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

    public interface IReturnType
    { }

    public interface IParameterType
    { }
}