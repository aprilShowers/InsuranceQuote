using Microsoft.EntityFrameworkCore.Migrations;

namespace InsuranceQuote.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revenue = table.Column<decimal>(nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Business = table.Column<string>(maxLength: 40, nullable: false),
                    Premium = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCustomers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceCustomers");
        }
    }
}
