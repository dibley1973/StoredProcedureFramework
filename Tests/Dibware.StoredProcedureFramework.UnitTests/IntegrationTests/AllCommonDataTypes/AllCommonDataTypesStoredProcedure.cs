using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.AllCommonDataTypes
{
    //[Schema("app")]
    //[Name("AllCommonDataTypes")]
    //internal class AllCommonDataTypesStoredProcedure
    //    : StoredProcedureBase<AllCommonDataTypesResultSet, AllCommonDataTypesParameters>
    //{
    //    public AllCommonDataTypesStoredProcedure(AllCommonDataTypesParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

    [Schema("app")]
    [Name("AllCommonDataTypes")]
    internal class AllCommonDataTypesStoredProcedure
        : StoredProcedureBase<List<AllCommonDataTypesReturnType>, AllCommonDataTypesParameters>
    {
        public AllCommonDataTypesStoredProcedure(AllCommonDataTypesParameters parameters)
            : base(parameters)
        {
        }
    }

    //internal class AllCommonDataTypesResultSet
    //{
    //    public List<AllCommonDataTypesReturnType> RecordSet1 { get; set; }

    //    public AllCommonDataTypesResultSet()
    //    {
    //        RecordSet1 = new List<AllCommonDataTypesReturnType>();
    //    }
    //}
}
