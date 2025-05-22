using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class another_type_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PopulationInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PopulationInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}