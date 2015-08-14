using System;
using Dibware.StoredProcedureFramework.Contracts;
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

        #region IStoredProcedure<TenantResultRow,NullParameter> Members


        public string ProcedureName
        {
            get { throw new NotImplementedException(); }
        }

        public string SchemaName
        {
            get { throw new NotImplementedException(); }
        }

        public string GetTwoPartName()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public abstract class StoredProcedureBase<TReturn, TParameter>
        : IStoredProcedure<TReturn, TParameter>
        where TReturn : class
        where TParameter : class
    {
        #region Members

        public Type ReturnType
        {
            get { return typeof (TReturn); }
        }

        public Type ParameterType
        {
            get { return typeof (TParameter);  }
        }

        #endregion

        #region IStoredProcedure<TReturn,TParameter> Members

        TReturn IStoredProcedure<TReturn, TParameter>.ReturnType
        {
            get { throw new NotImplementedException(); }
        }

        TParameter IStoredProcedure<TReturn, TParameter>.ParameterType
        {
            get { throw new NotImplementedException(); }
        }

        public string ProcedureName
        {
            get { throw new NotImplementedException(); }
        }

        public string SchemaName
        {
            get { throw new NotImplementedException(); }
        }

        public string GetTwoPartName()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class GAT2 : StoredProcedureBase<TenantResultRow, NullParameter>
    {

    }
}
