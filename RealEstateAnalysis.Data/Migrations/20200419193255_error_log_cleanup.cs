using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class error_log_cleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackEnd",
                table: "ErrorsLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBackEnd",
                table: "ErrorsLog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}