using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class removecitydatacitiesurls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityDataCityUrls",
                schema: "LocationAnalysis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityDataCityUrls",
                schema: "LocationAnalysis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityDataCityUrls", x => x.Id);
                });
        }
    }
}