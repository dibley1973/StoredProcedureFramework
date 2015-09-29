using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    /// <summary>
    /// Represents a "normal" stored procedure which has parameters and returns
    /// a single result set
    /// </summary>
    internal class NormalStoredProcedure
        : StoredProcedureBase<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedure(NormalStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class NormalStoredProcedureResultSet
    {
        public List<NormalStoredProcedureRecordSet1ReturnType> RecordSet1 { get; set; }

        public NormalStoredProcedureResultSet()
        {
            RecordSet1 = new List<NormalStoredProcedureRecordSet1ReturnType>();
        }
    }

    internal class NormalStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
    }

    internal class NormalStoredProcedureRecordSet1ReturnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
