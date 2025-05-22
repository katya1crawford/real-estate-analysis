using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class location_column_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeighborhoodPovertyRate",
                schema: "LocationAnalysis",
                table: "Neighborhoods");

            migrationBuilder.AddColumn<decimal>(
                name: "PovertyRate",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PovertyRate",
                schema: "LocationAnalysis",
                table: "Neighborhoods");

            migrationBuilder.AddColumn<decimal>(
                name: "NeighborhoodPovertyRate",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}