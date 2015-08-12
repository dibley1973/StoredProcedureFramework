

using Dibware.StoredProcedureFramework.Tests.Contracts;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Tenant_GetAll")]
    public class GAT : IStoredProcedure<TenantResultRow, NullParameter>
    {
        #region IStoredProcedure<TenantResultRow,NullParameter> Members

        public TenantResultRow ReturnType
        {
            get { throw new System.NotImplementedException(); }
        }

        public NullParameter ParameterType
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}
