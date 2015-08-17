# StoredProcedureFramework
A .Net framework for calling stored procedures. This is an on-going project and will be  strongly based upon the great work carried out by "bluemoonsailor" at "Mindless Passenger". See link: https://mindlesspassenger.wordpress.com/2011/02/02/code-first-and-stored-procedures/

## Project Brief
The aim of this project is to provide the following:
* (Must) Ability to support a POCO that represent a stored procedure
* (Must) Ability to support a PCOC that represents a row that is returned by a stored procedure
* (Must) Ability to support a PCOC that represents the parameters
* (Must) Ability to execute the stored procedure represented by the POCO against DBConnection using extensions
* (Must) Ability to execute the stored procedure represented by the POCO against SqlConnection using extensions
* (Must) Ability to execute the stored procedure represented by the POCO against DBContext using extensions
* (Must) Ability to handle output parameters
* (Must) Ability to handle all common parameter types
* (Must) Ability to handle all common return data types
* (Must) Contain a suite of unit tests that test all public accessors
* (Must) Contain a suite of integration tests that document usage of the assembly
* (Should) Ability to handle lesser used parameter types
* (Should) Ability to handle lesser used return data types
* (Should) Ability to handle multiple recordsets returned from a stored procedure
* (Should) warn calling code if parameter value data may be truncated due to smaller pameter type
