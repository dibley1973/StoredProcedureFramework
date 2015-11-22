using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

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

    //internal class StoredProcedureWithParametersButNoReturn
    //    : NoReturnTypeStoredProcedureBase<StoredProcedureWithParametersButNoReturnParameters>
    //{
    //    public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}


    //internal class StoredProcedureWithParametersButNoReturnParameters
    //{
    //    [ParameterDbType(SqlDbType.Int)]
    //    public int Id { get; set; }
    //}
}
