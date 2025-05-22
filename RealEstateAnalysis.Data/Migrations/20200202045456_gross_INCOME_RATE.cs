using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class gross_INCOME_RATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualPotentialGrossIncomeGrowthRate",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualGrossScheduledRentalIncomeGrowthRate",
                schema: "RentalProperty",
                table: "Properties",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualGrossScheduledRentalIncomeGrowthRate",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualPotentialGrossIncomeGrowthRate",
                schema: "RentalProperty",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}