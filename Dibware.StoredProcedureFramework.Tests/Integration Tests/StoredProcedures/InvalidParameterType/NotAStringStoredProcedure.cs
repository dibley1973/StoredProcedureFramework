using System.Collections.Generic;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.InvalidParameterType
{

    [Schema("app")]
    [Name("DecimalTest")]
    internal class IncorrectStringParameterStoredProcedure
        : StoredProcedureBase<IncorrectStringParameterStoredProcedureResultSet, IncorrectStringParameterStoredProcedureParameters>
    {
        public IncorrectStringParameterStoredProcedure()
            : base(new IncorrectStringParameterStoredProcedureParameters())
        {
        }
    }

    internal class IncorrectStringParameterStoredProcedureParameters
    {

    }

    internal class IncorrectStringParameterStoredProcedureResultSet
    {
        public List<NullStoredProcedureResult> RecordSet1 { get; set; }

        public IncorrectStringParameterStoredProcedureResultSet()
        {
            RecordSet1 = new List<NullStoredProcedureResult>();
        }
    }
}
