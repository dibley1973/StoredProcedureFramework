using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

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
            [ParameterDbType(SqlDbType.Structured)]
            public List<SimpleParameterTableType> TvpParameters { get; set; }
        }
    }
}