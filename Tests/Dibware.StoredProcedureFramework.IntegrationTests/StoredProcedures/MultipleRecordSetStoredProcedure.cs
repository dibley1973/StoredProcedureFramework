using System;
using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    /// <summary>
    /// Represents a stored procedure with multiple recordsets
    /// </summary>
    internal class MultipleRecordSetStoredProcedure
        : StoredProcedureBase<
            MultipleRecordSetStoredProcedure.ResultSet, 
            MultipleRecordSetStoredProcedure.Parameter>
    {
        public MultipleRecordSetStoredProcedure(Parameter parameters)
            : base(parameters)
        {
        }

        internal class Parameter
        {
            //[ParameterDbType(SqlDbType.Int)]
            public int Id { get; set; }

            [Size(20)]
            public string Name { get; set; }

            //[ParameterDbType(SqlDbType.Bit)]
            public bool Active { get; set; }

            [ParameterDbType(SqlDbType.Decimal)]
            [Precision(10)]
            [Scale(4)]
            public decimal Price { get; set; }

            [ParameterDbType(SqlDbType.UniqueIdentifier)]
            public Guid UniqueIdentifier { get; set; }

            [ParameterDbType(SqlDbType.TinyInt)]
            public byte Count { get; set; }
        }

        internal class ResultSet
        {
            public List<ReturnType1> RecordSet1 { get; set; }
            public List<ReturnType2> RecordSet2 { get; set; }
            public List<ReturnType3> RecordSet3 { get; set; }

            public ResultSet()
            {
                RecordSet1 = new List<ReturnType1>();
                RecordSet2 = new List<ReturnType2>();
                RecordSet3 = new List<ReturnType3>();
            }
        }

        internal class ReturnType1
        {
            //[ParameterDbType(SqlDbType.Int)]
            public int Id { get; set; }

            public string Name { get; set; }
        }

        internal class ReturnType2
        {
            [ParameterDbType(SqlDbType.Bit)]
            public bool Active { get; set; }

            [ParameterDbType(SqlDbType.Decimal)]
            public decimal Price { get; set; }
        }

        internal class ReturnType3
        {
            [ParameterDbType(SqlDbType.UniqueIdentifier)]
            public Guid UniqueIdentifier { get; set; }

            [ParameterDbType(SqlDbType.TinyInt)]
            public byte Count { get; set; }
        }
    }
}
