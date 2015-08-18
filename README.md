# StoredProcedureFramework #
**A .Net framework for calling stored procedures.**
This framework can be used with or without the presence of Entity Framework, but a seperate dll is required when using EF.

**Please Note:**
This is an on-going project and has been inspired by and some of teh ocde will be strongly based upon the great work carried out by "bluemoonsailor" at "Mindless Passenger". 
See link: [https://mindlesspassenger.wordpress.com/2011/02/02/code-first-and-stored-procedures/]

## Project Brief ##
The aim of this project is to provide the following:
* (Must) Ability to support a POCO that represent a stored procedure **Done**
* (Must) Ability to support a POCO that represents a row that is returned by a stored procedure **Done**
* (Must) Ability to support a POCO that represents the parameters **Done**
* (Must) Ability to execute the stored procedure represented by the POCO against DBConnection using extensions **WIP**
* (Must) Ability to execute the stored procedure represented by the POCO against SqlConnection using extensions **WIP**
* (Must) Ability to execute the stored procedure represented by the POCO against DBContext using extensions **WIP**
* (Must) Ability to handle output parameters
* (Must) Ability to handle all common parameter types
* (Must) Ability to handle all common return data types
* (Must) Ability to handle stored procedures that return no results **Done**
* (Must) Contain a suite of unit tests that test all public accessors
* (Must) Contain a suite of integration tests that document usage of the assembly
* (Must) Entity Framework specifc extensions must be in own assembly to remove dependency on EF DLLs for main project assembly **Done**
* (Should) Ability to handle lesser used parameter types
* (Should) Ability to handle lesser used return data types
* (Should) Ability to handle multiple recordsets returned from a stored procedure
* (Should) Warn calling code if parameter value data may be truncated due to smaller pameter type
* (Should) Implement David Doran's "FastActivator" for object intanciation
* (Could) Not have any "Resharper" warnings or "code clones" in production code
