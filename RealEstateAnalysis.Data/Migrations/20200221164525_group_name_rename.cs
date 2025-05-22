using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class group_name_rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "ReportGroupName",
                schema: "RentalProperty",
                table: "Properties",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportGroupName",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                schema: "RentalProperty",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}