using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    internal class TenantGetAllNoAttributes
            : NoParametersStoredProcedureBase<TenantGetAllNoAttributesResultSet>
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

    internal class TenantGetAllNoAttributesResultSet
    {
        public List<TenantResultRow> RecordSet1 { get; set; }

        public TenantGetAllNoAttributesResultSet()
        {
            RecordSet1 = new List<TenantResultRow>();
        }
    }

}