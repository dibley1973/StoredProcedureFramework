
namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class StoredProcedureWithParametersButNoReturn2
        : StoredProcedureBase<NullStoredProcedureResult, StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn2(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

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
