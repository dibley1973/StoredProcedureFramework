using System;
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    internal class AllCommonDataTypesStoredProcedureWithoutParameterAttributes
        : StoredProcedureBase<
            List<AllCommonDataTypesStoredProcedureWithParameterAttributes.Return>,
            AllCommonDataTypesStoredProcedureWithoutParameterAttributes.Parameter>
    {
        public AllCommonDataTypesStoredProcedureWithoutParameterAttributes(Parameter parameters)
            : base(parameters)
        {
        }

        internal class Parameter
        {
            public Int64 BigInt { get; set; }
            public Byte[] Binary { get; set; }
            public Boolean Bit { get; set; }
            public Char[] Char { get; set; }
            public DateTime Date { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime DateTime2 { get; set; }

            [Precision(18)]
            [Scale(2)]
            public Decimal Decimal { get; set; }

            public Double Float { get; set; }
            public Byte[] Image { get; set; }
            public Int32 Int { get; set; }

            [Precision(19)]
            [Scale(4)]
            public Decimal Money { get; set; }

            [Size(5)]
            public String NChar { get; set; }

            [Size(5)]
            public String NText { get; set; }

            [Precision(18)]
            [Scale(2)]
            public Decimal Numeric { get; set; }

            [Size(8)]
            public String NVarchar { get; set; }

            public Single Real { get; set; }
            public DateTime SmallDateTime { get; set; }
            public Int16 Smallint { get; set; }

            [Precision(10)]
            [Scale(4)]
            public Decimal Smallmoney { get; set; }

            [Size(19)]
            public String Text { get; set; }

            public TimeSpan Time { get; set; }
            public Byte[] Timestamp { get; set; }
            public Byte TinyInt { get; set; }
            public Guid UniqueIdentifier { get; set; }

            [Size(3)]
            public Byte[] VarBinary { get; set; }

            [Size(7)]
            public String VarChar { get; set; }

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
