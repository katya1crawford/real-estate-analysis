using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class homesmediandays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomesMedianDaysOnMarket",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomesMedianDaysOnMarket",
                schema: "LocationAnalysis",
                table: "Neighborhoods");
        }
    }
}