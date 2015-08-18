using System;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes
{
    internal class AllCommonDataTypesParameters
    {
        public Int64 Bigint { get; set; }
        public Byte[] Binary { get; set; }
        public Boolean Bit { get; set; }
        public Char[] Char { get; set; }
        public DateTime Date { get; set; }
        public DateTime Datetime { get; set; }
        public DateTime Datetime2 { get; set; }
        public Decimal Decimal { get; set; }
        public Double Float { get; set; }
        public Byte[] Image { get; set; }
        public Int32 Int { get; set; }
        public Decimal Money { get; set; }
        public String Nchar { get; set; }
        public String Ntext { get; set; }
        public Decimal Numeric { get; set; }
        public String Nvarchar { get; set; }
        public Single Real { get; set; }
        public DateTime Smalldatetime { get; set; }
        public Int16 Smallint { get; set; }
        public Decimal Smallmoney { get; set; }
        public String Text { get; set; }
        public TimeSpan Time { get; set; }
        public Byte[] Timestamp { get; set; }
        public Byte Tinyint { get; set; }
        public Guid Uniqueidentifier { get; set; }
        public Byte[] Varbinary { get; set; }
        public String Varchar { get; set; }
        public String Xml { get; set; }
    }
}