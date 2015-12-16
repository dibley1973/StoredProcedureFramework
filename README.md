# StoredProcedureFramework #
**A .Net framework for SQL Server calling stored procedures.**
The purpose of this framework is to allow stored procedures, their parameters and their return types to be represented in strongly typed .Net classes. These can then be used in conjunction with a SqlConnection, DbConnection or DbContext to execute the stored procedure. This framework can be used with or without the presence of Entity Framework, but a separate dll (`Dibware.StoredProcedureFrameworkForEF` which is part of this project) is required when using with EF.

**Please Note:**
* This project has been inspired by and some of the code will be strongly based upon the great work carried out by "bluemoonsailor" at "Mindless Passenger". 
See link: [https://mindlesspassenger.wordpress.com/2011/02/02/code-first-and-stored-procedures/]

This is an on-going project, 0.7 is WIP, but v0.6 is the most recent Release Candidate as it is stable.

## Versions
* 0.7 WIP (support for Scalar functions and Table value functions)
* 0.6 (Release Candidate) Cleaned code, bug fix for Issue #5 and preparation for adding SQL UDF support
* 0.5 (Release Candidate) This version supports transactions.
* 0.4   
* 0.3 This version supports stored procedures with Table Value Parameters. 
* 0.2 This version will support multiple RecordSets and will have a different API to version 1.0.
* 0.1 This was the initial version which did not support multiple RecordSets. To enable multiple RecordSets to be supported alongside single RecordSets a break to the API is required. Development has stopped on this version but the code will remain available for use.

## Nuget Package
The project is not currently available on NuGet but it is the intention to make this so once the version reaches 1.0 and is considered solid.

## Project Brief ##
The aim of this project is to provide the following:
* (Must) Ability to support a POCO that represent a stored procedure  **Done**
* (Must) Ability to support a POCO that represents a row that is returned by a stored procedure  **Done**
* (Must) Ability to support a POCO that represents the parameters  **Done**
* (Must) Ability to execute the stored procedure represented by the POCO against DBConnection using extensions  **Done**
* (Must) Ability to execute the stored procedure represented by the POCO against SqlConnection using extensions **Done**
* (Must) Ability to execute the stored procedure represented by the POCO against DBContext using extensions **Done**
* (Must) Ability to handle output parameters **Done**
* (Must) Ability to handle all common parameter types **Done**
* (Must) Ability to handle all common return data types **Done**
* (Must) Ability to handle precision and scale for number data types **Done**
* (Must) Ability to handle size for string data types **Done**
* (Must) Ability to handle stored procedures that return no results **Done**
* (Must) Ability to handle parameters with NULL value **Done**
* (Must) Ability to handle return types with NULL values **Done**
* (Must) Ability to support Table Value Parameters **Done**
* (Must) Ability to support Transactions **Done**
* (Must) Entity Framework specific extensions must be in own assembly to remove dependency on EF DLLs for main project assembly **Done**
* (Should) Implement ability to call stored procedures from DbContext like "MyContext.MyStoredProcedure.Execute()" **Done**
* (Should) Ability to handle multiple RecordSets returned from a stored procedure **Done**
* (Should) Contain a suite of Example Tests that document usage of both assemblies **Done**
* (Should) Contain a suite of Integration Tests for both assemblies **Done**
* (Should) Contain a suite of Unit Tests that test all public accessors **Done**
* (Should) Ability to handle lesser used parameter types **Not currently on roadmap**
* (Should) Ability to handle lesser used return data types **Not currently on roadmap**
* (Should) Warn calling code if parameter value data may be truncated due to smaller parameter type **On-going Investigation**
* (Should) Implement David Doran's "FastActivator" for object instantiation **Investigated: no gain**
* (Could) Ability to return results from Sql User Defined Scalar Function **Done**
* (Could) Ability to return results from Sql User Defined Table Function **Done**
* (Could) Not have any "Resharper" warnings **WIP**
* (Could) Not have any "Code Clones" in production code **WIP**

## WIKI
Please visit the wiki for examples how to define classes which represent a stored procedure and use them in code to call the stored procedures they represent [WIKI link](https://github.com/dibley1973/StoredProcedureFramework/wiki)

## Bugs, Enhancement or Feature Requests
For any bugs, enhancements or for feature  requests please raise an Issues with the appropriate label; `bug` or `enhancement`. 

### Bugs
for a bug, please describe the bug and steps to reproduce. Please provide one or more unit or integration tests which prove the existence of the bug and will pass once the bug is fixed. This will greatly speed up bug detection and fixing. Where possible included the SQL code for a self contained stored procedure which can be used to test for the bug and the fix.

### Enhancement or Feature Requests
For enhancements or feature requests, please detail what the feature is. Please also include one or more integration tests which describe how the feature will be called and how teh results shoudl appear.

## Solution
The solution is written in C# .Net v4.0. The decision to write in v4.0 and not a later version is to enable other projects with this framework version and above to be able to consume it.

### Folder Structure
The folder structure is an ever evolving beast, as I strive to get a logical organisation for it

* Solution
  + Binaries
    - 0.3
    - 0.4
    - 0.5 RC
    - 0.6 RC
  + CodeBase
    - Dibware.StoredProcedureFramework.csproj
    - Dibware.StoredProcedureFrameworkForEF.csproj
  + Documents
  + Examples
    - Dibware.StoredProcedureFramework.Examples.csproj
    - Dibware.StoredProcedureFramework.Examples.Database.sqlproj
  + Tests
    - Dibware.StoredProcedureFramework.IntegrationTests.csproj
    - Dibware.StoredProcedureFramework.IntegrationTests.Database.sqlproj
    - Dibware.StoredProcedureFramework.UnitTests.csproj
    
#### Binaries
This folder contains the compiled DLLs in folders with the respective version numbers.

#### CodeBase
This folder contains the physical code which is compiled in to the binaries folder, above.

#### Documents
This folder contains the documentation for this project. It includes this document.

#### Examples
This folder contains two projects. The first is a unit test project which contains examples describing how to use the framework. The second project is the database project for the examples in the first project.

#### Tests
This project contains three projects. The first is the Integration Test project which contains test which interact with a test database. The second project is the database project for the Integration Test project. The third project is a Unit Test project. No database project is needed for this.
