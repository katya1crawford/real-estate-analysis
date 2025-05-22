using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class propertystatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "PropertyStatuses",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties",
                column: "PropertyStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyStatuses_PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties",
                column: "PropertyStatusId",
                principalSchema: "Lookup",
                principalTable: "PropertyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyStatuses_PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyStatuses",
                schema: "Lookup");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyStatusId",
                schema: "RentalProperty",
                table: "Properties");
        }
    }
}