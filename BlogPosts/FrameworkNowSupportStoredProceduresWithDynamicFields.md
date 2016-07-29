# The Stored Procedure Framework Now Supports A Stored Procedure With Dynamic Fields

Until recently when using the Stored Procedure Framework you needed to know the exactly *FieldName* and *DataType* of each column returned from a stored procedure so this could be accurately represented with the corresponding .Net CLR types in the class you needed to define that represents the row returned by the stored procedure. Recently however following a request a change has been made to support dynamic fields in stored procedure results. this allows supporting of stored procedures which contain pivoting of rows to columns or dynamically executed SQL statements.

Take for instance the basic stored procedure below:

CREATE PROCEDURE [app].[GetPossibleDynamicStoredProcedure]
AS
BEGIN
    SELECT  'Dave'      [Firstname],
            'Smith'     [Surname],
            32          [Age],
            GETDATE()   [DateOfBirth]
    UNION

    SELECT  'Peter'     [Firstname],
            'Pan'       [Surname],
            134         [Age],
            GETDATE()   [DateOfBirth];
END

Previously we would have needed a class that defines each field to be returned, which would be too restrictive to call stored procedures with dynamic field names or *DataTypes*.

    [Schema("app")]
    internal class GetPossibleDynamicStoredProcedure
        : NoParametersStoredProcedureBase<List<GetPossibleDynamicStoredProcedure.Return>>
    {    
        internal class Return
        {
            public string Firstname { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }

However, now the *Stored Procedure Framework* has been updated to include support of dynamic fields using the .Net *ExpandoObject* as the type parameter for the return type list.

    [Schema("app")]
    internal class GetDynamicColumnStoredProcedure
        : NoParametersStoredProcedureBase<List<ExpandoObject>>
    {
    }

The *Stored Procedure Framework* will return a list of *ExpandoObjects* which can then be cast to the .Net *dynamic* object as required. An example of this is shown in the unit test below.

    [TestClass]
    public class DynamicColumnStoredProcedure
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void GetDynamicColumnStoredProcedure()
        {
            // ARRANGE
            var procedure = new GetDynamicColumnStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Firstname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Surname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Age"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "DateOfBirth"));
            Assert.IsFalse(DynamicObjectHelper.HasProperty(result, "MiddleName"));

            var dynamicResult = (dynamic) result;
            Assert.AreEqual("Dave", dynamicResult.Firstname);
            Assert.AreEqual("Smith", dynamicResult.Surname);
            Assert.AreEqual(32, dynamicResult.Age);
        }
    } 
    
Note: Currently dynamic fields in stored procedures are only supported with stored procedures having single recordsets. Support for multiple recordsets with dynamic columns is on the roadmap.

## GitHub Source 
The updated source code can be found on GitHub [here](https://github.com/dibley1973/StoredProcedureFramework).

## NuGet Package
The Stored Procedure Framework is also available [here](https://www.nuget.org/packages/Dibware.StoredProcedureFramework/) on NuGet, with its EF counterpart [here](https://www.nuget.org/packages/Dibware.StoredProcedureFrameworkForEF/).

## Disclaimer
I am the author of the *Stored Procedure Framework*.
