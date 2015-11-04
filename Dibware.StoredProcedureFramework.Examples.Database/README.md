# # Dibware.StoredProcedureFramework.Examples.Database

## Project Purpose 
This project is to hold the database objects for the examples

## Seed Data
The data for seeding the database exists in the *_Seed Data* folder. This data will be automatically deployed to the database when using teh publish option to create the database

## Deployment
Use the *Publish...* option from Visual Studio SSDT to deploy the database object and the seed data. The database should be recreated each time if you use the deployment profile "Dibware.StoredProcedureFramework.Examples.Database.publish.xml".