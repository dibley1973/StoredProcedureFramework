using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    internal class AllCommonDataTypesStoredProcedureWithParameterAttributes
        : StoredProcedureBase<
            List<AllCommonDataTypesStoredProcedureWithParameterAttributes.Return>,
            AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter>
    {
        public AllCommonDataTypesStoredProcedureWithParameterAttributes(Parameter parameters)
            : base(parameters)
        {
        }

        internal class Parameter
        {
            [ParameterDbType(SqlDbType.BigInt)]
            public Int64 BigInt { get; set; }

            [Size(8)]
            [ParameterDbType(SqlDbType.Binary)]
            public Byte[] Binary { get; set; }

            [ParameterDbType(SqlDbType.Bit)]
            public Boolean Bit { get; set; }

            [Size(3)]
            [ParameterDbType(SqlDbType.Char)]
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
            [ParameterDbType(SqlDbType.VarChar)]
            public String VarChar { get; set; }

            [ParameterDbType(SqlDbType.Xml)]
            public String Xml { get; set; }
        }

        internal class Return
        {
            public Int64 BigInt { get; set; }
            public Byte[] Binary { get; set; }
            public Boolean Bit { get; set; }
            public String Char { get; set; }
            public DateTime Date { get; set; }
            public DateTime Datetime { get; set; }
            public DateTime Datetime2 { get; set; }
            public Decimal Decimal { get; set; }
            public Double Float { get; set; }
            public Byte[] Image { get; set; }
            public Int32 Int { get; set; }
            public Decimal Money { get; set; }
            public String NChar { get; set; }
            public String NText { get; set; }
            public Decimal Numeric { get; set; }
            public String NVarchar { get; set; }
            public Single Real { get; set; }
            public DateTime SmallDateTime { get; set; }
            public Int16 SmallInt { get; set; }
            public Decimal Smallmoney { get; set; }
            public String Text { get; set; }
            public TimeSpan Time { get; set; }
            public Byte[] Timestamp { get; set; }
            public Byte TinyInt { get; set; }
            public Guid UniqueIdentifier { get; set; }
            public Byte[] Varbinary { get; set; }
            public String Varchar { get; set; }
            public String Xml { get; set; }
        }
    }
}
