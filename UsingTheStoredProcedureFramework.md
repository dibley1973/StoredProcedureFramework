# Using the StoredProcedureFramework
The purpose of this document is to describe how to use the Stored Procedure Framework for .Net. 

## Table of Contents
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
* [Example Usage] (#example-usage)
  + [The most basic type of stored procedure] (#the-most-basic-type-of-stored-procedure)
  + [A Stored Procedure without Parameters] (#a-stored-procedure-without-parameters)
  + [A Stored Procedure with Parameters but without a Return Type] (#a-stored-procedure-with-parameters-but-without-a-return-type)
  + [A "Normal" Stored procedure](#a-normal-stored-procedure)
  + [A Stored Procedure With Multiple RecordSets]  (#a-stored-procedure-with-multiple-recordsets)
  + [A Stored Procedure with Table Value Parameters] (#a-stored-procedure-with-table-value-parameters)
* [Calling the Stored Procedures from Code] (#calling-the-stored-procedures-from-code)
  + [Calling the Stored Procedures from Code using SqlConnection](#calling-the-stored-procedures-from-code-using-sqlconnection)
    - [Using Transactions with SqlConnection] (#using-transactions-with-sqlconnection)
  + [Calling the Stored Procedures from Code using DbContext] (#calling-the-stored-procedures-from-code-using-dbcontext)
    - [Calling the Stored Procedures from Code using DbContext in traditional way] (#calling-the-stored-procedures-from-code-using-dbcontext-in-traditional-way)
    - [Calling the Stored Procedures from Code using DbContext in Simplified API way](#calling-the-stored-procedures-from-code-using-dbcontext-in-simplified-api-way)
    - [Calling the Stored Procedures from Code using DbContext in Simplified API with In-Line Declaration] (#calling-the-stored-procedures-from-code-using-dbcontext-in-simplified-api-with-in-line-declaration)
    - [Using Transactions with DbContext] (#using-transactions-with-dbcontext)

## Representing Stored Procedures in Code
The aim of this framework is to allow representing of stored procedures, their parameters and return types, as .Net POCO objects. These objects can then be executed against the target database using either the SqlConnection, dbConnection or DbContext objects. (All code examples can be found in the **Dibware.StoredProcedureFramework.Examples** project.

### General Rules
To represent a Stored Procedure when using the StoredProcedureFramework we need to create a **P**lain **O**ld **C**LR **O**bject class for it. The stored procedure class must inherit from one of a predetermined number of base classes. These base classes all exists in the **Dibware.StoredProcedureFramework.Base** *namespace* of the framework. In most cases the class should inherit from the **StoredProcedureBase** base class, but three other *shortcut* base class are also provided for added convenience.

#### Base Classes
There are four base classes from which a stored procedure object class must inherit from.
* StoredProcedureBase - for Stored Procedures with parameters and return types
* NoParametersStoredProcedureBase - for Stored Procedures with return types but no parameters
* NoReturnTypeStoredProcedureBase - for Stored procedures with parameters but no return types
* NoParametersNoReturnTypeStoredProcedureBase - for Stored Procedures without parameters or returns types

##### StoredProcedureBase
The **StoredProcedureBase** base class which exists in the `Dibware.StoredProcedureFramework.Base` namespace is probably the most frequently used base class as it is for a stored procedure which takes parameters and also returns a result. The result can be either a *ResultSet* for multiple RecordSets or a list of return types for a single RecordSet. This base class will typically be used for your the object which represents your *GetProductForId* type of stored procedures. When inherited from this base class demands two *Type Parameters* to be defined; **TReturn** and **TParameters**.

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
The `TReturn` type parameter defines the type that represents the result returned from a stored procedure. This can be either a list of objects where each object defines the signature of the *row* that is returned in the case of a single RecordSet, or a *ResultSet* object for a SQL stored procedure which returns *Multiple RecordSets*. The *ResultSet* must contain one or more properties which are lists of return types, where each represents one of the *RecordSets*. Each list contains an object which represents the signature of the row being returned. The order which each property representing the *RecordSets* is defined in the class must match the order the SQL Stored procedure returns the RecordSets. 

For simplicity the classes that define the row signature has a matching name and data type of the column which is returned from the SQL stored procedure *RecordSet*. If this cannot be achieved due to a reserved word for instance that the framework provides attributes which can be used to override the *Name* or *DataType* of the field. Size, Scale and Precision can also be set using the attributes the framework provides. See the section for [Stored Procedure Attributes] (#stored-procedure-attributes) for further details.

##### TParameters
The `TParameters` type parameter defines the type that represents the collection of parameters which the stored procedure requires. The order in which the parameters exist in the SQL stored procedure is the order which they must be defined in the POCO class.

For simplicilty the classes that define the parameter signature has a matching name and data type of the parameter which the SQL stored procedure *RecordSet* requires. If this cannot be acheived due to a reserved word for instance that the framework provides attributes which can be used to override the *Name* or *DataType* of the parameter. Direction, Size, Scale and Precision can also be set using the attributes the framework provides. See the section for [Stored Procedure Attributes] (#stored-procedure-attributes) for further details.

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
There are a number of attributes which the stored procedure framework provides which can be used to override the conventions which the framework uses.

* DirectionAttribute
* NameAttribute
* ParameterDbTypeAttribute
* PrecisionAttribute
* ScaleAttribute
* SchemaAttribute
* SizeAttribute

##### DirectionAttribute
This attribute can be applied to a property which defines a SQL stored procedure parameter and used to override the default *Input Only* behaviour of the parameters. The attribute is constructed with a `System.data.ParameterDirection` enumeration.

##### NameAttribute
This attribute can be applied to a class, struct, or property to override the name which the framework will use by convention for the object. This attribute can be used to override the name of a stored procedure, a parameter or a return type field. The attribute is constructed with the overriding name.

##### ParameterDbTypeAttribute
This attribute can be applied to a property to override the SqlDbType of a parameter or a retutn type field. The attribute is constructed with a `System.Data.SqlDbType`.

##### PrecisionAttribute
This attribute can be applied to a property to override the default precision of `Decimal`, `Numeric`, `Money`, `SmallMoney` data types. The attribute is constructed with a `System.Byte`.

##### ScaleAttribute
This attribute can be applied to a property to override the default scale of `Decimal`, `Numeric`, `Money`, `SmallMoney` data types. The attribute is constructed with a `System.Byte`.

##### SchemaAttribute
This attribute can be applied to a class, struct, or property to override the default schema of *dbo* which the framework will assume for a stored procedure. The attribute is constructed with the overriding schema name.

##### SizeAttribute
This attribute can be applied to a property to override the default size of a text or binary types of parameter or field. The attribute is constructed with a `System.Int32`.

## Example Usage
All of the code in the examples listed below will exist in the following projects:
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

So to call this stored procedure using the framework we need a class to represent this stored procedure, `AccountLastUpdatedDateTimeReset`. So the framework knows how to use this class it must inherit from the `StoredProcedureBase` abstract class. This is the base class which the framework expects **all** stored procedure POCO classes to inherit from. The `StoredProcedureBase` base class expects two type parameters to be defined for it). 

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

...but this is a bit cumbersome for such a basic stored procedure. Having to define the "Null" return type and "Null" parameters is a bit clumsy, so the framework provides another abstract base class **NoParametersNoReturnTypeStoredProcedureBase** which our stored procedure class can inherit from which does this for us and makes our code a little more succinct. Now we can define the class like below:

    internal class AccountLastUpdatedDateTimeReset
        : NoParametersNoReturnTypeStoredProcedureBase
    {
    }

We do not need to provide a constructor as the **NoParametersNoReturnTypeStoredProcedureBase** already handles this for us in its default constructor. We can call the procedure using the code given in the test below. Please note the `SqlConnectionExampleTestBase` base class just sets up the SqlConnection for the test and handles opening and closing of the SqlConnection for us.

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

The first thing you may notice is the `Schema` attribute which this class has been decorated with. That informs the framework that the stored procedure exists in the *app* schema not the *dbo* schema. By default the the framework anticipates all stored procedures are in the *dbo* schema, by using the `SchemaAttribuute` to override the default the stored procedures can accessed in any schema. The next point you may observe is the class inherits from `NoParametersStoredProcedureBase<TReturn>`. It could just as easily inherit from `StoredProcedureBase<TReturn, NullStoredProcedureParameters>` but the `NoParametersStoredProcedureBase` base class is a *short-cut* base class to save defining an extra type parameter. As this stored procedure requires no parameters and we are using the `NoParametersStoredProcedureBase` base class we do not need to provide an explicit constructor with no parameters as the base class will handle this for us. The `TReturn` type parameter is needed and in this case is our list of `TenantDto`.

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

The default parameter data type is a *string* / *VarChar* combination. By convention because the parameter type is an `int` the framework will determine that the parameter type is an integer **SqlDBType**. Our procedure only has a single parameter, but if the Stored procedure had multiple parameters then there would be multiple properties representing each one, defined in the same order in the class as they are in the SQL stored procedure.
    
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

So again we need a class which represents all of our parameters, and for this stored procedure we can reuse the `TenantIdParameters` from earlier which has the correctly set up *TenantId* parameter.

    public class TenantIdParameters
    {
        public int TenantId { get; set; }
    }

Once the parameters class is defined we need to create a class that represents the row from our the first and only RecordSet that we will return. We will use the `CompanyDto` so it can be passed to another layer as a data transfer object if need be.

    /// <summary>
    /// Encapsulates company data
    /// </summary>
    internal class CompanyDto
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }
    
Once we have those two classes defined we can define a class that represents the stored procedure which pulls them altogether, `CompanyGetAllForTenantID`. This class inherits from the *StoredProcedureBase* and take the *List<CompanyDto>* as the *TReturn* type parameter, and the *TenantIdParameters* as the TParameters* Type parameter. We also need to provide a "pass-through" constructor with a argument which is of the same type as "NormalStoredProcedureParameters" class. 

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
   
Now we will look at a variation on this stored procedure, by looking at a stored procedure that returns multiple RecordSets.

### A Stored Procedure With Multiple RecordSets 

So for our *Multiple RecordSet* stored procedure example we will use the following stored procedure which has three recordSets:

    CREATE PROCEDURE app.TenantCompanyAccountGetForTenantId
        @TenantId int
    AS
    BEGIN
        SELECT
            [TenantId]
        ,   [IsActive]
        ,   [TenantName]
        ,   [RecordCreatedDateTime]
        FROM
            [app].[Tenant]
        WHERE
            [TenantId] = @TenantId;

        SELECT 
            [CompanyId]
        ,   [TenantId]
        ,   [IsActive]
        ,   [CompanyName]
        ,   [RecordCreatedDateTime]
        FROM
            [app].[Company]
        WHERE
            [TenantId] = @TenantId;

        SELECT 
            [account].[AccountId]
        ,   [account].[CompanyId]
        ,   [account].[IsActive]
        ,   [account].[AccountName]
        ,   [account].[RecordCreatedDateTime]
        ,   [account].[LastUpdatedDateTime]
        FROM
            [app].[Account] [account]
        INNER JOIN
            [app].[Company] [company]
        ON 
            [company].[CompanyId] = [account].[CompanyId]
        AND
            [company].[TenantId] = @TenantId;

    END

You can see that each *RecordSet* has a different signature. The first returns all columns from the `app.Tenant` table, the second returns all columns from the `app.Company` table, and the last returns all column From the `app.Account` table. So when we create our class that represents the results from this stored procedure it must be capable of handling all three RecordSets. So first we will need an object to represent each of these return row signatures, and these you can see below as DTOs, `TenantDto`, `CompanyDto`, and  `AccountDto`. 

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

    /// <summary>
    /// Encapsulates Company data
    /// </summary>
    internal class CompanyDto
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }

    /// <summary>
    /// Encapsulates Account data
    /// </summary>
    internal class AccountDto
    {
        public int AccountId { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public string AccountName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
    
Now will need a *ResultSet* object to hold each *RecordSet* list of DTOs. The class will contain three properties, each will be a list of DTOs which match the column definitions for each RecordSet the stored procedure is going to return. The order of the properties must match the order which the stored procedure returns the RecordSets; this is a convention which the framework demands. We must also remember to instantiate each *RecordSet* which in this case we will do in the constructor of this object.
    
    internal class TenantCompanyAccountGetForTenantIdResultSet
    {
        public List<TenantDto> Tenants { get; set; }
        public List<CompanyDto> Companies { get; set; }
        public List<AccountDto> Accounts { get; set; }

        public TenantCompanyAccountGetForTenantIdResultSet()
        {
            Tenants = new List<TenantDto>();
            Companies = new List<CompanyDto>();
            Accounts = new List<AccountDto>();
        }
    }

We will need a parameters object for the stored procedure.

    public class TenantIdParameters
    {
        public int TenantId { get; set; }
    }

And finally we need a class to represent the complete Stored Procedure:

    [Schema("app")]
    internal class TenantCompanyAccountGetForTenantId
        : StoredProcedureBase<
            TenantCompanyAccountGetForTenantId.TenantCompanyAccountGetForTenantIdResultSet, 
            TenantIdParameters>
    {

        public TenantCompanyAccountGetForTenantId(TenantIdParameters parameters)
            : base(parameters)
        {}

        internal class TenantCompanyAccountGetForTenantIdResultSet
        {
            public List<TenantDto> Tenants { get; set; }
            public List<CompanyDto> Companies { get; set; }
            public List<AccountDto> Accounts { get; set; }

            public TenantCompanyAccountGetForTenantIdResultSet()
            {
                Tenants = new List<TenantDto>();
                Companies = new List<CompanyDto>();
                Accounts = new List<AccountDto>();
            }
        }
    }

The stored procedure inherits from `StoredProcedureBase<TenantCompanyAccountGetForTenantIdResultSet, TenantIdParameters>` defining the `TReturn` and `TParameters` *Type Parameters* accordingly. We need a constructor which takes a parameter of type `TenantIdParameters`, which passes through the constructor call to the base class. You may notice I have also nested the `TenantCompanyAccountGetForTenantIdResultSet` class within the stored procedure class as it is very strongly tied to and unlikely to be needed outside of the context of **this** stored procedure. We can call this procedure and access the data in the three *RecordSets* as follows.

    [TestMethod]
    public void TenantCompanyAccountGetForTenantId_ReturnsAllThreeRecordSets()
    {
        // ARRANGE
        var parameters = new TenantIdParameters
        {
            TenantId = 1
        };
        var procedure = new TenantCompanyAccountGetForTenantId(parameters);
        TenantCompanyAccountGetForTenantId.TenantCompanyAccountGetForTenantIdResultSet resultSet;
        const int expectedTenantCount = 1;
        const int expectedCompanyCount = 2;
        const int expectedAccountCount = 3000000;

        // ACT
        resultSet = Connection.ExecuteStoredProcedure(procedure);
        List<TenantDto> tenants = resultSet.Tenants;
        List<CompanyDto> companies = resultSet.Companies;
        List<AccountDto> accounts = resultSet.Accounts;

        // ASSERT
        Assert.IsNotNull(tenants);
        Assert.IsNotNull(companies);
        Assert.IsNotNull(accounts);
        Assert.AreEqual(expectedTenantCount, tenants.Count);
        Assert.AreEqual(expectedCompanyCount, companies.Count);
        Assert.AreEqual(expectedAccountCount, accounts.Count);
    }

So again the first task is to instantiate and populate the parameters object, then create an instance of the procedure, constructing it with the parameters. We then execute the stored procedure on the connection, capturing the ResultSet into a variable. From the ResultSet we can retrieve the individual lists of DTOs.

### A Stored Procedure with Table Value Parameters
Table Value Parameters was introduced in to SQL Server in SQL Server 2008. This framework can call stored procedures with table value parameters with only a small amount of extra code. So lets look at the stored procedure below which takes a parameter which is a user defined table type `[app].[CompanyTableType]`.

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

... which uses the following table User Defined Type...

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

The two internal classes represent the the parameters and the table type. We **do not need to make these two classes internal**, but by doing this it helps identify their purpose. The `CompanyTableType` represents the *User Defined Type* and `CompaniesAddParameters` class, the parameters. Note that the parameters class has a property which is a list of our table type. The stored procedure can be called using the framework like below:

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

First we create a new list of the table type object and populate this list with the values we want to pass to the stored procedure. We then create the parameters object setting the companies property to be our list. After that we construct the stored procedure using the parameters, and finally execute the procedure against the current SqlConnection (Or DbContext).

## Calling the Stored Procedures from Code

Now we have created classes to represent the most common types of stored procedures lets now look at how we go about calling these procedures.

The framework provides extension methods which can be used to call the stored procedures on three key .Net data access objects. **SqlConnection**, **DbConnection** and also **DbContext**.  The extension methods for **SqlConnection**, **DbConnection** can be found in the main **Dibware.StoredProcedureFramework** assembly, but for the **DbContext** extensions there is a separate assembly, **Dibware.StoredProcedureFrameworkForEF**. This is to prevent the need for a dependency on **Entity Framework** in the main assembly and hence extra bloat. If your project does not have **Entity Framework** or you are not using the **DbContext** extensions then you don't need a reference to **Dibware.StoredProcedureFrameworkForEF** to call the procedures. 

Regardless of whether you are using *this* stored procedure framework alongside **Entity Framework** or not you will *always* need a reference to the main **Dibware.StoredProcedureFramework** assembly. 

### Calling the Stored Procedures from Code using SqlConnection

So for the purpose of the examples we will call the extension method on the **SqlConnection** object, but the code is basically the same when called on the **DbConnection** or the **DbContext** objects. So lets use the *CompanyGetAllForTenantID* which we defined earlier in this document and call it using the extension method on the SqlConnection Object. 

    [TestMethod]
    public void CompanyGetAllForTenantId()
    {
        // ARRANGE
        const int expectedCompanyCount = 2;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
        var parameters = new TenantIdParameters { TenantId = 1 };
        var procedure = new CompanyGetAllForTenantID(parameters);
        List<CompanyDto> companies;
        CompanyDto company1;

        // ACT
        using (SqlConnection connection = new SqlConnection(connectionName))
        {
            companies = connection.ExecuteStoredProcedure(procedure);
            company1 = companies.FirstOrDefault();
        }

        // ASSERT
        Assert.AreEqual(expectedCompanyCount, companies.Count);
        Assert.IsNotNull(company1);
    }

So reading down through the test we can see first in the *ARRANGE* section that we are instantiating a connection name, the stored procedure parameters and our stored procedure POCO class.

Then in the  *ACT*, once the *SqlConnection* object is created we can execute the stored procedure by passing our instantiated Stored Procedure object to the *ExecuteStoredProcedure* extension method on the *SqlConnection* object. The framework will open and close a valid connection if the connection is closed, or will keep it open it it is already open.
  
#### Using Transactions with SqlConnection
The stored procedure framework will work with transactions and allow transaction batches to be committed or rolled back.

##### Using TransactionScope
The `TransactionScope` object which can be found in the `System.Transactions.TransactionScope` namespace can be used to control the transaction, and two examples of how to use `TransactionScope` with this framework are shown below.

###### Rolling Back a Transaction
Below is an example of rolling back an insert using the stored procedure framework while the transaction which is controlled by the `TransactionScope` object.

    [TestMethod]
    public void StoredProcedure_WithTransactionScopeNotCommited_DoesNotWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
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
        var companyAddProcedure = new CompaniesAdd(parameters);
        var companyCountProcedure = new CompanyCountAll();

        // ACT
        using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                originalCount  = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyAddProcedure);
                intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            }
        }
        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.Close();
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }

The example test proves that the initial row count is the same as the final row count after the transaction is rolled back, even though in the middle of the transaction it was increased by three rows.
    
###### Committing A Transaction
Below is an example of committing an insert using the stored procedure framework while the transaction which is controlled by the `TransactionScope` object.

    [TestMethod]
    public void StoredProcedure_WithTransactionScopeCompleted_DoesWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
        var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
        {
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
        };
        var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
        {
            Companies = companiesToAdd
        };
        var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
        var companyCountProcedure = new CompanyCountAll();
        var companyDeleteParameters = new TenantIdParameters
        {
            TenantId = 2
        };
        var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);

        // ACT
        using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                originalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyAddProcedure);
                transactionScope.Complete();
            }
        }
        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.ExecuteStoredProcedure(companyDeleteProcedure);
            finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.Close();
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }
    
The example test proves that the intermediate row count went up by three after the transaction was committed. Please note the test does delete the new rows after the test and outside of the transaction to reset the data.

##### SqlTransaction
The `SqlTransaction` object which can be found in the `System.Data.SqlClient` namespace can be used to control the transaction, and two examples of how to use `SqlTransaction` with this framework are shown below.

###### Rolling back a Transaction
Below is an example of rolling back an insert using the stored procedure framework while the transaction which is controlled by the `SqlTransaction` object. Note that you must ensure that the `transaction` is set when calling `ExecuteStoredProcedure`.

    [TestMethod]
    public void StoredProcedure_WithSqlTransactionRolledBack_DoesNotWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
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
        var companyAddProcedure = new CompaniesAdd(parameters);
        var companyCountProcedure = new CompanyCountAll();
        SqlTransaction transaction;

        // ACT
        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            using(transaction = connection.BeginTransaction())
            { 
                originalCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                transaction.Rollback();
            }
        }

        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.Close();
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }

###### Committing a Transaction
Below is an example of committing an insert using the stored procedure framework while the transaction which is controlled by the `SqlTransaction` object. Note that you must ensure that the `transaction` is set when calling `ExecuteStoredProcedure`.
 
    [TestMethod]
    public void StoredProcedure_WithSqlTransactionCommitted_DoesWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
        var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
        {
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
        };
        var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
        {
            Companies = companiesToAdd
        };
        var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
        var companyCountProcedure = new CompanyCountAll();
        var companyDeleteParameters = new TenantIdParameters
        {
            TenantId = 2
        };
        var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);
        SqlTransaction transaction;

        // ACT

        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            using(transaction = connection.BeginTransaction())
            { 
                originalCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                transaction.Commit();
            }
        }

        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();
            intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.ExecuteStoredProcedure(companyDeleteProcedure);
            finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            connection.Close();
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }

        
### Calling the Stored Procedures from Code using DbContext

#### Calling the Stored Procedures from Code using DbContext in traditional way

If you are already using Entity Framework in your solution you may wish to call the stored procedure direct from the DbContext. Providing you import a reference to the *Dibware.StoredProcedureFrameworkForEF* DLL you can use the extension method that DLL provides directly on DbContext object. The example code below shows how we can use the extension method on the **DbContext** object (*or an object that inherits from it*) to execute the stored procedure. The code is basically the same when called on the **SqlConnection** or the **DbConnection**.

    [TestClass]
    public class StoredProcedureFromDbContext
    {
        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            const int expectedCompanyCount = 2;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var parameters = new TenantIdParameters { TenantId = 1 };
            var procedure = new CompanyGetAllForTenantID(parameters);
            List<CompanyDto> companies;
            CompanyDto company1;

            // ACT
            using (ApplicationDbContext context = new ApplicationDbContext(connectionName))
            {
                companies = context.ExecuteStoredProcedure(procedure);
                company1 = companies.FirstOrDefault();
            }

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }

So reading down through the test we can see first in the *ARRANGE* section that we are instantiating a connection name, the stored procedure parameters and our stored procedure POCO class.

Then in the  *ACT*, once the `DBContext` object is created we can execute the stored procedure by passing our instantiated Stored Procedure object to the *ExecuteStoredProcedure* extension method on the `DBContext` object. 

The example above is the *normal* verbose method for using with a `DbContext`. The framework also provides a method to call the stored procedure in a more *entity framework code first kind of approach*, so they appear more like DBSet properties on your custom DbContext object, like so:

    MyContext.MyStoredProcedure.Execute();

However both of these methods require the stored procedures to be defined in a slightly different way. The two methods are known as:
* Simplified API
* Simplified API with in-line declaration

#### Calling the Stored Procedures from Code using DbContext in Simplified API way

Before we can use this method we have to change the base classes which the store procedures inherit from and use the base classes from the `Dibware.StoredProcedureFrameworkForEF` assembly rather than the ones in the `Dibware.StoredProcedureFramework`. The EF specific base classes have teh same names but with a "ForEf" suffix and are as follows:

* StoredProcedureBaseForEf
* NoParametersNoReturnTypeStoredProcedureBaseForEf
* NoParametersStoredProcedureBaseForEf
* NoReturnTypeStoredProcedureBaseForEf

So lets look at calling the following stored procedure using this approach...

    CREATE PROCEDURE [app].[AccountGetAllForCompanyId]
    (
        @CompanyId INT
    )
    AS
    BEGIN
        SELECT 
            AccountId
        ,   CompanyId
        ,   IsActive
        ,   AccountName
        ,   RecordCreatedDateTime
        ,   LastUpdatedDateTime
        FROM
            app.Account
        WHERE
            CompanyId = CompanyId;
    END


So the class that will represent the stored procedure will be like so:

    [Schema("app")]
    internal class AccountGetAllForCompanyId
        : StoredProcedureBaseForEf<List<AccountDto>, CompanyIdParameters>
    {
        public AccountGetAllForCompanyId(DbContext context)
            : base(context, null)
        {
        }
    }
    
 You will notice that we now inherit from `StoredProcedureBaseForEf` instead of `StoredProcedureBase` and the constructor only takes a `DbContext` object and passes through a `null` value to the base for the parameters. We now need to look at adding a property, which will be of type `AccountGetAllForCompanyId`, on to our `ApplicationDbContext` so it can be called like so `Context.AccountGetAllForCompanyId.ExecuteFor(...);`
 
    // [Schema("app")] // We could have SchemaAttribute here instead of on class declaration
    internal AccountGetAllForCompanyId AccountGetAllForCompanyId { get; set; }

Note: you can place the `SchemaAttribute` on either the class definition or the property, the framework can work out the name of the procedure to call with either, but you do not need both. This is useful if you have two identical stored procedures in two different schemas, and have a property for each. You can decorate each property with it's own `SchemaAttribute` and leave it off of the class definition.

when we use properties on the DbContext for our stored procedures we need to initialise the stored procedure  properties in the Context constructor using the call to `InitializeStoredProcedureProperties`, like so...

    internal class ApplicationDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        protected ApplicationDbContext() : base("ApplicationDbContext") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<ApplicationDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<ApplicationDbContext>();
            Database.SetInitializer(databaseInitializer);

            // Instantiate all of the Stored procedure properties
            this.InitializeStoredProcedureProperties();
        }

        #endregion
    }
    
Alternatively you can make sure your application DbContext inherits from the stored procedure framework's `StoredProcedureDbContext` which is in the `Dibware.StoredProcedureFrameworkForEF` namespace and this will automatically call the `InitializeStoredProcedureProperties` extension method on `DbContext` from it's own constructor so you don't have to.

    internal class ApplicationDbContext : StoredProcedureDbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        protected ApplicationDbContext() : base("ApplicationDbContext") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<ApplicationDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<ApplicationDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this is carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
        }

        #endregion
    }    

... we can then execute the stored procedures via the new properties like this for stored procedures without parameters...

        [TestMethod]
        // This method of calling uses the simplified method
        public void AccountGetAllForCompanyId_SIMPLIFIED()
        {
            // ARRANGE
            const int expectedAccountCount = 3000000;
            var parameters = new CompanyIdParameters
            {
                CompanyId = 1
            };

            // ACT
            var accounts = Context.AccountGetAllForCompanyId.ExecuteFor(parameters);
            var actualAccountcount = accounts.Count;

            // ASSERT
            Assert.AreEqual(expectedAccountCount, actualAccountcount);
        }
        
However if you would prefer you can *inline* the parameters with an anonymous object using the method below...

#### Calling the Stored Procedures from Code using DbContext in Simplified API with In-Line Declaration          
        
There is another set of predefined classes available in the box of tools and these are the `StoredProcedure` family of classes and they reside in the `Dibware.StoredProcedureFrameworkForEF.Generic` namespace. These can be used to use anonymous objects to in-line the parameters right into the procedure call, like so...

    Context.TenantGetForId.ExecuteFor(new { TenantId = 1 });

There are three classes in the family:
* `StoredProcedure`
* `StoredProcedure<TReturn>`
* `StoredProcedure<TReturn, TParameters>`

These are used as Types for stored procedure defined as properties on the application's DbContext, giving you with a more generic API so your stored procedure properties look more like your `DbSet` properties on your application's `DbContext`. An example of is shown below:
    
    [Schema("app")]
    internal StoredProcedure<List<TenantDto>> TenantGetAll { get; set; }
    
    [Schema("app")]
    internal StoredProcedure<List<TenantDto>> TenantGetForId { get; set; }
    
    [Schema("app")]
    internal StoredProcedure TenantDeleteForId { get; set; }

When calling these procedures there is little change for a stored procedure with no parameters...

    [TestMethod]
    public void TenantGetAll()
    {
        // ARRANGE
        const int expectedTenantCount = 2;

        // ACT
        var tenants = Context.TenantGetAll.Execute(); 
        TenantDto tenant1 = tenants.FirstOrDefault();

        // ASSERT
        Assert.AreEqual(expectedTenantCount, tenants.Count);
        Assert.IsNotNull(tenant1);
    }

... however for procedures with parameters a new anonymous object can be instantiated and passed in-line in the call...

    [TestMethod]
    // This method of calling uses the simplified in-line method
    public void TenantGetForId_SIMPLIFIED_INLINE()
    {
        // ACT
        var tenant = Context.TenantGetForId.ExecuteFor(new { TenantId = 1 });

        // ASSERT
        Assert.IsNotNull(tenant);
    }

... or ...

    [TestMethod]
    public void TenantDeleteId()
    {
        // ACT
        Context.TenantDeleteForId.ExecuteFor(new { TenantId = 100 });
    }
    
So when using a DbContext you have a few different approaches you can choose from, to best match your own preferred style of coding.

#### Using Transactions with DbContext
The stored procedure framework will work with transactions and allow transaction batches to be committed or rolled back.

##### Using TransactionScope
The `TransactionScope` object which can be found in the `System.Transactions.TransactionScope` namespace can be used to control the transaction, and two examples of how to use `TransactionScope` with this framework are shown below.

###### Rolling Back a Transaction
Below is an example of rolling back an insert using the stored procedure framework while the transaction which is controlled by the `TransactionScope` object.

    [TestMethod]
    public void StoredProcedure_WithTransactionScopeNotCommited_DoesNotWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
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
        var companyAddProcedure = new CompaniesAdd(parameters);
        var companyCountProcedure = new CompanyCountAll();

        // ACT
        using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            using (var context = new ApplicationDbContext(connectionName))
            {
                originalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                context.ExecuteStoredProcedure(companyAddProcedure);
                intermediateCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            }
        }
        using (var context = new ApplicationDbContext(connectionName))
        {
            finalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }

The example test proves that the initial row count is the same as the final row count after the transaction is rolled back, even though in the middle of the transaction it was increased by three rows.
    
###### Committing A Transaction
Below is an example of committing an insert using the stored procedure framework while the transaction which is controlled by the `TransactionScope` object.

    [TestMethod]
    public void StoredProcedure_WithTransactionScopeCompleted_DoesWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
        var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
        {
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
        };
        var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
        {
            Companies = companiesToAdd
        };
        var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
        var companyCountProcedure = new CompanyCountAll();
        var companyDeleteParameters = new TenantIdParameters
        {
            TenantId = 2
        };
        var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);

        // ACT
        using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            using (var context = new ApplicationDbContext(connectionName))
            {
                originalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                context.ExecuteStoredProcedure(companyAddProcedure);
                transactionScope.Complete();
            }
        }
        using (var context = new ApplicationDbContext(connectionName))
        {
            intermediateCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            context.ExecuteStoredProcedure(companyDeleteProcedure);
            finalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }
    
The example test proves that the intermediate row count went up by three after the transaction was committed. Please note the test does delete the new rows after the test and outside of the transaction to reset the data.

##### SqlTransaction
The `SqlTransaction` object which can be found in the `System.Data.SqlClient` namespace can be used to control the transaction, and two examples of how to use `SqlTransaction` with this framework are shown below.

###### Rolling back a Transaction
Below is an example of rolling back an insert using the stored procedure framework while the transaction which is controlled by the `SqlTransaction` object. Note that you must ensure that the `transaction` is set when calling `ExecuteStoredProcedure`.

    [TestMethod]
    public void StoredProcedure_WithSqlTransactionRolledBack_DoesNotWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
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
        var companyAddProcedure = new CompaniesAdd(parameters);
        var companyCountProcedure = new CompanyCountAll();

        // ACT
        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                using (var context = new ApplicationDbContext(connection, false))
                {
                    originalCount = context.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    context.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                    intermediateCount = context.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    transaction.Rollback();
                }
            }
        }

        using (var context = new ApplicationDbContext(connectionName))
        {
            finalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }

###### Committing a Transaction
Below is an example of committing an insert using the stored procedure framework while the transaction which is controlled by the `SqlTransaction` object. Note that you must ensure that the `transaction` is set when calling `ExecuteStoredProcedure`.
 
    [TestMethod]
    public void StoredProcedure_WithSqlTransactionCommitted_DoesWriteRecords()
    {
        // ARRANGE
        const int expectedIntermediateCount = 5;
        int originalCount;
        int intermediateCount;
        int finalCount;
        string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
        var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
        {
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
            new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
        };
        var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
        {
            Companies = companiesToAdd
        };
        var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
        var companyCountProcedure = new CompanyCountAll();
        var companyDeleteParameters = new TenantIdParameters
        {
            TenantId = 2
        };
        var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);
        
        // ACT
        using (var connection = new SqlConnection(connectionName))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                using (var context = new ApplicationDbContext(connection, false))
                {
                    originalCount = context.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    context.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                    transaction.Commit();
                }
            }
        }

        using (var context = new ApplicationDbContext(connectionName))
        {
            intermediateCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
            context.ExecuteStoredProcedure(companyDeleteProcedure);
            finalCount = context.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
        }

        // ASSERT
        Assert.AreEqual(originalCount, finalCount);
        Assert.AreEqual(expectedIntermediateCount, intermediateCount);
    }
