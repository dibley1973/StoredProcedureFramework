using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    /// <summary>
    /// Represents a stored procedure with a table value parameter butt without a return type
    /// </summary>
    internal class TableValueParameterWithoutReturnTypeStoredProcedure
        : NoReturnTypeStoredProcedureBase<
            TableValueParameterWithoutReturnTypeStoredProcedure.Parameter>
    {
        public TableValueParameterWithoutReturnTypeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [DbType(SqlDbType.Structured)]
            public List<SimpleParameterTableType> TvpParameters { get; set; }
        }
    }
}