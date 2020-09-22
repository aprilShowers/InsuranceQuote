# Insurance Quote Api

This app is quick and not Production ready but it works. I'm excited to talk with the team about features that would make it better.

## Using EF Core as ORM

Install Nuget packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`

install EF Core in CLI: `dotnet tool install --global dotnet-ef`\
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
- I opted to keep the data single tiered with one Premium value in the InsuranceCustomers table. In the real world, a customer could have multiple Premiums for multiple jobs with varying Revenues (especially in the world of freelance). In that case, there would be a Policy entity to store each PolicyQuote entity a Customer was requesting and the InsuranceCustomer would have an ICollection\<Policy\> Policies to store all their requested quotes.

- I opted to Seed the Db with no Premium, so that a quick table check would show the difference between seeded and manually added data.

- For brevity, I did not create a Data Transfer Object for the GETs. I know this is a no-no but really wanted to focus my time on the POST/PUT returning only a Premium. In the real world, I wouldn't return my data model directly to the client.

- I ran into an issue I hadn't considered when doing manual validation in Postman. Because I created the revenue as a decimal type, when passing a paylod with the Revenue missing, it defaulted to 0.00 in db causing the Premium formula to return 0. I added a Range Attribute to display a meaningful error message to the user and set the Revenue's Column type to a decimal.

- Passing a State or Business (that was not one of the three covered) in the payload also caused issues for the Premium calculation. I added a custom Validation Attribute for each to present the user with a meaningful error message in both cases.

#### Things that could be Refactored

- The integration tests are running in memory. For this reason, the DELETE test fails if run more than once because it has a hard coded {id} to delete. I don't love that and with more time, I would fix this issue and thought about using a MockDb to do so or just returning the Db to a clean seeded state after each test run. 

- As the list of covered States and Businneses grows, it might make sense to store them in an enum and do a switch on them.

- A complete API would also have a PATCH method but I didn't add it in the interest of time.

- The QuotesController should not be responsible for rounding decimals; it violates the Single Responsibilty Principle.
