using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class types_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PopulationInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "PopulationInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "CrimeIndexInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "CrimeIndexInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "CrimeIndexInYearStart",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "CrimeIndexInYearEnd",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}