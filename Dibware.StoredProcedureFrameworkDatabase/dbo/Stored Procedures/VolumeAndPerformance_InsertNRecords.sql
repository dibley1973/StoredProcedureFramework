-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-09-29
-- Description:	Selects all from the volumn and performance table
-- =============================================
CREATE PROCEDURE [dbo].[VolumeAndPerformance_InsertNRecords] 
    @NumberOfRecords INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @RecordCounter INT;
    SELECT  @RecordCounter = 1;

    /* Field variables */
    DECLARE @Id             [uniqueidentifier],
            @FirstName      [varchar](50),
            @LastName       [varchar](50),
            @Address1       [varchar](50),
            @Address2       [varchar](50),
            @City           [varchar](50),
            @County         [varchar](50),
            @DateOfBirth    [smalldatetime];


    /* Main loop START */
    WHILE @RecordCounter <= @NumberOfRecords
    BEGIN
        /* Prepare random data */        
        SELECT @FirstName = [dbo].[RandomTextGenerator] (50);
        SELECT @LastName =  [dbo].[RandomTextGenerator] (50);
        SELECT @Address1 =  [dbo].[RandomTextGenerator] (50);
        SELECT @Address2 =  [dbo].[RandomTextGenerator] (50);
        SELECT @City =      [dbo].[RandomTextGenerator] (50);
        SELECT @County =    [dbo].[RandomTextGenerator] (50);
        SELECT @DateOfBirth = GETDATE() + (365 * 2 * RAND() - 365);

        /* Insert the record */
        INSERT INTO 
            [dbo].[VolumnAndPerformance]
        (
            FirstName, 
            LastName, 
            Address1, 
            Address2, 
            City, 
            County, 
            DateOfBirth
        )
        VALUES
        (
           @FirstName, 
           @LastName, 
           @Address1, 
           @Address2, 
           @City, 
           @County, 
           @DateOfBirth
        )

        /* Increment counter */
        SET @RecordCounter = @RecordCounter + 1
    END
    /* Main loop END */
END