using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class decimalfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "MonetaryTransactions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MonetaryTransactions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "MonetaryTransactions",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MonetaryTransactions",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}