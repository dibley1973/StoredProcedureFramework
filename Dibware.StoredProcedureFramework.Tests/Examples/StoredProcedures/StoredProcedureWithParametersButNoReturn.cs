using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    // Verbose way of defining procedure
    //internal class StoredProcedureWithParametersButNoReturn
    //: StoredProcedureBase<NullStoredProcedureResult, StoredProcedureWithParametersButNoReturnParameters>
    //{
    //    public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

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
