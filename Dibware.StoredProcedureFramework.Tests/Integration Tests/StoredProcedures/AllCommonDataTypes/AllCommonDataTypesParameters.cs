using Dibware.StoredProcedureFramework.StoredProcAttributes;
using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    internal class AllCommonDataTypesParameters
    {

        [ParameterDbType(SqlDbType.BigInt)]
        public Int64 BigInt { get; set; }

        [Size(8)]
        [ParameterDbType(SqlDbType.Binary)]
        public Byte[] Binary { get; set; }

        [ParameterDbType(SqlDbType.Bit)]
        public Boolean Bit { get; set; }

        [Size(3)]
        public Char[] Char { get; set; }

        [ParameterDbType(SqlDbType.Date)]
        public DateTime Date { get; set; }

        [ParameterDbType(SqlDbType.DateTime)]
        public DateTime DateTime { get; set; }

        [ParameterDbType(SqlDbType.DateTime2)]
        public DateTime DateTime2 { get; set; }

        [Precision(18)]
        [Scale(2)]
        [ParameterDbType(SqlDbType.Decimal)]
        public Decimal Decimal { get; set; }

        [ParameterDbType(SqlDbType.Float)]
        public Double Float { get; set; }

        [ParameterDbType(SqlDbType.Image)]
        public Byte[] Image { get; set; }

        [ParameterDbType(SqlDbType.Int)]
        public Int32 Int { get; set; }

        [Precision(19)]
        [Scale(4)]
        [ParameterDbType(SqlDbType.Money)]
        public Decimal Money { get; set; }

        [Size(5)]
        [ParameterDbType(SqlDbType.NChar)]
        public String NChar { get; set; }

        [Size(5)]
        [ParameterDbType(SqlDbType.NText)]
        public String NText { get; set; }

        [Precision(18)]
        [Scale(2)]
        [ParameterDbType(SqlDbType.Decimal)]
        public Decimal Numeric { get; set; }

        [Size(8)]
        [ParameterDbType(SqlDbType.NVarChar)]
        public String NVarchar { get; set; }

        [ParameterDbType(SqlDbType.Real)]
        public Single Real { get; set; }

        [ParameterDbType(SqlDbType.SmallDateTime)]
        public DateTime SmallDateTime { get; set; }

        [ParameterDbType(SqlDbType.SmallInt)]
        public Int16 Smallint { get; set; }

        [Precision(10)]
        [Scale(4)]
        [ParameterDbType(SqlDbType.SmallMoney)]
        public Decimal Smallmoney { get; set; }

        [Size(19)]
        [ParameterDbType(SqlDbType.Text)]
        public String Text { get; set; }

        [ParameterDbType(SqlDbType.Time)]
        public TimeSpan Time { get; set; }

        [ParameterDbType(SqlDbType.Timestamp)]
        public Byte[] Timestamp { get; set; }

        [ParameterDbType(SqlDbType.TinyInt)]
        public Byte TinyInt { get; set; }

        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid UniqueIdentifier { get; set; }

        [Size(3)]
        [ParameterDbType(SqlDbType.VarBinary)]
        public Byte[] VarBinary { get; set; }

        [Size(7)]
        public String VarChar { get; set; }

        [ParameterDbType(SqlDbType.Xml)]
        public String Xml { get; set; }
    }
}