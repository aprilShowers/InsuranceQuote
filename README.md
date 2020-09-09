# Insurance Quote Api

## Using EF Core as ORM

Install Nuget packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`

run: `dotnet ef` in cmd of current working directory to confirm that EF is installed\
when ready run: `dotnet ef migrations add <InitialMigrationName>` to create migration

## Using local db to store InsuranceCustomer data in InsuranceDB

after connection string is in place and you have registered the Db with the ServiceCollection,\
run: `dotnet ef database update` to create the Db locally

after adding seed data to InsuranceContext via overriding OnModelCreating, you will need to create another ef migration\
once that has been added, when the app starts up, dummy data will be loaded into the db\
this can be confirmed by opening the InsuranceDb in SQL Server Object Explorer and viewing data in the InsuranceCustomers table

## Using Automapper
Install Nuget package:
- `AutoMapper.Extensions.Microsoft.DependencyInjection`

Automapper is what allowed to me to return a Dto to the client instead of the entity. I added a Profiles dir to store the mapping.

### Thoughts I'd like to discuss in meeting
- I opted to keep the data single tiered with one Premium value in the InsuranceCustomers table. In the real world, a customer could have multiple Premiums for multiple jobs with varying Revenues (especially in the world of freelance). 

- I ran into an issue I hadn't considered when doing manual validation in Postman. Because I created the revenue as a decimal type, when passing a paylod with the Revenue missing, it defaulted to 0.00 in db causing the Premium formula to return 0. I did some research and am thinking that a [RangeAttribute] may be the best option.

- When writing a test for the post, I was hoping to return the newly created URI for the new Customer but I do not think it is possible to get the int, since the tests are running in memory. I could hard code a value but as soon as I delete or add a new Customer (with the next id), that test would fail.


