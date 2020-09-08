using Microsoft.EntityFrameworkCore.Migrations;

namespace InsuranceQuote.Api.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InsuranceCustomers",
                columns: new[] { "Id", "Business", "Premium", "Revenue", "State" },
                values: new object[,]
                {
                    { 1, "Plumber", 0m, 8200.00m, "FL" },
                    { 2, "Programmer", 0m, 20000.00m, "FL" },
                    { 3, "Architect", 0m, 45000.00m, "OH" },
                    { 4, "Architect", 0m, 45000.00m, "TX" },
                    { 5, "Programmer", 0m, 1000000.00m, "OH" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsuranceCustomers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InsuranceCustomers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InsuranceCustomers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InsuranceCustomers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InsuranceCustomers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
