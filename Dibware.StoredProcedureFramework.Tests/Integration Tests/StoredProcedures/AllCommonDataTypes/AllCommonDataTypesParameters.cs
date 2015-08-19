using System;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    internal class AllCommonDataTypesParameters
    {
        public Int64 BigInt { get; set; }

        [StoredProcAttributes.Size(8)]
        public Byte[] Binary { get; set; }

        public Boolean Bit { get; set; }

        [StoredProcAttributes.Size(3)]
        public Char[] Char { get; set; }

        public DateTime Date { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DateTime2 { get; set; }
        public Decimal Decimal { get; set; }
        public Double Float { get; set; }
        public Byte[] Image { get; set; }
        public Int32 Int { get; set; }
        public Decimal Money { get; set; }

        [StoredProcAttributes.Size(5)]
        public String NChar { get; set; }

        public String NText { get; set; }
        public Decimal Numeric { get; set; }

        [StoredProcAttributes.Size(8)]
        public String NVarchar { get; set; }

        public Single Real { get; set; }
        public DateTime SmallDateTime { get; set; }
        public Int16 Smallint { get; set; }
        public Decimal Smallmoney { get; set; }
        public String Text { get; set; }

        public TimeSpan Time { get; set; }
        public Byte[] Timestamp { get; set; }
        public Byte TinyInt { get; set; }
        public Guid UniqueIdentifier { get; set; }

        [StoredProcAttributes.Size(3)]
        public Byte[] VarBinary { get; set; }

        [StoredProcAttributes.Size(7)]
        public String VarChar { get; set; }

        public String Xml { get; set; }
    }
}