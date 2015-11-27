using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    internal class TenantGetAllNoAttributes
            : NoParametersStoredProcedureBase<List<TenantResultRow>>
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