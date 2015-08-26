using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    [Schema("app")]
    [Name("AllCommonDataTypes")]
    internal class AllCommonDataTypesStoredProcedure
        : StoredProcedureBase<AllCommonDataTypesReturnType, AllCommonDataTypesParameters>
    {
        public AllCommonDataTypesStoredProcedure(AllCommonDataTypesParameters parameters)
            : base(parameters)
        {
        }
    }
}
