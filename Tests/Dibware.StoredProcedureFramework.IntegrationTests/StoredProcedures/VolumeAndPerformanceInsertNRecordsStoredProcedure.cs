using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class VolumeAndPerformanceInsertNRecordsStoredProcedure
        : NoReturnTypeStoredProcedureBase<VolumeAndPerformanceInsertNRecordsStoredProcedure.Parameter>
    {
        public VolumeAndPerformanceInsertNRecordsStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            //[ParameterDbType(SqlDbType.Int)]
            public int NumberOfRecords { get; set; }
        }
    }
}
