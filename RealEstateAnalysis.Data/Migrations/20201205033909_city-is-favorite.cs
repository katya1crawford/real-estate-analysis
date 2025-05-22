using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class cityisfavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                schema: "LocationAnalysis",
                table: "Cities");
        }
    }
}