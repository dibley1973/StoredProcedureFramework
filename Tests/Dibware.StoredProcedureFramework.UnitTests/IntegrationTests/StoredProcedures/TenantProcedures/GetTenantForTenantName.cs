﻿using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TenantProcedures
{
    [Schema("app")]
    [Name("Tenant_GetForTenantName")]
    [ReturnType(typeof(TenantResultRow))]
    internal class GetTenantForTenantName
    {
        [Name("TenantName")]
        [ParameterDbType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }

    //internal class GetTenantForTenantNameProcedureResultSet
    //{
    //    public List<TenantResultRow> RecordSet1 { get; set; }

    //    public GetTenantForTenantNameProcedureResultSet()
    //    {
    //        RecordSet1 = new List<TenantResultRow>();
    //    }
    //}

    //[Schema("app")]
    //[Name("Tenant_GetForTenantName")]
    //internal class GetTenantForTenantNameProcedure
    //    : StoredProcedureBase<GetTenantForTenantNameProcedureResultSet, GetTenantForTenantNameParameters>
    //{
    //    public GetTenantForTenantNameProcedure(
    //        GetTenantForTenantNameParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

    [Schema("app")]
    [Name("Tenant_GetForTenantName")]
    internal class GetTenantForTenantNameProcedure
        : StoredProcedureBase<List<TenantResultRow>, GetTenantForTenantNameParameters>
    {
        public GetTenantForTenantNameProcedure(
            GetTenantForTenantNameParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class GetTenantForTenantNameParameters
    {
        [Name("TenantName")]
        [Size(100)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }
}