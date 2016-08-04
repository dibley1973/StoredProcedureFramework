using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedureContextTests.Context
{
    internal class IntegrationTestStoredProcedureContext
        : StoredProcedureContext
    {
        public IntegrationTestStoredProcedureContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        #region Stored Procedures

        public MostBasicStoredProcedureForContext MostBasicStoredProcedure { get; private set; }

        #endregion
    }

    internal class MostBasicStoredProcedureForContext : StoredProcedure
    {
        public MostBasicStoredProcedureForContext(StoredProcedureContext context) 
            : base(context)
        {
        }
    }
}