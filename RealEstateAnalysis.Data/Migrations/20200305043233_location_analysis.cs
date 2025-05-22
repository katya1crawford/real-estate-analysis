using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class location_analysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cities");

            migrationBuilder.EnsureSchema(
                name: "Neighborhoods");

            migrationBuilder.CreateTable(
                name: "LocationAnalysis",
                schema: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    PopulationInYearStart = table.Column<short>(nullable: false),
                    PopulationInYearEnd = table.Column<short>(nullable: false),
                    MedianHouseholdIncomeInYearStart = table.Column<decimal>(nullable: false),
                    MedianHouseholdIncomeInYearEnd = table.Column<decimal>(nullable: false),
                    MedianHouseOrCondoValueInYearStart = table.Column<decimal>(nullable: false),
                    MedianHouseOrCondoValueInYearEnd = table.Column<decimal>(nullable: false),
                    CrimeIndexInYearStart = table.Column<short>(nullable: false),
                    CrimeIndexInYearEnd = table.Column<short>(nullable: false),
                    RecentYearJobsPercentageChange = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAnalysis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationAnalysis",
                schema: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    MedianHouseholdIncome = table.Column<decimal>(nullable: false),
                    MedianContractRent = table.Column<decimal>(nullable: false),
                    CityUnemploymentRate = table.Column<decimal>(nullable: false),
                    NeighborhoodUnemploymentRate = table.Column<decimal>(nullable: false),
                    NeighborhoodPovertyRate = table.Column<decimal>(nullable: false),
                    EthnicMixMainSlicePercent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAnalysis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAnalysis",
                schema: "Cities");

            migrationBuilder.DropTable(
                name: "LocationAnalysis",
                schema: "Neighborhoods");
        }
    }
}