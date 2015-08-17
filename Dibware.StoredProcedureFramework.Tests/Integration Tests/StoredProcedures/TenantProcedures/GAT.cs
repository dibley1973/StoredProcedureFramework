using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    //[StoredProcAttributes.SchemaAttribute("app")]
    //[StoredProcAttributes.Name("Tenant_GetAll")]
    //public class GAT : IStoredProcedure<TenantResultRow, NullStoredProcedureParameters>
    //{
    //    #region IStoredProcedure<TenantResultRow,NullStoredProcedureParameters> Members

    //    public TenantResultRow ReturnType
    //    {
    //        get { throw new System.NotImplementedException(); }
    //    }

    //    public NullStoredProcedureParameters ParametersesType
    //    {
    //        get { throw new System.NotImplementedException(); }
    //    }

    //    #endregion

    //    #region IStoredProcedure<TenantResultRow,NullStoredProcedureParameters> Members


    //    public string ProcedureName
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string SchemaName
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string GetTwoPartName()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}

    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Tenant_GetAll")]
    public class TenantGetAllWithAttributes : StoredProcedureBase<TenantResultRow, NullStoredProcedureParameters>
    {
        public TenantGetAllWithAttributes(NullStoredProcedureParameters parameters)
            : base(parameters)
        {

        }
    }


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
