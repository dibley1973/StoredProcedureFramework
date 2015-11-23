# Using the StoredProcedureFramework
The purpose of this document is to describe how to use the Stored Procedure Framework for .Net. 

PLEASE NOTE: THIS DOCUMENT IS STILL BEING UPDATED, and may be in accurate due to a change in API an some functionality. Please refer to the unit tests and examples in the code for the true usage documentation.

Please also note there is on-going work to split the current single TEST project into three specific projects:
* Dibware.StoredProcedureFramework.UnitTests
* Dibware.StoredProcedureFramework.IntegrationTests
* Dibware.StoredProcedureFramework.Examples

There will then be two database projects
* Dibware.StoredProcedureFramework.IntegrationTests.Database
* Dibware.StoredProcedureFramework.Examples.Database

## TOC
* [Representing Stored Procedures in Code] (#representing-stored-procedures-in-code)
  +  [General Rules] (#general-rules)
    -   [Base Classes] (#base-classes)
      * StoredProcedureBase
      * NoParametersStoredProcedureBase
      * NoReturnTypeStoredProcedureBase
      * NoParametersNoReturnTypeStoredProcedureBase
    - [Type Parameters] (#type-parameters)
      * TReturn
      * TParameters
    - [Constructors] (#constructors)
      * ctor()
      * ctor(string procedureName)
      * ctor(string schemaName, string procedureName)
      * ctor(TParameters parameters)
      * ctor(string procedureName, TParameters parameters)
      * ctor(string schemaName, string procedureName, TParameters parameters)
     - [Stored Procedure Attributes] (#stored-procedure-attributes)
* [Example Usage] (#examples-usage)
  + The most basic type of stored procedure
  + A Stored Procedure without Parameters
  + A Stored Procedure with Parameters but without a Return Type
  + A "Normal" Stored procedure
  + A Stored Procedure With Multiple RecordSets
  + [A Stored Procedure with Table Value Parameters] (#a-stored-procedure-with-table-value-parameters)
* [Calling the Stored Procedures from Code using SqlConnection](#calling-the-stored-procedures-from-code-using-sqlconnection)
* [Calling the Stored Procedures from Code using DbContext](#calling-the-stored-procedures-from-code-using-dbcontext)

## Representing Stored Procedures in Code
The aim of this framewor is to allow representing of stored procedures, their parameters and return types, as .Net POCO objects. These objects can then be executed against the target dataabse using either the SqlConnection, dbConnection or DbContext objects. (All code examples can be found in the **Dibware.StoredProcedureFramework.Examples** project.

### General Rules
To represent a Stored Procedure when using the StoredProcedureFramework we need to create a **P**lain **O**ld **C**LR **O**bject class for it. The stored procedure class must inherit from one of a predetermined number of base classes. These base classes all exists in the **Dibware.StoredProcedureFramework.Base** *namespace* of the framework. In most cases the class should inherit from the **StoredProcedureBase** base class, but three other *shortcut* base class are also provided for added convenience.

#### Base Classes
There are four base classes from which a stored procedure object class must inherit from.
* StoredProcedureBase - for Stored Procedures with parameters and return types
* NoParametersStoredProcedureBase - for Stored Procedures with return types but no parameters
* NoReturnTypeStoredProcedureBase - for Stored procedures with parameters but no return types
* NoParametersNoReturnTypeStoredProcedureBase - for Stored Procedures without parameters or returns types

##### StoredProcedureBase
The **StoredProcedureBase** base class which exists in the `Dibware.StoredProcedureFramework.Base` namespace is probably the most frequently used base class as it is for a stored procedure which takes parameters and also returns a result. The result can be either a *ResultSet* for multiple RecordSets or a list of return types for a single RecordSet. This base class will typically be used for your the object which represents your *GetProductForId* type of stored procedures. When in herited from this base class demands two *Type Parameters* to be defined; **TReturn** and **TParameters**.

##### NoParametersStoredProcedureBase
The **NoParametersStoredProcedureBase** base class is used for a stored procedure which does not have parameters but does return a result. Again this result can be either a  *ResultSet* for multiple RecordSets or a list of return types for a single RecordSet. This base class will typically be inherited from for your *GetAllProducts* type of stored procedures and when inherited from this class demands one *Type Parameter* to be defined; **TReturn**.

##### NoReturnTypeStoredProcedureBase
The **NoReturnTypeStoredProcedureBase** base class is used for a stored procedure which does have one or more parameters but does not return any results. This will typically be inherited from for  your *DeleteProductById* type of stored procedures, and when inherited from this class demands one *Type Parameter* to be defined; **TParameters**.

#####NoParametersNoReturnTypeStoredProcedureBase
The **NoParametersNoReturnTypeStoredProcedureBase** base class is used for a stored procedure which do not have any parameters and also do not return any results. This will be used for your *RunHouseKeepingBatch* type of stored procedures and when inherited from does not demand any *Type Parameters* to be defined.

#### Type Parameters
Typically when defining the stored procedure POCO class there will be a requirement to define one or both of the following *Type Parameters*. 
* TReturn
* TParameters

##### TReturn
The `TReturn` type parameter defines the type that represents the result returned from a stored procedure. This can be either a list of objects where each object defines the signature of the *row* that is returned in the case of a single RecordSet, or a *ResultSet* object for a SQL stored procedure which returns *Multiple RecordSets*. The *ResultSet* must contain one or more properties which are lists of return types, where each represents one of the *RecordSets*. Each list contains an object whhich represents the signature of the row being returned. The order which each property representing the *RecordSets* is defined in the class must match the order the SQL Stored procedure returns the RecordSets. 

For simplicilty the classes that define the row signature has a matching name and data type of the column which is returned from the SQL stored procedure *RecordSet*. If this cannot be acheived due to a reserved word for instance that the framework provides attributes which can be used to override the *Name* or *DataType* of the field. Size, Scale and Precision can also be set using the attributes the framework provides. See the section for [Stored Procedure Attributes] (#stored-procedure-attributes) for further details.

##### TParameters
The `TParameters` type parameter defines the type that represents the collection of parameters which the stored procedure requires. The order in which the parameters exist in the SQL stored procedure is the order which they must be defined in the POCO class.

For simplicilty the classes that define the parameter signature has a matching name and data type of the paremeter which the SQL stored procedure *RecordSet* requires. If this cannot be acheived due to a reserved word for instance that the framework provides attributes which can be used to override the *Name* or *DataType* of the parameter. Direction, Size, Scale and Precision can also be set using the attributes the framework provides. See the section for [Stored Procedure Attributes] (#stored-procedure-attributes) for further details.

#### Constructors
The stored procedure POCO class will also require one of six constructor signatures, depending upon the type of procedure:

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

#### Stored Procedure Attributes
There are a number of attributes which the stored procedure framework provids which can be used to override the conventions which the framework uses.

* DirectionAttribute
* NameAttribute
* ParameterDbTypeAttribute
* PrecisionAttribute
* ScaleAttribute
* SchemaAttribute
* SizeAttribute

##### DirectionAttribute
This attribute can be applied to a property which defines a SQL stored procedure parameter and used to overrride the default *Input Only* behaviour of the parameters. The attribute is constructed with a `System.data.ParameterDirection` enumeration.

##### NameAttribute
This attribute can be applied to a class, struct, or property to override the name which the framework will use by convention fot the object. This attribute can be used to override the name of a stored procedure, a parameter or a return type field. The attribute is constructed with the overriding name.

##### ParameterDbTypeAttribute
This attribute can be applied to a property to override the SqlDbType of a parameter or a retutn type field. The attribute is constructed with a `System.Data.SqlDbType`.

##### PrecisionAttribute
This attribute can be applied to a property to overdide the default precision of `Decimal`, `Numeric`, `Money`, `SmallMoney` data types. The attribute is constructed with a `System.Byte`.

##### ScaleAttribute
This attribute can be applied to a property to overide the default scale of `Decimal`, `Numeric`, `Money`, `SmallMoney` data types. The attribute is constructed with a `System.Byte`.

##### SchemaAttribute
This attribute can be applied to a class, struct, or property to override the default schema of *dbo* which the framework will assume for a stored procedure. The attribute is constructed with the overriding schema name.

##### SizeAttribute
This attribute can be applied to a property to overdide the default size of a text or binary types of parameter or field. The attribute is constructed with a `System.Int32`.

## Example Usage
(Once complete) all of the code in the examples listed below will exist in the following projects:
* Dibware.StoredProcedureFramework.Examples.csproj
* Dibware.StoredProcedureFramework.Examples.Database.sqlproj

### The most basic type of stored procedure
The most basic type of stored procedure is one that has no parameters and returns no result. For example a stored procedure that just performs an action like resetting a field value, but does not take any parameters and does not return any results, it uses maybe a configuration table or function in the database. For example the procedure below which resets the *LastUpdatedDateTime* field on the *Account* table.

    CREATE PROCEDURE [dbo].[AccountLastUpdatedDateTimeReset] 
    AS
    BEGIN
        UPDATE
            [app].[Account]
        SET
            [LastUpdatedDateTime] = GETDATE();
    END

So to call this stored procedure using teh framework we need a class to represent this stored procedure, `AccountLastUpdatedDateTimeReset`. so teh framework knows how to use this class it must inherit from the `StoredProcedureBase` abstract class. This is the base class which the framework expects **all** stored procedure POCO classes to inherit from. The `StoredProcedureBase` base class expects two type parameters to be defined for it). 

    public abstract class StoredProcedureBase<TReturn, TParameters> {...}

If we wish to inherit from this class, *which we must for the framework to function correctly*, then we must provide a class for each type parameter. The *TReturn* type parameter defines the type of the which the stored procedure is to return and the *TParameters* type parameter defines a class for the stored procedure parameters.  As our procedure neither returns any values or takes any parameters we need to explicitly state this. The framework already provides us with concrete classes that can be used when there is no return type and or no parameter type. These both exist in the **Dibware.StoredProcedureFramework** namespace and are the `NullStoredProcedureResult` and `NullStoredProcedureParameters` classes:

#### NullStoredProcedureResult
This class is used when the procedure will not return any kind of result.

    /// <summary>
    /// An object that represents the absence of an 
    /// expected result from a stored procedure
    /// </summary>
    public class NullStoredProcedureResult
    {
    }

#### NullStoredProcedureParameters
This class is used when the stored procedure does not require any parameters.

    /// <summary>
    /// An object that represents the absence of parameters
    /// for a stored procedure
    /// </summary>
    public class NullStoredProcedureParameters
    {
    }

So we could define the class that represents this stored procedure as follows...

    internal class AccountLastUpdatedDateTimeReset
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        public AccountLastUpdatedDateTimeReset()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

...but this is a bit cumbersome for such a basic stored procedure. Having to define the "Null" return type and "Null" parameters is a bit clumsey, so the framework provides another abstract base class **NoParametersNoReturnTypeStoredProcedureBase** which our stored procedure class can inherit from which does this for us and makes our code a little more succinct. Now we can define the class like below:

    internal class AccountLastUpdatedDateTimeReset
        : NoParametersNoReturnTypeStoredProcedureBase
    {
    }

We do not need to provide a constructor as the **NoParametersNoReturnTypeStoredProcedureBase** already handles this for us in its default constructor. We can call the procedure using the code given in teh test below. Please note the `SqlConnectionExampleTestBase` base class just sets up the SqlConnection for teh test and handles opening and closing of the SqlConnection for us.

    [TestClass]
    public class StoredProcedureWithoutParametersOrReturnType
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void AccountLastUpdatedDateTimeReset()
        {
            // ARRANGE
            var procedure = new AccountLastUpdatedDateTimeReset();

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Nothing to assert
        }
    }

So to call the procedure we first create a new instance of the stored procedure POCO object, and then we pass that to the `ExecuteStoredProcedure` extension method of the `SqlConnection` object. No results are expected so none are gathered.


### A Stored Procedure without Parameters
The next stored procedure to look at is one which returns a result but does not have any parameters. This would typically be used for your *MyTable_GetAll* type of stored procedure, so for this example we will use a stored procedure which returns all tenants from the `Tenant` table in the `app` schema:

    CREATE PROCEDURE [app].[TenantGetAll]
    AS
    BEGIN
        -- Insert statements for procedure here
        SELECT      [TenantId]
        ,           [IsActive]
        ,           [TenantName]
        ,           [RecordCreatedDateTime]
        FROM        [app].[Tenant];
    END

For this example we will assume we have already created the table `app.Tenant`...
    CREATE TABLE [app].[Tenant] (
        [TenantId]              INT            IDENTITY (1, 1) NOT NULL,
        [IsActive]              BIT            NOT NULL,
        [TenantName]            NVARCHAR (100) NULL,
        [RecordCreatedDateTime] DATETIME       NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_app.Tenant] PRIMARY KEY CLUSTERED ([TenantId] ASC)
    );

... and seeded the table with the following data:
    INSERT INTO [app].[Tenant] ( [IsActive], [TenantName] ) VALUES ( 1, 'Acme Tenant' )
    INSERT INTO [app].[Tenant] ( [IsActive], [TenantName] ) VALUES ( 1, 'Universal Tenant')    

As this procedure returns data we need to define a class that will represent a row of data in our result *RecordSet*. For each field in the *RecordSet* we need a property in this class to represent it. The property **should** match the *Name* and *DataType* of the field it represents in the *RecordSet* row returned. Remember that StoredProcedureAttributes can be used to override both the *Name* and *DataType* if required, but in this case we will ensure they match. So in the example case of the `TenantGetAll` stored procedure we are looking at a class which contains properties of the names and types we want to return, as below.

    /// <summary>
    /// Encapsulates tenant data
    /// </summary>
    internal class TenantDto
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public string TenantName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }

We will use a DTO as this is likely to be the same object which transports our returned data up through the layers to the client, domain or business logic layer. In our example the DTO is be defined in the Example project which is akin to a DataAccess layer, however in a real world scenario the DTO may be defined in your services layer or elsewhere. Now we have a class which represents our return type we can build a class to represent our stored procedure.

    [Schema("app")]
    internal class TenantGetAll
        : NoParametersStoredProcedureBase<List<TenantDto>>
    {
    }

The first thing you may notice is the `Schema` attribute which this class has been decorated with. That informs the framework that the stored procedure exists in the *app* schema not the *dbo* schema. By default the the framework anticipates all stored procedures are in the *dbo* schema, by using the `SchemaAttribuute` to override teh default the stored procedures can acccessed in any schema. The next point you may observe is the class inherits from `NoParametersStoredProcedureBase<TReturn>`. It could just as easily inherit from `StoredProcedureBase<TReturn, NullStoredProcedureParameters>` but the `NoParametersStoredProcedureBase` base class is a *short-cut* base class to save defining an extra type parameter. As this stored procedure requires no parameters and we are using the `NoParametersStoredProcedureBase` base class we do not need to provide an explicit constructor with no parameters as the base class will handle this for us. The `TReturn` type parameter is needed and in this case is our list of `TenantDto`.

We can call the stored procedure using the extensions on SqlConnection object like so:    
    
    [TestMethod]
    public void TenantGetAll()
    {
        // ARRANGE
        var procedure = new TenantGetAll();
        const int expectedTenantCount = 2;

        // ACT
        List<TenantDto> tenants = Connection.ExecuteStoredProcedure(procedure);
        TenantDto tenant1 = tenants.FirstOrDefault();

        // ASSERT
        Assert.AreEqual(expectedTenantCount, tenants.Count);
        Assert.IsNotNull(tenant1);
    } 
    
First we create an instance of the stored procedure POCO object, and then we pass that to the `ExecuteStoredProcedure` extension method of the `SqlConnection` object. We are expecting results this time so we can gather them from the results of the `ExecuteStoredProcedure` method call. They will be a list of our `TenantDto` so we can access them like any normal list.
 
### A Stored Procedure with Parameters but without a Return Type
The next type of stored procedure in complexity is a procedure that takes parameters but does not return a result set. For example a **TableNameDeleteForId** stored procedure. So taking the example stored procedure below which deletes a records from the `Company` table based upon the `TenantId`.

    CREATE PROCEDURE [app].[CompanyDeleteForTenantId]
    (
        @TenantId INT
    )
    AS
    BEGIN
       -- Insert statements for procedure here
        DELETE FROM
            app.Company
        WHERE
            TenantId = @TenantId;
    END

First we need a class that represents the stored procedure parameters, `TenantIdParameters`. 

    internal class StoredProcedureWithParametersButNoReturnParameters
    {
        public int Id { get; set; }
    }

The default parameter data type is a *string* / *VarChar* combination. By convention because the paraemeter type is an `int` the framework will deteremine that the parameter type is an integer **SqlDBType**. Our procedure only has a single parameter, but if the Stored procedure had multiple parameters then there would be multiple properties representing each one, defined in teh same order in the class as they are in teh SQL stored procedure.
    
Now we can define the class which will represent the stored procedure and will use the parameters type define above as the **TParameters** type parameter. We also now need a constructor which takes a parameters argument of `TenantIdParameters`. This just passes straight through to the base class which handles construction tasks.

    [Schema("app")]
    internal class CompanyDeleteForTenantId
        : StoredProcedureBase<NullStoredProcedureResult, TenantIdParameters>
    {
        public CompanyDeleteForTenantId(TenantIdParameters parameters)
            : base(parameters)
        {
        }
    }
    
 You will notice that we have to define the **TReturn** type parameter as **NullStoredProcedureResult** as no results are expected to be returned form the stored procedure when it is called. The framework lets us short cut the definition slightly by omitting the **NullStoredProcedureResult** as the **TReturn** return type parameter and using another base class, the **NoReturnTypeStoredProcedureBase** class. We can inherit from this class like so: 
    
    [Schema("app")]
    internal class CompanyDeleteForTenantId
        : NoReturnTypeStoredProcedureBase<TenantIdParameters>
    {
        public CompanyDeleteForTenantId(TenantIdParameters parameters)
            : base(parameters)
        {
        }
    }
    
This now brings us on nicely to what I call the "normal" and probably the most common type of stored procedure; one that takes parameters **and** returns a result set.

### A "Normal" Stored procedure
This represents what I consider to be the bread-and-butter of stored procedures; a procedure that has only input parameters, no output parameters, and returns a single result set. It might be the "GetMeSomethingById" type of stored procedure. So taking the stored procedure below which gets all companies for the specified TenantId...

    CREATE PROCEDURE [app].[CompanyGetAllForTenantID]
    (
        @TenantId INT
    )
    AS
    BEGIN
           -- SET NOCOUNT ON added to prevent extra result sets from
           -- interfering with SELECT statements.
           SET NOCOUNT ON;

        -- Insert statements for procedure here
           SELECT 
            CompanyId
        ,   TenantId
        ,   IsActive
        ,   CompanyName
        ,   RecordCreatedDateTime
        FROM
            app.Company
        WHERE
            TenantId = @TenantId;
    END

So again we need a class which represents all of our parameters, and for this stored procedure we can reuse the `TenantIdParameters` from earlier which has teh correctly set up *TenantId* parameter.

    public class TenantIdParameters
    {
        public int TenantId { get; set; }
    }

Once the parameters class is defined we need to create a class that represents the row from our the first and only RecordSet that we will return. We will use the `CompanyDto` so it can be passed to another layer as a data transfer object if need be.

    /// <summary>
    /// Encapsulates compnay data
    /// </summary>
    internal class CompanyDto
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }
    
Once we have those two classes defined we can define a class that represents the stored procedure which pulls them altogether, `CompanyGetAllForTenantID`. This class inherits from the *StoredProcedureBase* and take the *List<CompanyDto>* as the *TReturn* type parameter, and the *TenantIdParameters* as the TParameters* Type pararmeter. We also need to provide a "pass-through" constructor with a argument which is of the same type as "NormalStoredProcedureParameters" class. 

    [Schema("app")]
    internal class CompanyGetAllForTenantID
        : StoredProcedureBase<List<CompanyDto>, TenantIdParameters>
    {
        public CompanyGetAllForTenantID(TenantIdParameters parameters)
            : base(parameters)
        {
        }
    }

 We can call the procedure using the example code below. First we create an instance of our parameters POCO and set the parameter value. Then we create an instance of our stored procedure POCO using the parameters. We then call the procedure using the `ExecuteStoredProcedure` extension method on the `SqlConnection` and collect the results into a list of `CompanyDto`s.
 
        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyGetAllForTenantID(parameters);
            const int expectedCompanyCount = 2;

            // ACT
            List<CompanyDto>  companies = Connection.ExecuteStoredProcedure(procedure);
            CompanyDto company1 = companies.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }   

    
### WORK IN PROGRESS BELOW 
### WORK IN PROGRESS BELOW
### WORK IN PROGRESS BELOW


    
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

### A Stored Procedure with Table Value Parameters
Table Value Parameters was introduced in to SQL Server in SQL Server 2008. This framework can call stored procedures with table value parameters with only a small amount of extra code. so lets look at the stored procedure below...

    CREATE PROCEDURE [app].[CompaniesAdd]
    (
        @Companies [app].[CompanyTableType] READONLY
    )
    AS
    BEGIN
        MERGE INTO [app].[Company] AS [target]
        USING   @Companies AS [source]
        ON      [target].[TenantId] = [source].[TenantId]
        AND     [target].[CompanyName] = [source].[CompanyName]
        WHEN MATCHED THEN UPDATE SET 
            [target].[IsActive] = [source].[IsActive]
        WHEN NOT MATCHED THEN INSERT VALUES
        (
            [source].[TenantId]
        ,   [source].[IsActive]
        ,   [source].[CompanyName]
        ,   GETDATE()
        );
    END

... which uses the table User Defined Type...

    CREATE TYPE [app].[CompanyTableType] AS TABLE (
        [TenantId]    INT            NOT NULL,
        [IsActive]    BIT            NOT NULL,
        [CompanyName] NVARCHAR (100) NULL);

... as the table value parameter we can represent this in code for the framework using the `CompaniesAdd` class and associated nested classes below. 

    [Schema("app")]
    internal class CompaniesAdd
        : NoReturnTypeStoredProcedureBase<CompaniesAdd.CompaniesAddParameters>
    {
        public CompaniesAdd(CompaniesAddParameters parameters)
            : base(parameters)
        {
        }
        internal class CompaniesAddParameters
        {
            [ParameterDbType(SqlDbType.Structured)]
            public List<CompanyTableType> Companies { get; set; }
        }

        internal class CompanyTableType
        {
            public int TenantId { get; set; }
            public bool IsActive { get; set; }
            public string CompanyName { get; set; }
        }
    }

The two internal classes represent the the parameters and the table type. We do not need to make these two classes internal, but by doing this it helps identify their purpose. The `CompanyTableType` represents the *User Defined Type* and `CompaniesAddParameters` class, the parameters. Note that the parameters class has a property which is a list of our table type. The stored procedure can be called using the framework like below:

    [TestMethod]
    public void CompaniesAdd()
    {
        // ARRANGE
        var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
        {
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
        };
        var parameters = new CompaniesAdd.CompaniesAddParameters
        {
            Companies = companiesToAdd
        };
        var procedure = new CompaniesAdd(parameters);

        // ACT
        Connection.ExecuteStoredProcedure(procedure);

        // ASSERT
    }

first we create a new list of the table type and populate this list with the values we want to pass to the stored procedure. We then create the parameters object setting the companies property to be our list. Next we construct the stored procedure using the parameters, and finally execute the procedure against the current SqlConnection (Or DbContext).

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


## Please note... The code below for calling Stored Procedures in a more "EF-friendly"  way is still experimental and may be subject to change as the framework matures. Please be aware of that if you choose this method of calling stored procedures.

Alternatively if you can also call the stored procedure in a more *entity framework code first kind of approach*, so they appear more like DBSet properties on your custom DbContext object, like so:

    MyContext.MyStoredProcedure.Execute();

But to do this we have to change the base classes which the store procedures inherit from and use the base classes from the `Dibware.StoredProcedureFrameworkForEF` assembly rather than the ones in the `Dibware.StoredProcedureFramework`. The EF specific base classes are as follows:

* StoredProcedureBaseForEf
* NoParametersNoReturnTypeStoredProcedureBaseForEf
* NoParametersStoredProcedureBaseForEf
* NoReturnTypeStoredProcedureBaseForEf

So if we change our most basic stored procedure to inherit from `NoParametersNoReturnTypeStoredProcedureBaseForEf` like so...

    internal class MostBasicStoredProcedureForEf
        : NoParametersNoReturnTypeStoredProcedureBaseForEf
    {
        public MostBasicStoredProcedureForEf(DbContext context)
            : base(context)
        {}
    }

and our normal stored procedure to inherit from `StoredProcedureBaseForEf` like so..

    internal class NormalStoredProcedureForEf
        : StoredProcedureBaseForEf<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedureForEf(DbContext context)
            : base(context, null)
        {
        }
    }
    
    
Then if we create a properties for the procedures on our database context, and initialise any properties that are stored procedure in the context constructor using the call to `InitializeStoredProcedureProperties`, like so...

    internal class IntegrationTestContext : DbContext
    {
        #region Stored Procedures

        public MostBasicStoredProcedureForEf MostBasicStoredProcedure { get; private set; }
        public MostBasicStoredProcedureForEf NormalStoredProcedure { get; private set; }

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

            // Instantiate all of the Stored procedure properties
            this.InitializeStoredProcedureProperties();
        }   
    }
    
Alternately you can make sure your application DbContext inherits from the stored procedure framework's `StoredProcedureDbContext` which is in the `Dibware.StoredProcedureFrameworkForEF` namespace and this will automatically call the `InitializeStoredProcedureProperties` extension method on `DbContext` from it's constructor so you don't have to.

    internal class ExampleTestDbContext : StoredProcedureDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleTestDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public ExampleTestDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<ExampleTestDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<ExampleTestDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this os carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
        }
    }    
    
... we can then execute the stored procedures via the new properties like this for stored procedures without parameters...

    var context = new IntegrationTestContext("MyDatabaseConnectionName");
    context.MostBasicStoredProcedure.Execute();
    
or like this for stored procedures with parameters...

    var context = new IntegrationTestContext("MyDatabaseConnectionName");
    var parameters = new NormalStoredProcedureParameters
    {
        Id = 1
    };
    context.NormalStoredProcedure.ExecuteFor( parameters)
    
or if you would prefer you can *inline* the parameters like this...

    var context = new IntegrationTestContext("MyDatabaseConnectionName");
    context.NormalStoredProcedure.ExecuteFor( new NormalStoredProcedureParameters { Id = 1 })

or you can call the procedure with *inline* anonymous parameter like so.

    [TestMethod]
    public void ExecuteFor_WhenPassedAnonymousParameterObject_GetsExpectedResults()
    {
        // ARRANGE
        const int expectedId = 10;
        const string expectedName = @"Dave";
        const bool expectedActive = true;
        NormalStoredProcedureResultSet resultSet;

        // ACT
        resultSet = Context.AnonymousParameterStoredProcedure.ExecuteFor(new { Id = expectedId });
        var results = resultSet.RecordSet1;
        var result = results.First();

        // ASSERT
        Assert.AreEqual(expectedId, result.Id);
        Assert.AreEqual(expectedName, result.Name);
        Assert.AreEqual(expectedActive, result.Active);
    }

If you would prefer to uses a more generic API and would like to have your stored procedure properties look more like your DbSet properties, then you can declare them on your custom DbContext like so:

    public StoredProcedure<NormalStoredProcedureResultSet> NormalStoredProcedure { get; private set; }

The stored procedure framework will understand that the stored procedure to be called will match the name of the property. Using the example above the stored procedure will call a stored procedure called "NormalStoredProcedure" in the "dbo" schema. If you wish to override the name you can use the `NameAttribute` like so...

    [Name("NormalStoredProcedure")]
    public StoredProcedure<NormalStoredProcedureResultSet> NormalStoredProcedure2 { get; private set; }

That is also the process used to override the schema using the `SchemaAttribute`. 
    
and call it just the same as the rest like so:

    [TestMethod]
    public void ExecuteForWithGenericstoredProce_WhenPassedConstructedParameters_GetsExpectedResults()
    {
        // ARRANGE
        const int expectedId = 10;
        const string expectedName = @"Dave";
        const bool expectedActive = true;
        NormalStoredProcedureResultSet resultSet;

        var parameters = new NormalStoredProcedureParameters
        {
            Id = expectedId
        };

        // ACT
        resultSet = Context.NormalStoredProcedure2.ExecuteFor(parameters);
        var results = resultSet.RecordSet1;
        var result = results.First();

        // ASSERT
        Assert.AreEqual(expectedId, result.Id);
        Assert.AreEqual(expectedName, result.Name);
        Assert.AreEqual(expectedActive, result.Active);
    }
  
        
**PLEASE NOTE** I have only just started *flirting* with the API for using anonymous parameters. I do not have yet any complex tests to validate the code is fully bug free. Other tests continue to pass so I don't anticipate that the new API option has broken the exiting API.
    
### Using Transactions
W.I.P.