using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    public class TenantGetAllNoAttributes
            : NoParametersStoredProcedureBase<TenantResultRow>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantGetAllNoAttributes"/> class.
        /// </summary>
        public TenantGetAllNoAttributes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantGetAllNoAttributes"/> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public TenantGetAllNoAttributes(string procedureName)
            : base(procedureName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantGetAllNoAttributes"/> class.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        public TenantGetAllNoAttributes(string schemaName, string procedureName)
            : base(schemaName, procedureName)
        {
        }

        #endregion
    }
}