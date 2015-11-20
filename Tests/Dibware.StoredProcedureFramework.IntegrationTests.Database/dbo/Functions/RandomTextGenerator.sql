
CREATE FUNCTION [dbo].[RandomTextGenerator]
(
    @NumberOfCharacters INT
)
RETURNS VARCHAR(255)
AS
BEGIN
    -- get a random varchar ascii char 32 to 128
    DECLARE @Text   VARCHAR(255),
            @Length INT;
    SELECT  @Text = ''
    SELECT  @Length = ((SELECT RandomValue FROM RANDOM) * (@NumberOfCharacters -1)) + 1;
    WHILE   @Length > 0
    BEGIN
        SELECT @Text = @Text + CHAR(CAST((SELECT RandomValue FROM RANDOM) * 96 + 32 as INT))
        SET @Length = @Length - 1
    END
    RETURN @Text;
END