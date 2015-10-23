using System;
using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.MultipleRecordSets
{
    internal class MultipleRecordSetStoredProcedure
        : StoredProcedureBase<MultipleRecordSetStoredProcedureResultSet, MultipleRecordSetStoredProcedureParameters>
    {
        public MultipleRecordSetStoredProcedure(MultipleRecordSetStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class MultipleRecordSetStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
        [Size(20)]
        public string Name { get; set; }
        [ParameterDbType(SqlDbType.Bit)]
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

    internal class MultipleRecordSetStoredProcedureResultSet
    {
        public List<MultipleRecordSetStoredProcedureReturnType1> RecordSet1 { get; set; }
        public List<MultipleRecordSetStoredProcedureReturnType2> RecordSet2 { get; set; }
        public List<MultipleRecordSetStoredProcedureReturnType3> RecordSet3 { get; set; }

        public MultipleRecordSetStoredProcedureResultSet()
        {
            RecordSet1 = new List<MultipleRecordSetStoredProcedureReturnType1>();
            RecordSet2 = new List<MultipleRecordSetStoredProcedureReturnType2>();
            RecordSet3 = new List<MultipleRecordSetStoredProcedureReturnType3>();
        }
    }

    internal class MultipleRecordSetStoredProcedureReturnType1
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    internal class MultipleRecordSetStoredProcedureReturnType2
    {
        [ParameterDbType(SqlDbType.Bit)]
        public bool Active { get; set; }

        [ParameterDbType(SqlDbType.Decimal)]
        public decimal Price { get; set; }
    }

    internal class MultipleRecordSetStoredProcedureReturnType3
    {
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid UniqueIdentifier { get; set; }

        [ParameterDbType(SqlDbType.TinyInt)]
        public byte Count { get; set; }
    }
}
