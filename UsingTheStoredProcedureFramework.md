# Using the StoredProcedureFramework

## Representing Stored Procedures in Code
(All code examples can be found in the **Examples** folder of the **Dibware.StoredProcedureFramework.Tests** project

### General Rules
To represent a Stored Procedure in the StoredProcedureFramework we need to create a **P**lain **O**ld **C**LR **O**bject class for. The stored procedure class must inherit from the **StoredProcedureBase** base class which exists in the **Dibware.StoredProcedureFramework** namespace of the framework. The **StoredProcedureBase** base class demands two type parameters; **TReturn** and **TParameters**. 

The **TReturn** type parameter define the type that represents a single row in the result set. 
The **TParameters** type parameter defines the type that represents the collection of parameters which the stored procedure requires.

The class will also require one of three constructor signatures:

#### ctor(TParameters parameters) : base(parameters) {}
The first constructor option just takes an argument of the type that is defined by the **TParameters** type parameter. This is the bare minimum needed to construct a stored procedure and can be used if the stored procedure has the same name as the class which represents it and if the stored procedure is owned by the **dbo** schema and custom attributes are not being used to set the schema and procedure names.

#### ctor(string procedureName, TParameters parameters) : base(procedureName, parameters) {}
The second constructor option takes a the procedure name as string as well as the object that represents the parameters. This constructor can be used of the stored procedure is owned by the **dbo** schema and custom attributes are not being used to set the schema.

####ctor(string schemaName, string procedureName, TParameters parameters) : base(schemaName, procedureName, parameters) {}
The third constructor option takes a the schema name, the procedure name and the object that represents the parameters.


**PLEASE NOT THIS DOCUMENT IS STILL BEING UPDATED FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**

### The most basic type of stored procedure
The most basic type of stored procedure is one that has no parameters and returns no result. for example a stored procedure that just perform an action like purging some history, but does not take a parameter as it loads information from a configuration table and performs its action based upon that. 

    CREATE PROCEDURE dbo.MostBasicStoredProcedure
    AS
    BEGIN
        -- Does some function here...
    END

As the **StoredProcedureBase** expects two type parameters so we must provide a class for each, the framework provides to concrete classes that can be used when there is not return type and or no parameter type. These both exist in the **Dibware.StoredProcedureFramework** namespace and are:
#### NullStoredProcedureParameters
    /// <summary>
    /// An object that represents the absence of parameters
    /// for a stored procedure
    /// </summary>
    public class NullStoredProcedureParameters
    {
    }

#### NullStoredProcedureResult
    /// <summary>
    /// An object that represents the absence of an 
    /// expected result from a stored procedure
    /// </summary>
    public class NullStoredProcedureResult
    {
    }

So we could define our class that represent this stored procedure as follows:

    internal class MostBasicStoredProcedure
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public MostBasicStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

But this is a bit cumbersome for such a basic stored procedure, so the framework provides another base class **NoParametersNoReturnTypeStoredProcedureBase** which our stored procedure class can inherit from making our code more succinct.

    internal class MostBasicStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        
    }

### A Stored Procedure without Parameters
The next stored procedure to look at is one which returns a result but does not have any parameters. This would typically be used for your "GetAll_X" type of stored procedure, for example:

    CREATE PROCEDURE dbo.StoredProcedureWithoutParameters
    AS
    BEGIN
        SELECT * FROM dbo.Blah;
    END

Which selects ALL records from the "Blah" table in the "dbo" schema.

    CREATE TABLE Blah (
        [Id]    INT    
    ,   [Name]  VARCHAR(50)
    )

For this procedure we need to define a class that will represent a row of data in our results. We need a property in this class to match the name and data type of each of the fields in the row of the results returned from the "StoredProcedureWithoutParameters" stored procedure

    internal class StoredProcedureWithoutParametersReturntype
    {
        int Id { get; set; }
        string Name { get; set; }
    }

We need to to define a class that represnts the result set for the rpocedure. As Stroed procedures can return multiple recordsets we need the resultset will have one or more collections of return types. For this storedprocedure we need only one
    internal class StoredProcedureWithoutParametersResultSet
    {
        public List<StoredProcedureWithoutParametersReturntype> RecordSet { get; set; }

        public StoredProcedureWithoutParametersResultSet()
        {
            RecordSet = new List<StoredProcedureWithoutParametersReturntype>();
        }
    }

We must ensure the RecordSet is instantiated before use. We now need to define a class which represents the stored procedure it self. First we will define the class the verbose way, in which we will define the return type **StoredProcedureWithoutParametersReturntype** in the **TReturn** type parameter and the lack of parameters for the stored procedure using the **NullStoredProcedureParameters** class in the **TParameters** type parameter.

    internal class StoredProcedureWithoutParameters
        : StoredProcedureBase<StoredProcedureWithoutParametersResultSet, NullStoredProcedureParameters>
    {
        public StoredProcedureWithoutParameters()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

As this is a common stored procedure scenario the framework also contains another base class to make definition of this kind of stored procedure less verbose. This base class is the **NoParametersStoredProcedureBase**

    internal class StoredProcedureWithoutParameters
        : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersResultSet>
    {
    }

### A Stored Procedure with Parameters but without a Return Type
The next stored procedure in complexity is a procedure that takes a parameter but does not return a result set. For example a **DeleteById** stored procedure. So taking the example stored procedure below which deletes a record from the Blah table based upon the Id.

    CREATE PROCEDURE dbo.StoredProcedureWithParametersButNoReturn
        @Id  INT
    AS
    BEGIN
        DELETE FROM dbo.Blah WHERE Id = @Id;
    END

First we need to define a class which represents the parameters for the stored procedure.

    internal class StoredProcedureWithParametersButNoReturnParameters
    {
        int Id { get; set; }
    }

The we can define the class which will represent the stored procedure and will use the parameters type define above as the **TParameters** type parameter.

    internal class StoredProcedureWithParametersButNoReturn
        : StoredProcedureBase<NullStoredProcedureResult, StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

You will notice that we have to define the **TReturn** type parameter as **NullStoredProcedureResult** as no results are expected to be returned form the stored procedure when it is called. We also now need a constructor which takes a parameters argument of StoredProcedureWithParametersButNoReturnParameters. This just passes straight through to the base class which handles construction tasks.

The framework lets us short cut the definition slightly by omitting the **NullStoredProcedureResult** as teh **TReturn** return type parameter and using another base class, the **NoReturnTypeStoredProcedureBase** class, like so:

    internal class StoredProcedureWithParametersButNoReturn
        : NoReturnTypeStoredProcedureBase<StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

We do still need the constructor as the parameters will hold the actual values we wish to pass to the stored procedure in the database.

This now brings us on nicely to the "normal" and probably the most common type of stored procedure; one that takes parameters and returns a result set.

### A "Normal" Stored procedure ###
This represents what I consider to be the bread-and-butter of stored procedures; a procedure that has only input parameters, and returns a single result set. It might be the "GetMeSomethingById" type of stored procedure. So below is our stored procedure.

    CREATE PROCEDURE dbo.NormalStoredProcedure
        @Id  INT
    AS
    BEGIN
        SELECT 
            @Id AS Id
        ,   'Dave' AS Name
        ,   CAST(1 AS BIT) AS Active
    END

We need a class that represents the parameters. The default parameter data type is a string / varchar combination. So for our procedure which uses an integer, we need to explicitly state the parameter type is an integer **SqlDBType** and we can do this using the **ParameterDbType** attribute.

    internal class NormalStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
    }

...and a class that represents the row we will return.

    internal class NormalStoredProcedureReturnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

Once we have those we can define a class that represents the stored procedure.

    /// <summary>
    /// Represents a "normal" stored procedure which has parameters and returns
    /// a single result set
    /// </summary>
    internal class NormalStoredProcedure
        : StoredProcedureBase<NormalStoredProcedureReturnType, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedure(NormalStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

You will see the class inherits from the "StoredProcedureBase" and uses the return type and parameters classes defined above as the type parameters. At very least we need to provide a "pass-through" constructor with a argument which is of the same type as "NormalStoredProcedureParameters" class. 

Now we have created classes to represent the most common types of stored procedures lets now look at how we go about calling these procedures.

## Calling the Stored Procedures from Code

The framework provides extension methods which can be used to call the stored procedures on three key .Net data access objects. **SqlConnection**, **DbConnection** and also **DbContext**.  The extension methods for **SqlConnection**, **DbConnection** can be found in the main **Dibware.StoredProcedureFramework** assembly, but for the **DbContext** extensions there is a separate assembly, **Dibware.StoredProcedureFrameworkForEF**. This is to prevent the need for a dependency on **Entity Framework** in the main assembly and hence extra bloat. If your project does not have **Entity Framework** or you are not using the **DbContext** extensions then you don't need a reference to **Dibware.StoredProcedureFrameworkForEF** to call the procedures. 

Regardless of whether you are using **Entity Framework**or not you will _always _ need a reference to the main **Dibware.StoredProcedureFramework** assembly!

So for the purpose of the examples we will call the extension method on the DbContext object, but the code is basically the same when called on the **SqlConnection** or the **DbConnection**.

        [TestMethod]
        public void NullValueParameterProcedure_WithNullableParamatersAndReturnType_ReturnsCorrectValues()
        {
            // ARRANGE  
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);
            
            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }

So reading down through the test we can see first we are setting up our expected result (based upon what we know the stored procedure _SHOULD_ return). We then need to instantiate and populate a parameters object. We can then use the parameters object to instantiate our stored procedure giving us everything set up and ready to go.

The Context in this test inherits from an entity Framework **DbContext** so I can execute the stored procedure by calling **Context.ExecuteStoredProcedure(...)** passing in the instantiated stored procedure object. This will return a list of results, which in this case we know will be a single record so can use LinQ to provide the this.


## TBC...