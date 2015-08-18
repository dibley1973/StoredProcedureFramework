using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    public class TenantGetAllNoAttributes
            : StoredProcedureBase<TenantResultRow, NullStoredProcedureParameters>
    {
        #region Constructors

        public TenantGetAllNoAttributes(NullStoredProcedureParameters parameters)
            : base(parameters)
        {
        }

        public TenantGetAllNoAttributes(string procedureName, NullStoredProcedureParameters parameters)
            : base(procedureName, parameters)
        {
        }

        public TenantGetAllNoAttributes(string schemaName, string procedureName, NullStoredProcedureParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
        }

        #endregion
    }
}