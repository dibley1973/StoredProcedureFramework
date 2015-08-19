namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("AllCommonDataTypes")]
    internal class AllCommonDataTypesStoredProcedure
        : StoredProcedureBase<AllCommonDataTypesReturnType, AllCommonDataTypesParameters>
    {
        public AllCommonDataTypesStoredProcedure(AllCommonDataTypesParameters parameters)
            : base(parameters)
        {
        }
    }
}
