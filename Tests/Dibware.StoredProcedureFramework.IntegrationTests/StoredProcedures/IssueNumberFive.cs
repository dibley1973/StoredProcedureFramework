using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("IssueNumberFive")]
    internal class IssueNumberFiveWithCorrectDataType
        : NoParametersStoredProcedureBase<
            List<IssueNumberFiveWithCorrectDataType.Return>>
    {
        internal class Return
        {
            public int Value1 { get; set; }
            public bool Value2 { get; set; }
            public string Value3 { get; set; }
        }
    }

    [Name("IssueNumberFive")]
    internal class IssueNumberFiveWithIncorrectDataType
        : NoParametersStoredProcedureBase<
            List<IssueNumberFiveWithIncorrectDataType.Return>>
    {

        internal class Return
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
            public string Value3 { get; set; }
        }
    }
}