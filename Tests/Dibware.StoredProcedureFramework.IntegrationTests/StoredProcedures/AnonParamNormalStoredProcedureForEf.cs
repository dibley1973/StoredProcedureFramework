using System.Collections.Generic;
using System.Data.Entity;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFrameworkForEF.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("NormalStoredProcedure")]
    internal class AnonParamNormalStoredProcedureForEf
       : StoredProcedureBaseForEf<List<AnonParamNormalStoredProcedureForEf.Return>, object>
    {
        public AnonParamNormalStoredProcedureForEf(DbContext context)
            : base(context, null)
        { }

        internal class Return
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }
}