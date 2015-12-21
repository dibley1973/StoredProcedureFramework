using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class VolumeAndPerformanceGetAllStoredProcedure
        : NoParametersStoredProcedureBase<List<VolumeAndPerformanceGetAllStoredProcedure.Return>>
    {
        internal class Return
        {
            [DbType(SqlDbType.UniqueIdentifier)]
            public Guid Id { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string FirstName { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string LastName { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Address1 { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Address2 { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string City { get; set; }

            [Size(50)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string County { get; set; }

            [DbType(SqlDbType.SmallDateTime)]
            public DateTime DateOfBirth { get; set; }
        }
    }
}
