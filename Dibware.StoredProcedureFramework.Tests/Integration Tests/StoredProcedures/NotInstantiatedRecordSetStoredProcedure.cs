using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures
{
    internal class NotInstantiatedRecordSetStoredProcedure
        : StoredProcedureBase<
            NotInstantiatedRecordSetStoredProcedureResultSet,
            NullStoredProcedureParameters>
    {
        public NotInstantiatedRecordSetStoredProcedure(NullStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class NotInstantiatedRecordSetStoredProcedureResultSet
    {
        public List<NotInstantiatedRecordSetStoredProcedureReturnType> RecordSet1 { get; set; }
    }

    internal class NotInstantiatedRecordSetStoredProcedureReturnType
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
