# Using the StoredProcedureFramework

**PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED FOLLOWING AN API CHANGE FOR MULTIPLE RECORDSETS!**

## TOC

* Representing Stored Procedures in Code
  +  General Rules
    -   Base Classes
      * StoredProcedureBase
      * NoParametersStoredProcedureBase
      * NoReturnTypeStoredProcedureBase
      * NoParametersNoReturnTypeStoredProcedureBase
    - Type Parameters
      * TReturn
      * TParameters
    - Constructors
      * ctor()
      * ctor(string procedureName)
      * ctor(string schemaName, string procedureName)
      * ctor(TParameters parameters)
      * ctor(string procedureName, TParameters parameters)
      * ctor(string schemaName, string procedureName, TParameters parameters)
  + The most basic type of stored procedure
  + A Stored Procedure without Parameters
  + A Stored Procedure with Parameters but without a Return Type
  + A "Normal" Stored procedure
  + A Stored Procedure With Multiple RecordSets
* [Calling the Stored Procedures from Code using SqlConnection](#calling-the-stored-procedures-from-code-using-sqlconnection)
* [Calling the Stored Procedures from Code using DbContext](#calling-the-stored-procedures-from-code-using-dbcontext)

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
        public int Id { get; set; }
        public string Name { get; set; }
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

First we need a class that represents the stored procedure parameters. The default parameter data type is a *string* / *VarChar* combination. So for our procedure which uses an integer, we need to explicitly state the parameter type is an integer **SqlDBType** and we can do this using the **ParameterDbType** attribute. If we do not specify the parameter type the framework will fall back to the **default** *string* / *VarChar* *DataType* . If the Stored procedure had multiple parameters then there would be multiple properties representing each one.

    internal class StoredProcedureWithParametersButNoReturnParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
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

### A "Normal" Stored procedure
This represents what I consider to be the bread-and-butter of stored procedures; a procedure that has only input parameters, no output parameters, and returns a single result set. It might be the "GetMeSomethingById" type of stored procedure. So taking the stored procedure below...

    CREATE PROCEDURE dbo.NormalStoredProcedure
        @Id  INT
    AS
    BEGIN
        SELECT 
            @Id AS Id
        ,   'Dave' AS Name
        ,   CAST(1 AS BIT) AS Active
    END

So below we have a class which represents all of our parameters, albeit with only a single property for the single parameter. again as the parameter is not the default string type we need to explicitly state what *DataType* it is by decorating the property with a *ParameterDbType* attribute.

    internal class NormalStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
    }

Once the parameters class is complete we need to create a class that represents the row  from our the first and only RecordSet that we will return.

    internal class NormalStoredProcedureReturnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

We now need to create a class that will represent the *ResultSet* that the stored procedure will return and in this case the ResultSet will have a single RecordSet property. This is a List of the row *ReturnType* we have already declared. Remember you can call the RecordSet property a more meaning full name like *Products* or *Accounts*, but don't forget to instantiate the recordSet in the ResultSet's constructor.

    internal class NormalStoredProcedureResultSet
    {
        public List<NormalStoredProcedureRecordSet1ReturnType> RecordSet1 { get; set; }

        public NormalStoredProcedureResultSet()
        {
            RecordSet1 = new List<NormalStoredProcedureRecordSet1ReturnType>();
        }
    }
    
Once we have those three classes defined we can define a class that represents the stored procedure which pulls them altogether. This class inherits from the *StoredProcedureBase* and take the *NormalStoredProcedureResultSet* as the *TReturn* type parameter, and the *NormalStoredProcedureParameters* as the TParameters* Type pararmeter. We also need to provide a "pass-through" constructor with a argument which is of the same type as "NormalStoredProcedureParameters" class. 

    /// <summary>
    /// Represents a "normal" stored procedure which has parameters and returns
    /// a single result set
    /// </summary>
    internal class NormalStoredProcedure
        : StoredProcedureBase<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedure(NormalStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

Now we will look at a variation on this stored procedure, by looking at a stored procedure that returns multiple RecordSets.

### A Stored Procedure With Multiple RecordSets 

So for our *Multiple RecordSet* stored procedure example we will use the following stored procedure:

    CREATE PROCEDURE [dbo].[MultipleRecordSetStoredProcedure]
        @Id                 INT
    ,   @Name               VARCHAR(20)
    ,   @Active             BIT
    ,   @Price              DECIMAL(10, 4)
    ,   @UniqueIdentifier   UNIQUEIDENTIFIER
    ,   @Count              TINYINT
    AS
    BEGIN
        /* First Record Set */
        SELECT 
            @Id     AS Id
        ,   @Name   AS Name
        UNION
        SELECT
            17      AS Id
        ,   'Bill'  AS Name;

        /* Second Record Set */
        SELECT 
            @Active as Active
        ,   @Price  AS Price;

        /* Third Record Set */
        SELECT
            @UniqueIdentifier   AS [UniqueIdentifier]
        ,   @Count              AS [Count];
        
    END

You can see that each *RecordSet* has a different signature. The first returns a column called *Id* of type *INT* and a column called *Name* of type *VARCHAR(20)*. The second returns a column called *Active* of type *BIT* and a column called *Price* of type *DECIMAL(10, 4)* and the last returns a column called *UniqueIdentifier* of type *UNIQUEIDENTIFIER* together with a column called *Count* of type *TINYINT*. we will need an object to represent each of these return row signatures, and these you an see below.

    internal class MultipleRecordSetStoredProcedureReturnType1
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    internal class MultipleRecordSetStoredProcedureReturnType2
    {
        [ParameterDbType(SqlDbType.Bit)]
        public bool Active { get; set; }

        [ParameterDbType(SqlDbType.Decimal)]
        public decimal Price { get; set; }
    }

    internal class MultipleRecordSetStoredProcedureReturnType3
    {
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid UniqueIdentifier { get; set; }

        [ParameterDbType(SqlDbType.TinyInt)]
        public byte Count { get; set; }
    }

We will need a *ResultSet* object to hold each *RecordSet* list. We must remember to instantiate each *RecordSet* in the constructor
    
    internal class MultipleRecordSetStoredProcedureResultSet
    {
        public List<MultipleRecordSetStoredProcedureReturnType1> RecordSet1 { get; set; }
        public List<MultipleRecordSetStoredProcedureReturnType2> RecordSet2 { get; set; }
        public List<MultipleRecordSetStoredProcedureReturnType3> RecordSet3 { get; set; }

        public MultipleRecordSetStoredProcedureResultSet()
        {
            RecordSet1 = new List<MultipleRecordSetStoredProcedureReturnType1>();
            RecordSet2 = new List<MultipleRecordSetStoredProcedureReturnType2>();
            RecordSet3 = new List<MultipleRecordSetStoredProcedureReturnType3>();
        }
    }

**PLEASE NOTE:** We do not need to name the *RecordSet* properties *RecordSet1*, *RecordSet2* etc. We could just as easily use a more meaningful name like *Account* and *Orders*, or what ever the data in each RecordSet represents.

We will need a parameters object to represent the six parameters which the stored procedure demands.

    internal class MultipleRecordSetStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }

        [Size(20)]
        public string Name { get; set; }

        [ParameterDbType(SqlDbType.Bit)]
        public bool Active { get; set; }

        [ParameterDbType(SqlDbType.Decimal)]
        [Precision(10)]
        [Scale(4)]
        public decimal Price { get; set; }

        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid UniqueIdentifier { get; set; }

        [ParameterDbType(SqlDbType.TinyInt)]
        public byte Count { get; set; }
    }

And finally we need a class to represent the complete Stored Procedure:

    internal class MultipleRecordSetStoredProcedure
        : StoredProcedureBase<MultipleRecordSetStoredProcedureResultSet, MultipleRecordSetStoredProcedureParameters>
    {
        public MultipleRecordSetStoredProcedure(MultipleRecordSetStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    } 
    
Now we have created classes to represent the most common types of stored procedures lets now look at how we go about calling these procedures.

## Calling the Stored Procedures from Code using SqlConnection

The framework provides extension methods which can be used to call the stored procedures on three key .Net data access objects. **SqlConnection**, **DbConnection** and also **DbContext**.  The extension methods for **SqlConnection**, **DbConnection** can be found in the main **Dibware.StoredProcedureFramework** assembly, but for the **DbContext** extensions there is a separate assembly, **Dibware.StoredProcedureFrameworkForEF**. This is to prevent the need for a dependency on **Entity Framework** in the main assembly and hence extra bloat. If your project does not have **Entity Framework** or you are not using the **DbContext** extensions then you don't need a reference to **Dibware.StoredProcedureFrameworkForEF** to call the procedures. 

Regardless of whether you are using *this* stored procedure framework alongside **Entity Framework** or not you will *always* need a reference to the main **Dibware.StoredProcedureFramework** assembly. 

So for the purpose of the examples we will call the extension method on the **SqlConnection** object, but the code is basically the same when called on the **DbConnection** or the **DbContext** objects. So lets use the *MostBasicStoredProcedure* which we defined earlier in this document and call it using the extension method on the SqlConnection Object. Remember this procedure has no parameters and returns no results. For convenience we will use a the MSTest harness, but it's not really a valid integration test, just example code. 

    [TestMethod]
    public void EXAMPLE_ExecuteMostBasicStoredProcedureOnSqlConnection)
    {
        // ARRANGE
        var procedure = new MostBasicStoredProcedure();
        var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        // ACT
        using (DbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            connection.ExecuteStoredProcedure(procedure);
        }

        // ASSERT
        // Should get here as Exception should not have been thrown
    }

So reading down through the test we can see first in the *ARRANGE* section that we are instantiating our stored procedure POCO class and a connection string. Then in the  *ACT*, once the *SqlConnection* object is created and the connection opened we can execute the stored procedure by passing our instantiated Stored Procedure object to the *ExecuteStoredProcedure* extension method on the *SqlConnection* object. We we do not expect a *ResultSet* from this stored procedure we have no need to capture it.

Now lets look at calling a stored procedure which does return results, but still does not have parameters. We will call the *StoredProcedureWithoutParameters* defined earlier in this document. See below to recap on how the objects for this stored procedure are defined.

    internal class StoredProcedureWithoutParameters
        : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersResultSet>
    {
    }

    internal class StoredProcedureWithoutParametersResultSet
    {
        public List<StoredProcedureWithoutParametersReturnType> RecordSet { get; set; }

        public StoredProcedureWithoutParametersResultSet()
        {
            RecordSet = new List<StoredProcedureWithoutParametersReturnType>();
        }
    }

    internal class StoredProcedureWithoutParametersReturnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

So assuming the values that will be returned have remained the same as when we defined the earlier we should get a value of *1* for the first row's *Id* field and a value of *Sid* for the *Name* field. We will use the example code below to call the procedure.

    [TestMethod]
    public void EXAMPLE_ExecuteStoredProcedureWithoutParametersOnSqlConnection()
    {
        // NOTE:
        // You need a record in the [dbo].[Blah] table with the following values for this test to pass!
        // |--------------------|
        // |    Id  |   Name    |
        // |====================|
        // |    1   |   Sid     |
        // |--------------------|

        // ARRANGE
        var procedure = new StoredProcedureWithoutParameters();
        var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
        StoredProcedureWithoutParametersResultSet resultSet;
        List<StoredProcedureWithoutParametersReturnType> resultList;
        StoredProcedureWithoutParametersReturnType firstResult;

        // ACT
        using (DbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            resultSet = connection.ExecuteStoredProcedure(procedure);
        }
        resultList = resultSet.RecordSet;
        firstResult = resultList.First();

        // ASSERT
        Assert.AreEqual(1, firstResult.Id);
        Assert.AreEqual("Sid", firstResult.Name);
    }

Set up is similar to what we did for the *MostBasicStoredProcedure*, but we need need a variable to hold the stored procedure's *ResultSet* and also a list to hold the *RecordSet*. For the sake of this example we will also create a variable to hold the first result, so we can make a couple of assertions against it. To access the results of the stored procedure we need to capture the return value of the *ExecuteStoredProcedure* extension method into the *resultSet* variable. This value is strongly typed and in this case will be of the type *StoredProcedureWithoutParametersResultSet*. from this we can drill in to the *RecordSet* and the individual records within the *RecordSet*.

Next up is a a stored procedure with parameters but no *ResultSet*, so we will use the *StoredProcedureWithParametersButNoReturn* from earlier in the document along with it's *StoredProcedureWithParametersButNoReturnParameters* class.

    internal class StoredProcedureWithParametersButNoReturn
        : NoReturnTypeStoredProcedureBase<StoredProcedureWithParametersButNoReturnParameters>
    {
        public StoredProcedureWithParametersButNoReturn(StoredProcedureWithParametersButNoReturnParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class StoredProcedureWithParametersButNoReturnParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
    }

Looking at the example calling code below we can see that once we have instantiated and set up the parameters and then constructed the stored procedure with those parameters, we can then call the procedure.

    [TestMethod]
    public void EXAMPLE_ExecuteStoredProcedureWithParametersButNoReturnOnSqlConnection()
    {
        // ARRANGE
        var parameters = new StoredProcedureWithParametersButNoReturnParameters
        {
            Id = 1
        };
        var procedure = new StoredProcedureWithParametersButNoReturn(parameters);
        var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        // ACT
        using (DbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            connection.ExecuteStoredProcedure(procedure);
        }
    }

We do not need to concern our selves with a *ReturnType* or *ResultSet* as the procedure returns no results.

So now to move on to a stored procedure which takes parameters and returns results. For this we will use the *NormalStoredProcedure* class and associated parameters object, *ReturnType* and *ResultSet* which we [defined earlier in the document](#a-normal-stored-procedure)...

    /// <summary>
    /// Represents a "normal" stored procedure which has parameters and returns
    /// a single result set
    /// </summary>
    internal class NormalStoredProcedure
        : StoredProcedureBase<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedure(NormalStoredProcedureParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class NormalStoredProcedureResultSet
    {
        public List<NormalStoredProcedureRecordSet1ReturnType> RecordSet1 { get; set; }

        public NormalStoredProcedureResultSet()
        {
            RecordSet1 = new List<NormalStoredProcedureRecordSet1ReturnType>();
        }
    }

    internal class NormalStoredProcedureParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int Id { get; set; }
    }

    internal class NormalStoredProcedureRecordSet1ReturnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

We can see in the example test below that the first task is to instantiate the parameters, which we then use to instantiate the stored procedure POCO. When we execute the procedure against the *SqlConnection* we get the *ResultSet* back which we can drill into to get the collection of records from the *RecordSet*. as we know our procedure returns canned results we can assert our expected values.

    [TestMethod]
    public void EXAMPLE_NormalStoredProcedure_WhenCalledOnSqlConnection_ReturnsCorrectValues()
    {
        // ARRANGE  
        const int expectedId = 10;
        const string expectedName = @"Dave";
        const bool expectedActive = true;

        var parameters = new NormalStoredProcedureParameters
        {
            Id = expectedId
        };
        NormalStoredProcedureResultSet resultSet;
        var procedure = new NormalStoredProcedure(parameters);
        var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        // ACT
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            resultSet = connection.ExecuteStoredProcedure(procedure);
        }
        var results = resultSet.RecordSet1;
        var result = results.First();

        // ASSERT
        Assert.AreEqual(expectedId, result.Id);
        Assert.AreEqual(expectedName, result.Name);
        Assert.AreEqual(expectedActive, result.Active);
    }

### Multiple RecordSet Stored Procedure
    
Next up we will look at the calling code for a stored procedure which returns *Multiple RecordSets*. This is basically the same as the example above except when interrogating the *ResultSet* there are more RecordSets to drill into. We will stick with the example defined [earlier in the document](#a-stored-procedure-with-multiple-recordsets)

    [TestClass]
    public class MultipleRecordSetTests
    {
        [TestMethod]
        public void EXAMPLE_MultipleRecordSetStoredProcedure_WithThreeSelects_ReturnsThreeRecordSets()
        {
            // ARRANGE
            const int expectedId = 10;
            const string expectedName = "Sid";
            const bool expectedActive = true;
            const decimal expectedPrice = 10.99M;
            Guid expectedUniqueIdentifier = Guid.NewGuid();
            const byte expectedCount = 17;
            var parameters = new MultipleRecordSetStoredProcedureParameters
            {
                Id = expectedId,
                Name = expectedName,
                Active = expectedActive,
                Price = expectedPrice,
                UniqueIdentifier = expectedUniqueIdentifier,
                Count = expectedCount
            };
            MultipleRecordSetStoredProcedureResultSet resultSet;
            var procedure = new MultipleRecordSetStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                resultSet = connection.ExecuteStoredProcedure(procedure);
            }
            var results1 = resultSet.RecordSet1;
            var result1 = results1.First();

            var results2 = resultSet.RecordSet2;
            var result2 = results2.First();

            var results3 = resultSet.RecordSet3;
            var result3 = results3.First();

            // ASSERT
            Assert.AreEqual(expectedId, result1.Id);
            Assert.AreEqual(expectedName, result1.Name);

            Assert.AreEqual(expectedActive, result2.Active);
            Assert.AreEqual(expectedPrice, result2.Price);

            Assert.AreEqual(expectedUniqueIdentifier, result3.UniqueIdentifier);
            Assert.AreEqual(expectedCount, result3.Count);
        }
    }
    
We can see from this test we have access to each *RecordSet* and the records within them.
   
## Calling the Stored Procedures from Code using DbContext
If you are already using Entity Framework in your solution you may wish to call the stored procedure direct from the DbContext. Providing you import a reference to the *Dibware.StoredProcedureFrameworkForEF* DLL you can use the extension method that DLL provides directly on DbContext object. The example code below shows how we can use the extension method on the **DbContext** object to execute the stored procedure. The code is basically the same when called on the **SqlConnection** or the **DbConnection**.

    [TestMethod]
    public void EXAMPLE_ExecuteMostBasicStoredProcedureOnDbConext()
    {
        // ARRANGE
        var procedure = new MostBasicStoredProcedure();
        var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        // ACT
        Context.ExecuteStoredProcedure(procedure);
        
        // ASSERT
        // Should get here as Exception should not have been thrown
    }
        
The *Context* in this test inherits from an entity Framework **DbContext** so I can execute the stored procedure by calling **Context.ExecuteStoredProcedure(...)** passing in the instantiated stored procedure object. 

Alternatively if you can also call the stored procedure in a more entity framework code first method, like so.

    MyContext.MyStoredProcedure.Execute();

But to do this we have to change the base classes which the store procedures inherit from and use the base classes from the `Dibware.StoredProcedureFrameworkForEF` assembly rather than the ones in the `Dibware.StoredProcedureFramework`. The EF specific base classes are as follows:

* StoredProcedureBaseForEF
* NoParametersNoReturnTypeStoredProcedureBaseForEF
* NoParametersStoredProcedureBaseForEF
* NoReturnTypeStoredProcedureBaseForEF

So if we change our most basic stored procedure to inherit from `NoParametersNoReturnTypeStoredProcedureBaseForEF` like so...

    internal class MostBasicStoredProcedureForEF
        : NoParametersNoReturnTypeStoredProcedureBaseForEF
    {
        public MostBasicStoredProcedureForEF(DbContext context)
            : base(context)
        {}
    }

And we create a property for it on our database context, and then initialise the stored procedure with the instance of the context in the context constructor, like so...

    internal class IntegrationTestContext : DbContext
    {
        #region Stored Procedures

        public MostBasicStoredProcedureForEF MostBasicStoredProcedure { get; private set; }

        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public IntegrationTestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new CreateDatabaseIfNotExists<IntegrationTestContext>();
            Database.SetInitializer(databaseInitializer);

            MostBasicStoredProcedure = new MostBasicStoredProcedureForEF(this);
        }
        
    }
    
Then we can execute the stored procedure via the new property like so...

    var context = new IntegrationTestContext("MyDatabaseConnectionName");
    context.MostBasicStoredProcedure.Execute();

## TBC... As need to add method to set parameters for this kind of calling code!