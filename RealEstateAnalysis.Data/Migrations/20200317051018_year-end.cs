using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class yearend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedianHouseOrCondoValueYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedianHouseholdIncomeYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PopulationYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedianHouseOrCondoValueYearEnd",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "MedianHouseholdIncomeYearEnd",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PopulationYearEnd",
                schema: "LocationAnalysis",
                table: "Cities");
        }
    }
}