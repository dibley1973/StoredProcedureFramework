# StoredProcedureFramework #
**A .Net framework for calling stored procedures.**
This framework can be used with or without the presence of Entity Framework, but a seperate dll is required when using EF.

**Please Note:**
This is an on-going project and has been inspired by and some of the ocde will be strongly based upon the great work carried out by "bluemoonsailor" at "Mindless Passenger". 
See link: [https://mindlesspassenger.wordpress.com/2011/02/02/code-first-and-stored-procedures/]

Please note a new API change is in progress. Please refer to code in branch "v0.2" which is in synch with the current documentation.

## Versions
* 0.2 This version will suupport multiple recordsets and will have a different API to version 1.0. This is the version that is currently in development.
* 0.1 This was the initial version which did not support multiple recordsets. To enable multiple recordsets to be supported alongside single recordsets a break to the API is required. Development has stopped on this version but the code will remain available for use.

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
* (Must) Entity Framework specifc extensions must be in own assembly to remove dependency on EF DLLs for main project assembly **Done**
* (Should) Ability to handle multiple recordsets returned from a stored procedure **Done**
* (Should) Contain a suite of unit tests that test all public accessors
* (Should) Contain a suite of integration tests that document usage of the assembly **WIP**
* (Should) Ability to handle lesser used parameter types
* (Should) Ability to handle lesser used return data types
* (Should) Warn calling code if parameter value data may be truncated due to smaller pameter type
* (Should) Implement David Doran's "FastActivator" for object intanciation **Investigated: no gain**
* (Should) Implement ability to call stored procedures from DbContext like "MyContext.MyStoredProcedure.Execute()" **WIP**
* (Could) Not have any "Resharper" warnings **WIP**
* (Could) Not have any "Code Clones" in production code **WIP**

## WIKI ##
Please visit the wiki for examples how to define classes which represent a stored procedure and use them in code to call the stored procedures they represent [WIKI link](https://github.com/dibley1973/StoredProcedureFramework/wiki)
