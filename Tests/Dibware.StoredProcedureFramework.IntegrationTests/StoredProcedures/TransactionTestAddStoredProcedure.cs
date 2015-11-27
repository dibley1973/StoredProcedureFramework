using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class TransactionTestAddStoredProcedure
        : StoredProcedureBase<List<TableValueParameterWithReturnTypeStoredProcedure.Return>,
            TransactionTestAddStoredProcedure.Parameter>
    {
        public TransactionTestAddStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [ParameterDbType(SqlDbType.Structured)]
            public List<TransactionTestParameterTableType> TvpParameters { get; set; }
        }

        internal class Return
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
            public string Name { get; set; }
        }
    }
}