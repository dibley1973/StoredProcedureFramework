
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.SizeAttributeProcedures
{
    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class CorrectSizeAttributeStoredProcedure
        : StoredProcedureBase<CorrectSizeAttributeReturnType, CorrectSizeAttributeParameters>
    {
        public CorrectSizeAttributeStoredProcedure(CorrectSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooSmallSizeAttributeStoredProcedure
        : StoredProcedureBase<TooSmallSizeAttributeReturnType, TooSmallSizeAttributeParameters>
    {
        public TooSmallSizeAttributeStoredProcedure(TooSmallSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooLargeSizeAttributeStoredProcedure
        : StoredProcedureBase<TooLargeSizeAttributeReturnType, TooLargeSizeAttributeParameters>
    {
        public TooLargeSizeAttributeStoredProcedure(TooLargeSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }
}
