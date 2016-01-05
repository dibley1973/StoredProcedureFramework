using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

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
            [DbType(SqlDbType.Structured)]
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