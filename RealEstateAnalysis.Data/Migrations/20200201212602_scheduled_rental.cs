using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class scheduled_rental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualRentIncome",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualGrossScheduledRentalIncome",
                schema: "RentalProperty",
                table: "Properties",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualGrossScheduledRentalIncome",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualRentIncome",
                schema: "RentalProperty",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}