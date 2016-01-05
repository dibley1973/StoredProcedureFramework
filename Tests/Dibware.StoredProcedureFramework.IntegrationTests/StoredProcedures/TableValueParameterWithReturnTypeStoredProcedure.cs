using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    /// <summary>
    /// Represents a stored procedure with a table value parameter and return type
    /// </summary>
    internal class TableValueParameterWithReturnTypeStoredProcedure
        : StoredProcedureBase<List<TableValueParameterWithReturnTypeStoredProcedure.Return>,
        TableValueParameterWithReturnTypeStoredProcedure.Parameter>
    {
        public TableValueParameterWithReturnTypeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [DbType(SqlDbType.Structured)]
            public List<SimpleParameterTableType> TvpParameters { get; set; }
        }

        internal class Return
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
            public string Name { get; set; }
        }
    }
}