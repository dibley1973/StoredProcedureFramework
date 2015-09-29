
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.SizeAttributeProcedures
{
    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class CorrectSizeAttributeStoredProcedure
        : StoredProcedureBase<CorrectSizeAttributeStoredProcedureResultSet, CorrectSizeAttributeParameters>
    {
        public CorrectSizeAttributeStoredProcedure(CorrectSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class CorrectSizeAttributeStoredProcedureResultSet
    {
        public List<CorrectSizeAttributeReturnType> RecordSet1 { get; set; }

        public CorrectSizeAttributeStoredProcedureResultSet()
        {
            RecordSet1 = new List<CorrectSizeAttributeReturnType>();
        }
    }




    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooSmallSizeAttributeStoredProcedure
        : StoredProcedureBase<TooSmallSizeAttributeStoredProcedureResultSet, TooSmallSizeAttributeParameters>
    {
        public TooSmallSizeAttributeStoredProcedure(TooSmallSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class TooSmallSizeAttributeStoredProcedureResultSet
    {
        public List<TooSmallSizeAttributeReturnType> RecordSet1 { get; set; }

        public TooSmallSizeAttributeStoredProcedureResultSet()
        {
            RecordSet1 = new List<TooSmallSizeAttributeReturnType>();
        }
    }


    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooLargeSizeAttributeStoredProcedure
        : StoredProcedureBase<TooLargeSizeAttributeStoredProcedureResultSet, TooLargeSizeAttributeParameters>
    {
        public TooLargeSizeAttributeStoredProcedure(TooLargeSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class TooLargeSizeAttributeStoredProcedureResultSet
    {
        public List<TooLargeSizeAttributeReturnType> RecordSet1 { get; set; }
    }
}
