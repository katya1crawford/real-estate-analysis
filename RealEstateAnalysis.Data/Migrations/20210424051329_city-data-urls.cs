using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class citydataurls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CityDataCityUrl",
                schema: "LocationAnalysis",
                table: "CityDataCityUrl");

            migrationBuilder.RenameTable(
                name: "CityDataCityUrl",
                schema: "LocationAnalysis",
                newName: "CityDataCityUrls",
                newSchema: "LocationAnalysis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityDataCityUrls",
                schema: "LocationAnalysis",
                table: "CityDataCityUrls",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CityDataCityUrls",
                schema: "LocationAnalysis",
                table: "CityDataCityUrls");

            migrationBuilder.RenameTable(
                name: "CityDataCityUrls",
                schema: "LocationAnalysis",
                newName: "CityDataCityUrl",
                newSchema: "LocationAnalysis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityDataCityUrl",
                schema: "LocationAnalysis",
                table: "CityDataCityUrl",
                column: "Id");
        }
    }
}