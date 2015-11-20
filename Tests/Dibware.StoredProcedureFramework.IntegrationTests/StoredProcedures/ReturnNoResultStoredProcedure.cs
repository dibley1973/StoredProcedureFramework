using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class ReturnNoResultStoredProcedure
        : StoredProcedureBase<
            NullStoredProcedureResult,
            NullStoredProcedureParameters>
    {
        public ReturnNoResultStoredProcedure(NullStoredProcedureParameters parameters)
            : base(parameters)
        { }
    }
}