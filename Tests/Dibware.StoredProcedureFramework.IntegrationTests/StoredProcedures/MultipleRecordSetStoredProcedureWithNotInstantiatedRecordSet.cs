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
    [Name("MultipleRecordSetStoredProcedure")]
    internal class MultipleRecordSetStoredProcedureWithNotInstantiatedRecordSet
        : StoredProcedureBase<
            MultipleRecordSetStoredProcedureWithNotInstantiatedRecordSet.ResultSet,
            MultipleRecordSetStoredProcedureWithNotInstantiatedRecordSet.Parameter>
    {
        public MultipleRecordSetStoredProcedureWithNotInstantiatedRecordSet(Parameter parameters)
            : base(parameters)
        {
        }

        internal class Parameter
        {
            public int Id { get; set; }

            [Size(20)]
            public string Name { get; set; }

            public bool Active { get; set; }

            [DbType(SqlDbType.Decimal)]
            [Precision(10)]
            [Scale(4)]
            public decimal Price { get; set; }

            [DbType(SqlDbType.UniqueIdentifier)]
            public Guid UniqueIdentifier { get; set; }

            [DbType(SqlDbType.TinyInt)]
            public byte Count { get; set; }
        }

        internal class ResultSet
        {
            public List<ReturnType1> RecordSet1 { get; private set; }
            public List<ReturnType2> RecordSet2 { get; private set; }
            public List<ReturnType3> RecordSet3 { get; private set; }

            public ResultSet()
            {
                RecordSet1 = new List<ReturnType1>();
                //RecordSet2 = new List<ReturnType2>(); // deliberately NOT instantiated
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
            [DbType(SqlDbType.Bit)]
            public bool Active { get; set; }

            [DbType(SqlDbType.Decimal)]
            public decimal Price { get; set; }
        }

        internal class ReturnType3
        {
            [DbType(SqlDbType.UniqueIdentifier)]
            public Guid UniqueIdentifier { get; set; }

            [DbType(SqlDbType.TinyInt)]
            public byte Count { get; set; }
        }
    }
}
