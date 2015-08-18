CREATE PROCEDURE [app].[AllCommonDataTypes]
    @BigInt             bigint
,   @Binary             binary
,   @Bit                bit
,   @Char               char
,   @Date               date
,   @DateTime           datetime
,   @DateTime2          datetime2
,   @Decimal            decimal
,   @float              float
,   @image              image
,   @Int                int
,   @Money              money
,   @NChar              nchar
,   @NText              ntext
,   @Numeric            numeric
,   @NVarchar           nvarchar
,   @Real               real
,   @Smalldatetime      smalldatetime
,   @Smallint           smallint
,   @Smallmoney         smallmoney
,   @Text               text
,   @Time               time
,   @Timestamp          timestamp
,   @Tinyint            tinyint
,   @Uniqueidentifier   uniqueidentifier
,   @Varbinary          varbinary
,   @Varchar            varchar
,   @Xml                xml
AS
    SELECT 
        @BigInt
,       @Binary
,       @Bit
,       @Char
,       @Date
,       @DateTime
,       @DateTime2
,       @Decimal
,       @float
,       @image
,       @Int
,       @Money
,       @NChar
,       @NText
,       @Numeric
,       @NVarchar
,       @Real
,       @Smalldatetime
,       @Smallint
,       @Smallmoney
,       @Text
,       @Time
,       @Timestamp
,       @Tinyint
,       @Uniqueidentifier
,       @Varbinary
,       @Varchar
,       @Xml
RETURN 0
