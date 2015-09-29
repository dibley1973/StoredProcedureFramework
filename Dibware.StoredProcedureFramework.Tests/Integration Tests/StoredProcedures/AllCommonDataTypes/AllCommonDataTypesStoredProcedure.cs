using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    [Schema("app")]
    [Name("AllCommonDataTypes")]
    internal class AllCommonDataTypesStoredProcedure
        : StoredProcedureBase<AllCommonDataTypesResultSet, AllCommonDataTypesParameters>
    {
        public AllCommonDataTypesStoredProcedure(AllCommonDataTypesParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class AllCommonDataTypesResultSet
    {
        public List<AllCommonDataTypesReturnType> RecordSet1 { get; set; }

        public AllCommonDataTypesResultSet()
        {
            RecordSet1 = new List<AllCommonDataTypesReturnType>();
        }
    }
}
