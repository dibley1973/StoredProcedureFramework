
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests
{
    [Schema("app")]
    [Name("DecimalTest")]
    internal class DecimalWrongReturnTestStoredProcedure
        : StoredProcedureBase<DecimalWrongReturnTestStoredProcedureResultSet, NullStoredProcedureParameters>
    {
        public DecimalWrongReturnTestStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

    internal class DecimalWrongReturnTestStoredProcedureResultSet
    {
        public List<DecimalWrongReturnTestReturnType> RecordSet1 { get; set; }

        public DecimalWrongReturnTestStoredProcedureResultSet()
        {
            RecordSet1 = new List<DecimalWrongReturnTestReturnType>();
        }
    }
}
