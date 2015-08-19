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

        //public AllCommonDataTypesStoredProcedure(string procedureName, AllCommonDataTypesParameters parameters)
        //    : base(procedureName, parameters)
        //{
        //}

        //public AllCommonDataTypesStoredProcedure(string schemaName, string procedureName, AllCommonDataTypesParameters parameters)
        //    : base(schemaName, procedureName, parameters)
        //{
        //}
    }
}
