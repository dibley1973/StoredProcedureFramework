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
    }

    public interface IReturnType
    { }

    public interface IParameterType
    { }
}