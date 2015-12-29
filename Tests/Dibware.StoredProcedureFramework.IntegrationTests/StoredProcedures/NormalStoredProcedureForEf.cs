using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Collections.Generic;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class NormalStoredProcedureForEf
        : StoredProcedureBaseForEf<List<NormalStoredProcedureForEf.Return>, NormalStoredProcedureForEf.Parameter>
    {
        public NormalStoredProcedureForEf(DbContext context)
            : base(context, null)
        {
        }

        internal class Parameter
        {
            public int Id { get; set; }
        }

        internal class Return
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }
}