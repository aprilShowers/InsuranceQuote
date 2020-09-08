# Insurance Quote Api

## Using EF Core as ORM

Install Nuget packages:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer

run: `dotnet ef` in cmd of current working directory to confirm that EF is installed\
when ready run: `dotnet ef migrations add <InitialMigrationName>` to create migration

## Using local db to store InsuranceCustomer data in InsuranceDB

after connection string is in place and you have registered the Db with the ServiceCollection,\
run: `dotnet ef database update` to create the Db locally
