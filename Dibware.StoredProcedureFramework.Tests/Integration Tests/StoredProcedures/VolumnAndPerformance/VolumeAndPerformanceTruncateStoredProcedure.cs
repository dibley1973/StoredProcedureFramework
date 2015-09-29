using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.VolumnAndPerformance
{
    [Name("VolumeAndPerformance_Truncate")]
    internal class VolumeAndPerformanceTruncateStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
    }

    [Name("VolumeAndPerformance_InsertNRecords")]
    internal class VolumeAndPerformanceInsertNRecordsStoredProcedure
        : NoReturnTypeStoredProcedureBase<VolumeAndPerformanceInsertNRecordsStoredProcedureParameters>
    {
        public VolumeAndPerformanceInsertNRecordsStoredProcedure(
            VolumeAndPerformanceInsertNRecordsStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int NumberOfRecords { get; set; }
    }


    [Name("VolumeAndPerformance_GetAll")]
    internal class VolumeAndPerformanceGetAllStoredProcedure
        : NoParametersStoredProcedureBase<VolumeAndPerformanceGetAllStoredProcedureResultSet>
    {
    }

    internal class VolumeAndPerformanceGetAllStoredProcedureResultSet
    {
        public List<VolumeAndPerformanceGetAllStoredProcedureReturnType> RecordSet1 { get; set; }

        public VolumeAndPerformanceGetAllStoredProcedureResultSet()
        {
            RecordSet1 = new List<VolumeAndPerformanceGetAllStoredProcedureReturnType>();
        }
    }

    internal class VolumeAndPerformanceGetAllStoredProcedureReturnType
    {
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid Id { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string FirstName { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string LastName { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Address1 { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Address2 { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string City { get; set; }

        [Size(50)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string County { get; set; }

        [ParameterDbType(SqlDbType.SmallDateTime)]
        public DateTime DateOfBirth { get; set; }
    }
}
