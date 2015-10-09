# Using the StoredProcedureFramework

**PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**

## Representing Stored Procedures in Code
(All code examples can be found in the **Examples** folder of the **Dibware.StoredProcedureFramework.Tests** project

### General Rules
To represent a Stored Procedure when using the StoredProcedureFramework we need to create a **P**lain **O**ld **C**LR **O**bject class for it. The stored procedure class must inherit from one of a predetermined number of base classes. These base classes all exists in the **Dibware.StoredProcedureFramework.Base** namespace of the framework. In most cases the class should inherit from the **StoredProcedureBase** base class, but three other base class *shortcuts* are also provided for convenience.

#### Base Classes
There are four base classes from which a stored procedure must be defined to inherit from.
* StoredProcedureBase - for Stored Procedures with parameters and return types
* NoParametersStoredProcedureBase - for Stored Procedures with return types but no parameters
* NoReturnTypeStoredProcedureBase - for Stored procedures with parameters but no return types
* NoParametersNoReturnTypeStoredProcedureBase - for Stored Procedures without parameters or returns types

##### StoredProcedureBase
The **StoredProcedureBase** base class is probably the most frequently used base class as it is for a stored procedure which takes parameters and also returns a *ResultSet*. This will be used for your *GetProductForId* type of stored procedures and when inherited from this class demands two *Type Parameters* to be defined; **TReturn** and **TParameters**.

##### NoParametersStoredProcedureBase
The **NoParametersStoredProcedureBase** base class is used for a stored procedure which does not have parameters but does return a *ResultSet*. This will be used for your *GetAllProducts* type of stored procedures and when inherited from this class demands one *Type Parameter* to be defined; **TReturn**.

##### NoReturnTypeStoredProcedureBase
The **NoReturnTypeStoredProcedureBase** base class is used for a stored procedure which does have one or more parameters but does not return a *ResultSet*. This will be used for your *DeleteProductById* type of stored procedures and when inherited from this class demands one *Type Parameter* to be defined; **TParameters**.

#####NoParametersNoReturnTypeStoredProcedureBase
The **NoParametersNoReturnTypeStoredProcedureBase** base class is used for a stored procedure which do not have any parameters and also do not return a *ResultSet*. This will be used for your *RunHouseKeepingBatch* type of stored procedures and when inherited from does not demand any *Type Parameters* to be defined.

#### Type Parameters
Typically when defining the stored procedure POCO class there will be a requirement to define one or both of the following *Type Parameters*. 
* TReturn
* TParameters

##### TReturn
The **TReturn** type parameter defines the type that represents *ResultSet* which is what will be returned from a stored procedure when it is executed. The *ResultSet* must contain one or more properties which are in effect *RecordSets* or lists of return types.

##### TParameters
The **TParameters** type parameter defines the type that represents the collection of parameters which the stored procedure requires.

#### Constructors
The stored procedure POCO class will also require one of three constructor signatures:

##### ctor()
The default constructor is only available for stored procedures which do not have parameters. This is the bare minimum needed to construct a stored procedure and can be used if the stored procedure has the same name as the class which represents it and if the stored procedure is owned by the **dbo** schema and custom attributes are not being used to set the schema and procedure names.

##### ctor(string procedureName) : base(procedureName) {}
This constructor is is only available for stored procedures which do not have parameters, but takes the procedure name. This constructor can be used of the stored procedure is owned by the **dbo** schema but has name that differs from the name of the POCO class.

##### ctor(string schemaName, string procedureName) : base(schemaName, procedureName) {}
This constructor is is only available for stored procedures which do not have parameters but takes a the schema name, the procedure name and the object that represents the parameters. This constructor can be used to set the schema name and the procedure name.

##### ctor(TParameters parameters) : base(parameters) {}
This constructor is is only available for stored procedures which do have parameters. It takes an argument of the type that is defined by the **TParameters** type parameter. This is the bare minimum needed to construct a stored procedure and can be used if the stored procedure has the same name as the class which represents it and if the stored procedure is owned by the **dbo** schema and custom attributes are not being used to set the schema and procedure names.

##### ctor(string procedureName, TParameters parameters) : base(procedureName, parameters) {}
This constructor is is only available for stored procedures which do have parameters. This constructor can be used of the stored procedure is owned by the **dbo** schema but has name that differs from the name of the POCO class. It takes an argument of the type that is defined by the **TParameters** type parameter. It also takes a the procedure name as string as well as the object that represents the parameters. 

##### ctor(string schemaName, string procedureName, TParameters parameters) : base(schemaName, procedureName, parameters) {}
This constructor is is only available for stored procedures which do have parameters. This constructor can be used to set the schema name and the procedure name. It takes an argument of the type that is defined by the **TParameters** type parameter. It also takes a the schema name and procedure name as string as well as the object that represents the parameters.  


### The most basic type of stored procedure
The most basic type of stored procedure is one that has no parameters and returns no result. For example a stored procedure that just performs an action like purging some history, but does not take a parameter as it loads information from a configuration table and performs its action based upon that, and does not return any *ResultSet*, like below. 

    CREATE PROCEDURE dbo.MostBasicStoredProcedure
    AS
    BEGIN
        -- Does some function here...
        PRINT 'Some silent operation'
    END

As the **StoredProcedureBase** abstract class expects two type parameters if we wish to inherit from that we would have to provide a class for each. The framework already provides to concrete classes that can be used when there is not return type and or no parameter type. These both exist in the **Dibware.StoredProcedureFramework** namespace and are:

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

So we could define the class that represents this stored procedure as follows:

    internal class MostBasicStoredProcedure
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public MostBasicStoredProcedure2()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

But this is a bit cumbersome for such a basic stored procedure, so the framework provides another abstract base class **NoParametersNoReturnTypeStoredProcedureBase** which our stored procedure class can inherit from making our code more succinct.

    internal class MostBasicStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
    }

We do not need to provide a constructor as the **NoParametersNoReturnTypeStoredProcedureBase** already handles this for us in its default constructor.


### A Stored Procedure without Parameters
The next stored procedure to look at is one which returns a result but does not have any parameters. This would typically be used for your *MyTable_GetAll* type of stored procedure, for example:

    CREATE PROCEDURE dbo.StoredProcedureWithoutParameters
    AS
    BEGIN
        SELECT * FROM dbo.Blah;
    END

Which selects ALL records from the *Blah* table in the *dbo* schema. So in our example we will assume the *Blah* table is defined and populated as below.

    CREATE TABLE Blah (
        [Id]    INT    
    ,   [Name]  VARCHAR(50)
    );
    INSERT INTO Blah
    (
        [Id] 
    ,   [Name]
    )
    VALUES 
    (
        1
    ,   'Sid'
    );

As this procedure returns data we need to define a class that will represent a row of data in our result *RecordSet*. For each field in the *RecordSet* we need a property in this class to represent it. The property must match the *Name* and *DataType* of the field it represents in the *RecordSet* row returned. So in the example case of the "StoredProcedureWithoutParameters" stored procedure we are looking at a class which contains an *Id* property of type *int* and a *Name* property of type *string*, as below.

    internal class StoredProcedureWithoutParametersReturnType
    {
        int Id { get; set; }
        string Name { get; set; }
    }

As the framework is capable of handling Stored procedures which can return *Multiple RecodSets* in a *ResultSet* we need to to define a class that represents the *ResultSet* for the Stored Procedure. The *ResultSet* will have one or more properties which are collections of *ReturnTypes*. As this Stored Procedure only returns one *RecordSet* we need only one property in the *ResultSet*. We must remember to instantiate the *RecordSet* in the constructor for the *ResultSet* before use. 

    internal class StoredProcedureWithoutParametersResultSet
    {
        public List<StoredProcedureWithoutParametersReturnType> RecordSet { get; set; }

        public StoredProcedureWithoutParametersResultSet()
        {
            RecordSet = new List<StoredProcedureWithoutParametersReturnType>();
        }
    }

**PLEASE NOTE:** You do not need to name the *RecordSet* property *RecordSet*. You can use a more meaningful name like *Orders* or  *Products*, or what ever the data in the RecordSet represents.
    
We now need to define a class which represents the stored procedure it self. First we will define the class the verbose way, inheriting from **StoredProcedureBase** where we would have to define the return type **StoredProcedureWithoutParametersResultSet** in the **TReturn** type parameter and the lack of parameters for the stored procedure using the **NullStoredProcedureParameters** class in the **TParameters** type parameter.

    internal class StoredProcedureWithoutParameters
        : StoredProcedureBase<StoredProcedureWithoutParametersResultSet, NullStoredProcedureParameters>
    {
        public StoredProcedureWithoutParameters()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

*However*, as this format of Stored Procedure is a common scenario the framework also contains another base class to make definition of this kind of stored procedure a little less verbose. This best base class to use in this case is the **NoParametersStoredProcedureBase**. Again we do not need to worry about a constructor as the **NoParametersStoredProcedureBase** base class handles supplying the *NullStoredProcedureParameters* to it's base class (the *StoredProcedureBase* class) for us, which allows us to define the procedure like so:

    internal class StoredProcedureWithoutParameters
        : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersResultSet>
    {
    }


### A Stored Procedure with Parameters but without a Return Type
The next type of stored procedure in complexity is a procedure that takes a parameter but does not return a result set. For example a **DeleteById** stored procedure. So taking the example stored procedure below which deletes a record from the Blah table based upon the Id.

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

The we can define the class which will represent the stored procedure and will use the parameters type define above as the **TParameters** type parameter. We also now need a constructor which takes a parameters argument of StoredProcedureWithParametersButNoReturnParameters. This just passes straight through to the base class which handles construction tasks.

    internal class StoredProcedureWithParametersButNoReturn
        : StoredProcedureBase<NullStoredProcedureResult, StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

You will notice that we have to define the **TReturn** type parameter as **NullStoredProcedureResult** as no results are expected to be returned form the stored procedure when it is called. The framework lets us short cut the definition slightly by omitting the **NullStoredProcedureResult** as the **TReturn** return type parameter and using another base class, the **NoReturnTypeStoredProcedureBase** class. We can inherit from this class like so:

    internal class StoredProcedureWithParametersButNoReturn
        : NoReturnTypeStoredProcedureBase<StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

This now brings us on nicely to what I call the "normal" and probably the most common type of stored procedure; one that takes parameters and returns a result set.

**PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED BELOW FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**
**PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED BELOW FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**
**PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED BELOW FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**

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


### Multiple RecordSets 

TBC... (This section is yet to be completed )

**PLEASE NOTE:** You do not need to name the *RecordSet* properties *RecordSet1*, *RecordSet2* etc. You can use a more meaningful name like *Account* and *Orders*, or what ever the data in each RecordSet represents.




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