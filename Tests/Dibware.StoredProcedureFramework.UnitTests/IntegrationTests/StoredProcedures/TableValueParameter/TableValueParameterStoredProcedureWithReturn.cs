using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TableValueParameter
{
    internal class TableValueParameterStoredProcedureWithReturn
    : StoredProcedureBase<List<TableValueParameterStoredProcedureWithReturnReturnType>, TableValueParameterStoredProcedureWithReturnParameters>
    {
        public TableValueParameterStoredProcedureWithReturn(TableValueParameterStoredProcedureWithReturnParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class TableValueParameterStoredProcedureWithReturnReturnType
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }

    internal class TableValueParameterStoredProcedureWithReturnParameters
    {
        [ParameterDbType(SqlDbType.Structured)]
        public List<SimpleTableValueParameterTableType> TvpParameters { get; set; }
    }
}