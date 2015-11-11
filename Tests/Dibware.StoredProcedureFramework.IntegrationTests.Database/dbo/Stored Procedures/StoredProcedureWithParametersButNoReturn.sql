    CREATE PROCEDURE dbo.StoredProcedureWithParametersButNoReturn
        @Id  INT
    AS
    BEGIN
        DELETE FROM dbo.Blah WHERE Id = @Id;
    END