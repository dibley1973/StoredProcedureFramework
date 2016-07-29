using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;
using System.Dynamic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class MultipleRecordSetDynamicColumnStoredProcedure
        : NoParametersStoredProcedureBase<
            MultipleRecordSetDynamicColumnStoredProcedure.ResultSet>
    {
        internal class ResultSet
        {
            public List<ExpandoObject> RecordSet1 { get; private set; }
            public List<ExpandoObject> RecordSet2 { get; private set; }
            public List<ExpandoObject> RecordSet3 { get; private set; }

            public ResultSet()
            {
                RecordSet1 = new List<ExpandoObject>();
                RecordSet2 = new List<ExpandoObject>();
                RecordSet3 = new List<ExpandoObject>();
            }
        }
    }
}