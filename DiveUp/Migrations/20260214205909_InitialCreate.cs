using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiveUp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AgentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VatNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoatName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcursionSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelDestinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDestinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceListName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RateValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reps_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursions_ExcursionSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "ExcursionSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_HotelDestinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "HotelDestinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationTypes_TransportationSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "TransportationSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherFrom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherTo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherCount = table.Column<int>(type: "int", nullable: true),
                    RepId = table.Column<int>(type: "int", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vouchers_Reps_RepId",
                        column: x => x.RepId,
                        principalTable: "Reps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TransportationCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CostValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationCosts_TransportationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransportationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgentCode",
                table: "Agents",
                column: "AgentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_SupplierId",
                table: "Excursions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_DestinationId",
                table: "Hotels",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reps_AgentId",
                table: "Reps",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCosts_TypeId",
                table: "TransportationCosts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationTypes_SupplierId",
                table: "TransportationTypes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_RepId",
                table: "Vouchers",
                column: "RepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boats");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "TransportationCosts");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "ExcursionSuppliers");

            migrationBuilder.DropTable(
                name: "HotelDestinations");

            migrationBuilder.DropTable(
                name: "TransportationTypes");

            migrationBuilder.DropTable(
                name: "Reps");

            migrationBuilder.DropTable(
                name: "TransportationSuppliers");

            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}
