﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class autogenerated_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScanDate",
                schema: "LocationAnalysis",
                table: "CityDataCityUrls");

            migrationBuilder.DropColumn(
                name: "IsAutoGenerated",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.AddColumn<DateTime>(
                name: "AutoGeneratedDate",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoGeneratedDate",
                schema: "LocationAnalysis",
                table: "Cities");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScanDate",
                schema: "LocationAnalysis",
                table: "CityDataCityUrls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutoGenerated",
                schema: "LocationAnalysis",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}