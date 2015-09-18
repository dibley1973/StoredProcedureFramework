
namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class StoredProcedureWithParametersButNoReturn
        : NoReturnTypeStoredProcedureBase<StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }


    internal class StoredProcedureWithParametersButNoReturnParameters
    {
        int Id { get; set; }
    }
}
