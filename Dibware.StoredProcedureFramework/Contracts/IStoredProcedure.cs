
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

        //TParameter ParameterType { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    public interface IStoredProcedure<out TReturn, out TParameter>
        where TReturn : class
        where TParameter : class
    {
        TReturn ReturnType { get; }

        TParameter ParameterType { get; }

        /// <summary>
        /// Gets the procedureName of the stored procedure
        /// </summary>
        string ProcedureName { get; }

        /// <summary>
        /// Gets (or privately sets) the schemaName this objects resides
        /// </summary>
        string SchemaName { get; }

        /// <summary>
        /// Gets the name of the two part.
        /// </summary>
        /// <returns></returns>
        string GetTwoPartName();
    }

    public interface IReturnType
    { }

    public interface IParameterType
    { }
}