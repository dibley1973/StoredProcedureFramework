# The Stored Procedure Framework Now Supports Dynamic Fields within Multiple Recorsets

A recent update to the **Stored Procedure Framework** was an "emergency enhancement* following a request from a consumer of the framework to add *Dynamic Field* support. This initial fix (v1.0.2) did not allow for *Dynamic Fields* within *Multiple Recordsets*, but only in single Recorsets. This has now been reworked to be a fully fledged fix allowing a stored procedure with "Unknown" *Dynamic Fields* to be returned. An example of how to acheive this is shown below.

Take for instance the stored procedure below which had three Recordsets. Lets take this pre-canned field schema and imagine that the fields in all of these may differ each time the stored procedure is called based upon a set of fictitious parameters. For example maybe the *real* stored procedure would pivot records to field names or something like that:

    CREATE PROCEDURE [dbo].[MultipleRecordSetDynamicColumnStoredProcedure]
    AS
    BEGIN
        /* First Record Set */
        SELECT  'Dave'      [Firstname],
                'Smith'     [Surname],
                32          [Age],
                GETDATE()   [DateOfBirth]
        UNION

        SELECT  'Peter'     [Firstname],
                'Pan'       [Surname],
                134         [Age],
                GETDATE()   [DateOfBirth];

        /* Second Record Set */
        SELECT 
            CAST(1 AS BIT)        AS [Active]
        ,   CAST(10.99 AS MONEY)  AS [Price];

        /* Third Record Set */
        SELECT
            NEWID()   AS [UniqueIdentifier]
        ,   1         AS [Count];
    END

We now need a C# class to represent the stored procedure 'MultipleRecordSetDynamicColumnStoredProcedure'. As this "example" stored procedure does not have any parameters we will inherit from the framework's 'NoParametersStoredProcedureBase' but use the classes internal 'ResultSet' class as the return type. You can see that the 'ResultSet' class has three Lists of the .Net dynamic *ExpandoObject*. These are instantiated in the constructor of the 'ResultSet' class. Each of these lists represent one of the recordsets that the stored procedure will return. Each row in the recordset is represented as an *ExpandObject* as we don't know what the fields will be. If we did we would just set up a pre-canned class with properties representing the fields.

    internal class MultipleRecordSetDynamicColumnStoredProcedure
        : NoParametersStoredProcedureBase<
            MultipleRecordSetDynamicColumnStoredProcedure.ResultSet>
    {
        internal class ResultSet
        {
            public List<ExpandoObject> RecordSet1 { get; private set; }
            public List<ExpandoObject> RecordSet2 { get; private set; }
            public List<ExpandoObject> RecordSet3 { get; private set; }

            public ResultSet()
            {
                RecordSet1 = new List<ExpandoObject>();
                RecordSet2 = new List<ExpandoObject>();
                RecordSet3 = new List<ExpandoObject>();
            }
        }
    }

We can now call the 'MultipleRecordSetDynamicColumnStoredProcedure' using code similar to the unit test below.
 
    [TestMethod]
    public void MultipleRecordSetStoredDynamiccolumProcedure_WithThreeSelects_ReturnsCorrectDataValues()
    {
        // ARRANGE
        var procedure = new MultipleRecordSetDynamicColumnStoredProcedure();

        // ACT
        var resultSet = Connection.ExecuteStoredProcedure(procedure);

        var results1 = resultSet.RecordSet1;
        var result1 = results1.First() as dynamic;

        var results2 = resultSet.RecordSet2;
        var result2 = results2.First() as dynamic;

        var results3 = resultSet.RecordSet3;
        var result3 = results3.First() as dynamic;

        // ASSERT
        Assert.IsNotNull(result1);
        Assert.AreEqual("Dave", result1.Firstname);
        Assert.AreEqual("Smith", result1.Surname);
        Assert.AreEqual(32, result1.Age);

        Assert.IsNotNull(result2);
        Assert.AreEqual(true, result2.Active);
        Assert.AreEqual(10.99, Math.Round(((double)result2.Price), 2));

        Assert.IsNotNull(result3);
        Assert.AreEqual(1, result3.Count);
    }    

So as you can see from our example in the test we do know what columns we are expecting, but you may want to use this feature if you are returning some dynamic data to be converted into JSON, or maybe for a generic reporting framework you are using which will convert the data nicely into a dynamic table.

Hopefully there are others who can now leverage these new features within the Stored Procedure Framework.

    
## GitHub Source 
The documentation and the source code can be found on GitHub [here](https://github.com/dibley1973/StoredProcedureFramework).

## NuGet Package
The Stored Procedure Framework is also available [here](https://www.nuget.org/packages/Dibware.StoredProcedureFramework/) on NuGet, with its EF counterpart [here](https://www.nuget.org/packages/Dibware.StoredProcedureFrameworkForEF/).

## Disclaimer
I am the author of the *Stored Procedure Framework*.
