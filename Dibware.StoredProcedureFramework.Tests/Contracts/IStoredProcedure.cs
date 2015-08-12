namespace Dibware.StoredProcedureFramework.Tests.Contracts
{
    public interface IStoredProcedure<out TReturn, out TParameter>
    {
        TReturn ReturnType { get; }

        TParameter ParameterType { get; }
    }
}