using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.UnitTests.UserDefinedTypes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
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