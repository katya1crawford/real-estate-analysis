using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateAnalysis.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FixAndFlipProperty");

            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.EnsureSchema(
                name: "RentalProperty");

            migrationBuilder.EnsureSchema(
                name: "Reonomy");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 500, nullable: true),
                    RefreshTokenExpirationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorsLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Values = table.Column<string>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    IsBackEnd = table.Column<bool>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCostTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExteriorRepairExpenseTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExteriorRepairExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralRepairExpenseTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralRepairExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InteriorRepairExpenseTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteriorRepairExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatingExpenseTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    TransactionNumber = table.Column<string>(maxLength: 150, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonetaryTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    AnnualPropertyTaxes = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    City = table.Column<string>(maxLength: 500, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Neighborhood = table.Column<string>(maxLength: 250, nullable: true),
                    County = table.Column<string>(maxLength: 250, nullable: true),
                    YearBuiltIn = table.Column<int>(nullable: false),
                    Bathrooms = table.Column<double>(nullable: false),
                    Bedrooms = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    DownPayment = table.Column<decimal>(nullable: false),
                    LoanApr = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    LoanYears = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    SquareFootage = table.Column<int>(nullable: false),
                    ThumbnailImage = table.Column<byte[]>(nullable: true),
                    ThumbnailImageContentType = table.Column<string>(nullable: true),
                    PropertyTypeId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: false)
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
                name: "Properties",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    City = table.Column<string>(maxLength: 500, nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Neighborhood = table.Column<string>(maxLength: 250, nullable: true),
                    County = table.Column<string>(maxLength: 250, nullable: true),
                    YearBuiltIn = table.Column<int>(nullable: false),
                    BuildingSquareFootage = table.Column<int>(nullable: false),
                    LotSquareFootage = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false),
                    DownPayment = table.Column<decimal>(nullable: false),
                    AnnualRentIncome = table.Column<decimal>(nullable: false),
                    OtherAnnualIncome = table.Column<decimal>(nullable: false),
                    AnnualVacancyRate = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    AnnualCapitalExpendituresRate = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    AnnualPropertyManagementFeeRate = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    LoanApr = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    LoanYears = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ThumbnailImage = table.Column<byte[]>(nullable: true),
                    ThumbnailImageContentType = table.Column<string>(nullable: true),
                    AnnualPotentialGrossIncomeGrowthRate = table.Column<decimal>(nullable: false),
                    AnnualOperatingExpensesGrowthRate = table.Column<decimal>(nullable: false),
                    PropertyTypeId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(maxLength: 450, nullable: false)
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
                name: "SoldProperties",
                schema: "Reonomy",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    City = table.Column<string>(maxLength: 500, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: true),
                    Neighborhood = table.Column<string>(maxLength: 250, nullable: true),
                    County = table.Column<string>(maxLength: 250, nullable: true),
                    BuildingSquareFootage = table.Column<int>(nullable: true),
                    Fips = table.Column<string>(maxLength: 250, nullable: true),
                    SourceId = table.Column<string>(maxLength: 100, nullable: true),
                    LotSquareFootage = table.Column<int>(nullable: true),
                    MortgageAmount = table.Column<decimal>(nullable: true),
                    MortgageLenderName = table.Column<string>(maxLength: 150, nullable: true),
                    MortgageRecordingDate = table.Column<DateTime>(nullable: true),
                    SalesDate = table.Column<DateTime>(nullable: true),
                    StdLandUseCodeDescription = table.Column<string>(maxLength: 500, nullable: true),
                    TotalUnits = table.Column<int>(nullable: true),
                    YearBuilt = table.Column<int>(nullable: true),
                    SalesPrice = table.Column<decimal>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    InvalidAddress = table.Column<bool>(nullable: false),
                    StateId = table.Column<long>(nullable: false),
                    PropertyTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoldProperties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalSchema: "Lookup",
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoldProperties_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "Lookup",
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCosts",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ClosingCostTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ExteriorRepairItemTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    GeneralRepairExpenseTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    InteriorRepairExpenseTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                name: "AnnualOperatingExpenses",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    OperatingExpenseTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualOperatingExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualOperatingExpenses_OperatingExpenseTypes_OperatingExpenseTypeId",
                        column: x => x.OperatingExpenseTypeId,
                        principalSchema: "Lookup",
                        principalTable: "OperatingExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualOperatingExpenses_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCosts",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ClosingCostTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExteriorRepairExpenses",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ExteriorRepairItemTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImages",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneralRepairExpenses",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    GeneralRepairExpenseTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteriorRepairExpenses",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    InteriorRepairExpenseTypeId = table.Column<long>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
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
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitGroups",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Bathrooms = table.Column<int>(nullable: false),
                    Bedrooms = table.Column<int>(nullable: false),
                    NumberOfUnits = table.Column<int>(nullable: false),
                    SquareFootage = table.Column<int>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitGroups_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RentalProperty",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesContent",
                schema: "FixAndFlipProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FilesContent",
                schema: "RentalProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesContent_Files_FileId",
                        column: x => x.FileId,
                        principalSchema: "RentalProperty",
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryTransactions_UserId",
                table: "MonetaryTransactions",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_AnnualOperatingExpenses_OperatingExpenseTypeId",
                schema: "RentalProperty",
                table: "AnnualOperatingExpenses",
                column: "OperatingExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualOperatingExpenses_PropertyId",
                schema: "RentalProperty",
                table: "AnnualOperatingExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCosts_ClosingCostTypeId",
                schema: "RentalProperty",
                table: "ClosingCosts",
                column: "ClosingCostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCosts_PropertyId",
                schema: "RentalProperty",
                table: "ClosingCosts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExteriorRepairExpenses_ExteriorRepairItemTypeId",
                schema: "RentalProperty",
                table: "ExteriorRepairExpenses",
                column: "ExteriorRepairItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExteriorRepairExpenses_PropertyId",
                schema: "RentalProperty",
                table: "ExteriorRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PropertyId",
                schema: "RentalProperty",
                table: "Files",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesContent_FileId",
                schema: "RentalProperty",
                table: "FilesContent",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_PropertyId",
                schema: "RentalProperty",
                table: "GalleryImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRepairExpenses_GeneralRepairExpenseTypeId",
                schema: "RentalProperty",
                table: "GeneralRepairExpenses",
                column: "GeneralRepairExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralRepairExpenses_PropertyId",
                schema: "RentalProperty",
                table: "GeneralRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_InteriorRepairExpenses_InteriorRepairExpenseTypeId",
                schema: "RentalProperty",
                table: "InteriorRepairExpenses",
                column: "InteriorRepairExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InteriorRepairExpenses_PropertyId",
                schema: "RentalProperty",
                table: "InteriorRepairExpenses",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                schema: "RentalProperty",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StateId",
                schema: "RentalProperty",
                table: "Properties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                schema: "RentalProperty",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroups_PropertyId",
                schema: "RentalProperty",
                table: "UnitGroups",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldProperties_PropertyTypeId",
                schema: "Reonomy",
                table: "SoldProperties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoldProperties_StateId",
                schema: "Reonomy",
                table: "SoldProperties",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ErrorsLog");

            migrationBuilder.DropTable(
                name: "MonetaryTransactions");

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
                name: "AnnualOperatingExpenses",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "ClosingCosts",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "ExteriorRepairExpenses",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "FilesContent",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "GalleryImages",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "GeneralRepairExpenses",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "InteriorRepairExpenses",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "UnitGroups",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "SoldProperties",
                schema: "Reonomy");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "OperatingExpenseTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "ClosingCostTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "ExteriorRepairExpenseTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "GeneralRepairExpenseTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "InteriorRepairExpenseTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "FixAndFlipProperty");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "RentalProperty");

            migrationBuilder.DropTable(
                name: "PropertyTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "States",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}