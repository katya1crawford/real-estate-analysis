using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class locationcities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EthnicMixMainSlicePercent",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "RecentYearJobsPercentageChange",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.AddColumn<decimal>(
                name: "EthnicMixLargestSlicePercent",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearStart",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "PopulationInYearEnd",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "CrimeIndexInYearStart",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "CrimeIndexInYearEnd",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<decimal>(
                name: "RecentYearJobsGrowthRate",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EthnicMixLargestSlicePercent",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "RecentYearJobsGrowthRate",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.AddColumn<decimal>(
                name: "EthnicMixMainSlicePercent",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<short>(
                name: "PopulationInYearStart",
                schema: "Cities",
                table: "LocationAnalysis",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "PopulationInYearEnd",
                schema: "Cities",
                table: "LocationAnalysis",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "CrimeIndexInYearStart",
                schema: "Cities",
                table: "LocationAnalysis",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "CrimeIndexInYearEnd",
                schema: "Cities",
                table: "LocationAnalysis",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "RecentYearJobsPercentageChange",
                schema: "Cities",
                table: "LocationAnalysis",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}