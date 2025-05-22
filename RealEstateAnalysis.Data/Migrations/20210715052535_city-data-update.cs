using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class citydataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedianHouseOrCondoValueYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedianHouseholdIncomeYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PopulationYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedianHouseOrCondoValueYearStart",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "MedianHouseholdIncomeYearStart",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "PopulationYearStart",
                schema: "LocationAnalysis",
                table: "Cities");
        }
    }
}