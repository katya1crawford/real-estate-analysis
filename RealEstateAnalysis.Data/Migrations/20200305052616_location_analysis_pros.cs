using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class location_analysis_pros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NeighborhoodName",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                schema: "Cities",
                table: "LocationAnalysis",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Cities",
                table: "LocationAnalysis",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "Cities",
                table: "LocationAnalysis",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "Cities",
                table: "LocationAnalysis",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAnalysis_States_StateId",
                schema: "Cities",
                table: "LocationAnalysis",
                column: "StateId",
                principalSchema: "Lookup",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAnalysis_AspNetUsers_UserId",
                schema: "Cities",
                table: "LocationAnalysis",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAnalysis_States_StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                column: "StateId",
                principalSchema: "Lookup",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAnalysis_AspNetUsers_UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationAnalysis_States_StateId",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAnalysis_AspNetUsers_UserId",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAnalysis_States_StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAnalysis_AspNetUsers_UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "NeighborhoodName",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "CityName",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Cities",
                table: "LocationAnalysis");
        }
    }
}