

using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures
{
    [Schema("app")]
    [Name("ReturnNoResult")]
    internal class ReturnNoResultStoredProcedure
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public ReturnNoResultStoredProcedure(NullStoredProcedureParameters parameters) : base(parameters)
        {
        }

        //public ReturnNoResultStoredProcedure(string procedureName, NullStoredProcedureParameters parameters) : base(procedureName, parameters)
        //{
        //}

        //public ReturnNoResultStoredProcedure(string schemaName, string procedureName, NullStoredProcedureParameters parameters) : base(schemaName, procedureName, parameters)
        //{
        //}
    }
}
