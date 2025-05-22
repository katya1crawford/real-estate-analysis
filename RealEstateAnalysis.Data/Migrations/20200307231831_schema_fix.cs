using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class schema_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationAnalysis",
                schema: "Neighborhoods",
                table: "LocationAnalysis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationAnalysis",
                schema: "Cities",
                table: "LocationAnalysis");

            migrationBuilder.EnsureSchema(
                name: "LocationAnalysis");

            migrationBuilder.RenameTable(
                name: "LocationAnalysis",
                schema: "Neighborhoods",
                newName: "Neighborhoods",
                newSchema: "LocationAnalysis");

            migrationBuilder.RenameTable(
                name: "LocationAnalysis",
                schema: "Cities",
                newName: "Cities",
                newSchema: "LocationAnalysis");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                newName: "IX_Neighborhoods_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                newName: "IX_Neighborhoods_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAnalysis_UserId",
                schema: "LocationAnalysis",
                table: "Cities",
                newName: "IX_Cities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAnalysis_StateId",
                schema: "LocationAnalysis",
                table: "Cities",
                newName: "IX_Cities_StateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Neighborhoods",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                schema: "LocationAnalysis",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateId",
                schema: "LocationAnalysis",
                table: "Cities",
                column: "StateId",
                principalSchema: "Lookup",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_AspNetUsers_UserId",
                schema: "LocationAnalysis",
                table: "Cities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhoods_States_StateId",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                column: "StateId",
                principalSchema: "Lookup",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhoods_AspNetUsers_UserId",
                schema: "LocationAnalysis",
                table: "Neighborhoods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateId",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_AspNetUsers_UserId",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_States_StateId",
                schema: "LocationAnalysis",
                table: "Neighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_AspNetUsers_UserId",
                schema: "LocationAnalysis",
                table: "Neighborhoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Neighborhoods",
                schema: "LocationAnalysis",
                table: "Neighborhoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.EnsureSchema(
                name: "Cities");

            migrationBuilder.EnsureSchema(
                name: "Neighborhoods");

            migrationBuilder.RenameTable(
                name: "Neighborhoods",
                schema: "LocationAnalysis",
                newName: "LocationAnalysis",
                newSchema: "Neighborhoods");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "LocationAnalysis",
                newName: "LocationAnalysis",
                newSchema: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_Neighborhoods_UserId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                newName: "IX_LocationAnalysis_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Neighborhoods_StateId",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                newName: "IX_LocationAnalysis_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_UserId",
                schema: "Cities",
                table: "LocationAnalysis",
                newName: "IX_LocationAnalysis_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_StateId",
                schema: "Cities",
                table: "LocationAnalysis",
                newName: "IX_LocationAnalysis_StateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationAnalysis",
                schema: "Neighborhoods",
                table: "LocationAnalysis",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationAnalysis",
                schema: "Cities",
                table: "LocationAnalysis",
                column: "Id");

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
    }
}