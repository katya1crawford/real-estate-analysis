using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class nofixflip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosingCosts",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "ExteriorRepairExpenses",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "FilesContent",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "GeneralRepairExpenses",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "InteriorRepairExpenses",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "FixAndFlipProperty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FixAndFlipProperty");

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AnnualPropertyTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bathrooms = table.Column<double>(type: "float", nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    County = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    LoanApr = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    LoanYears = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Neighborhood = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SquareFootage = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<long>(type: "bigint", nullable: false),
                    ThumbnailImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    YearBuiltIn = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalSchema: "Lookup",
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "Lookup",
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCosts",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingCostTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCosts_ClosingCostTypes_ClosingCostTypeId",
                        column: x => x.ClosingCostTypeId,
                        principalSchema: "Lookup",
                        principalTable: "ClosingCostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClosingCosts_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExteriorRepairExpenses",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExteriorRepairItemTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExteriorRepairExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExteriorRepairExpenses_ExteriorRepairExpenseTypes_ExteriorRepairItemTypeId",
                        column: x => x.ExteriorRepairItemTypeId,
                        principalSchema: "Lookup",
                        principalTable: "ExteriorRepairExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExteriorRepairExpenses_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralRepairExpenses",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralRepairExpenseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralRepairExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralRepairExpenses_GeneralRepairExpenseTypes_GeneralRepairExpenseTypeId",
                        column: x => x.GeneralRepairExpenseTypeId,
                        principalSchema: "Lookup",
                        principalTable: "GeneralRepairExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneralRepairExpenses_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteriorRepairExpenses",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InteriorRepairExpenseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteriorRepairExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteriorRepairExpenses_InteriorRepairExpenseTypes_InteriorRepairExpenseTypeId",
                        column: x => x.InteriorRepairExpenseTypeId,
                        principalSchema: "Lookup",
                        principalTable: "InteriorRepairExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteriorRepairExpenses_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesContent",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesContent_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "FixAndFlipProperty",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCosts_ClosingCostTypeId",
                schema: "FixAndFlipProperty",
                table: "ClosingCosts",
                column: "ClosingCostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCosts_PropertyId",
                schema: "FixAndFlipProperty",
                table: "ClosingCosts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExteriorRepairExpenses_ExteriorRepairItemTypeId",
                schema: "FixAndFlipProperty",
                table: "ExteriorRepairExpenses",
                column: "ExteriorRepairItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExteriorRepairExpenses_PropertyId",
                schema: "FixAndFlipProperty",
                table: "ExteriorRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PropertyId",
                schema: "FixAndFlipProperty",
                table: "Files",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesContent_FileId",
                schema: "FixAndFlipProperty",
                table: "FilesContent",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRepairExpenses_GeneralRepairExpenseTypeId",
                schema: "FixAndFlipProperty",
                table: "GeneralRepairExpenses",
                column: "GeneralRepairExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRepairExpenses_PropertyId",
                schema: "FixAndFlipProperty",
                table: "GeneralRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_InteriorRepairExpenses_InteriorRepairExpenseTypeId",
                schema: "FixAndFlipProperty",
                table: "InteriorRepairExpenses",
                column: "InteriorRepairExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InteriorRepairExpenses_PropertyId",
                schema: "FixAndFlipProperty",
                table: "InteriorRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                schema: "FixAndFlipProperty",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StateId",
                schema: "FixAndFlipProperty",
                table: "Properties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                schema: "FixAndFlipProperty",
                table: "Properties",
                column: "UserId");
        }
    }
}