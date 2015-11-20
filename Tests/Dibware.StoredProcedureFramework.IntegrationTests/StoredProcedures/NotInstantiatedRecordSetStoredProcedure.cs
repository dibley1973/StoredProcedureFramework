using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class NotInstantiatedRecordSetStoredProcedure
        : StoredProcedureBase<
            List<NotInstantiatedRecordSetStoredProcedure.Return>,
            NullStoredProcedureParameters>
    {
        public NotInstantiatedRecordSetStoredProcedure(NullStoredProcedureParameters parameters)
            : base(parameters)
        { }

        internal class Return
        {
            //[ParameterDbType(SqlDbType.Int)]
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
