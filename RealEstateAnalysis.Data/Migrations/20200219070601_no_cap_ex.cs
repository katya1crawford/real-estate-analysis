using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class no_cap_ex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualCapitalExpendituresRate",
                schema: "RentalProperty",
                table: "Properties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AnnualCapitalExpendituresRate",
                schema: "RentalProperty",
                table: "Properties",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}