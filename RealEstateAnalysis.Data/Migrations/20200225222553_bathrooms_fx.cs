using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class bathrooms_fx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Bathrooms",
                schema: "RentalProperty",
                table: "UnitGroups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Bathrooms",
                schema: "RentalProperty",
                table: "UnitGroups",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}