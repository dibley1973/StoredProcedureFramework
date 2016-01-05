using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.UnitTests.UserDefinedTypes;

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
            [DbType(SqlDbType.Structured)]
            public List<SimpleParameterTableType> TvpParameters { get; set; }
        }
    }
}