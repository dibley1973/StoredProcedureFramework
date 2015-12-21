using System;
using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class AllCommonDataTypesStoredProcedure
        : StoredProcedureBase<
            List<AllCommonDataTypesStoredProcedure.Return>,
            AllCommonDataTypesStoredProcedure.Parameter>
    {
        public AllCommonDataTypesStoredProcedure(Parameter parameters)
            : base(parameters)
        {
        }

        internal class Parameter
        {
            [DbType(SqlDbType.BigInt)]
            public Int64 BigInt { get; set; }

            [Size(8)]
            [DbType(SqlDbType.Binary)]
            public Byte[] Binary { get; set; }

            [DbType(SqlDbType.Bit)]
            public Boolean Bit { get; set; }

            [Size(3)]
            public Char[] Char { get; set; }

            [DbType(SqlDbType.Date)]
            public DateTime Date { get; set; }

            [DbType(SqlDbType.DateTime)]
            public DateTime DateTime { get; set; }

            [DbType(SqlDbType.DateTime2)]
            public DateTime DateTime2 { get; set; }

            [Precision(18)]
            [Scale(2)]
            [DbType(SqlDbType.Decimal)]
            public Decimal Decimal { get; set; }

            [DbType(SqlDbType.Float)]
            public Double Float { get; set; }

            [DbType(SqlDbType.Image)]
            public Byte[] Image { get; set; }

            [DbType(SqlDbType.Int)]
            public Int32 Int { get; set; }

            [Precision(19)]
            [Scale(4)]
            [DbType(SqlDbType.Money)]
            public Decimal Money { get; set; }

            [Size(5)]
            [DbType(SqlDbType.NChar)]
            public String NChar { get; set; }

            [Size(5)]
            [DbType(SqlDbType.NText)]
            public String NText { get; set; }

            [Precision(18)]
            [Scale(2)]
            [DbType(SqlDbType.Decimal)]
            public Decimal Numeric { get; set; }

            [Size(8)]
            [DbType(SqlDbType.NVarChar)]
            public String NVarchar { get; set; }

            [DbType(SqlDbType.Real)]
            public Single Real { get; set; }

            [DbType(SqlDbType.SmallDateTime)]
            public DateTime SmallDateTime { get; set; }

            [DbType(SqlDbType.SmallInt)]
            public Int16 Smallint { get; set; }

            [Precision(10)]
            [Scale(4)]
            [DbType(SqlDbType.SmallMoney)]
            public Decimal Smallmoney { get; set; }

            [Size(19)]
            [DbType(SqlDbType.Text)]
            public String Text { get; set; }

            [DbType(SqlDbType.Time)]
            public TimeSpan Time { get; set; }

            [DbType(SqlDbType.Timestamp)]
            public Byte[] Timestamp { get; set; }

            [DbType(SqlDbType.TinyInt)]
            public Byte TinyInt { get; set; }

            [DbType(SqlDbType.UniqueIdentifier)]
            public Guid UniqueIdentifier { get; set; }

            [Size(3)]
            [DbType(SqlDbType.VarBinary)]
            public Byte[] VarBinary { get; set; }

            [Size(7)]
            public String VarChar { get; set; }

            [DbType(SqlDbType.Xml)]
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
