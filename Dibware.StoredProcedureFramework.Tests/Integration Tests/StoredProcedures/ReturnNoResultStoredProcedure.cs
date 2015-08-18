

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("ReturnNoResult")]
    public class ReturnNoResultStoredProcedure
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public ReturnNoResultStoredProcedure(NullStoredProcedureParameters parameters) : base(parameters)
        {
        }

        public ReturnNoResultStoredProcedure(string procedureName, NullStoredProcedureParameters parameters) : base(procedureName, parameters)
        {
        }

        public ReturnNoResultStoredProcedure(string schemaName, string procedureName, NullStoredProcedureParameters parameters) : base(schemaName, procedureName, parameters)
        {
        }
    }
}
