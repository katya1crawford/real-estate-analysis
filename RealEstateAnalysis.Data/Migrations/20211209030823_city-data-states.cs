using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class citydatastates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityDataStateUrls",
                schema: "LocationAnalysis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityDataStateUrls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityDataStateUrls",
                schema: "LocationAnalysis");
        }
    }
}