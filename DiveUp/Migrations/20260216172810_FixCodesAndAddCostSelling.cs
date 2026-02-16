using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiveUp.Migrations
{
    /// <inheritdoc />
    public partial class FixCodesAndAddCostSelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationCosts_TransportationTypes_TypeId",
                table: "TransportationCosts");

            migrationBuilder.DropColumn(
                name: "VoucherCount",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "VoucherFrom",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "VoucherTo",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "TransportationCosts");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "TransportationCosts");

            migrationBuilder.DropColumn(
                name: "RecordBy",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "RecordTime",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TransportationCosts",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "TransportationCosts",
                newName: "RoundType");

            migrationBuilder.RenameColumn(
                name: "CostValue",
                table: "TransportationCosts",
                newName: "CostEGP");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationCosts_TypeId",
                table: "TransportationCosts",
                newName: "IX_TransportationCosts_SupplierId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Vouchers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Vouchers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountVouchers",
                table: "Vouchers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromNumber",
                table: "Vouchers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToNumber",
                table: "Vouchers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationTypes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationTypes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TransportationTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationSuppliers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationSuppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TransportationSuppliers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TransportationSuppliers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNo",
                table: "TransportationSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TransportationSuppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TransportationSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatNo",
                table: "TransportationSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationCosts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationCosts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarTypeId",
                table: "TransportationCosts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "TransportationCosts",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Reps",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Reps",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Reps",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reps",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Reps",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "Rates",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "Rates",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Rates",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "PriceLists",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "PriceLists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PriceLists",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Nationalities",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Nationalities",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Nationalities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Hotels",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Hotels",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Hotels",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "HotelDestinations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "HotelDestinations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HotelDestinations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Guides",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Guides",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Guides",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Guides",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "ExcursionSuppliers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "ExcursionSuppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ExcursionSuppliers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ExcursionSuppliers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNo",
                table: "ExcursionSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ExcursionSuppliers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ExcursionSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VatNo",
                table: "ExcursionSuppliers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Excursions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Excursions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Excursions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Boats",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Boats",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Boats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Agents",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Agents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Agents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "Agents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExcursionCostSellings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceListId = table.Column<int>(type: "integer", nullable: true),
                    ExcursionId = table.Column<int>(type: "integer", nullable: true),
                    DestinationId = table.Column<int>(type: "integer", nullable: true),
                    AgentId = table.Column<int>(type: "integer", nullable: true),
                    SupplierId = table.Column<int>(type: "integer", nullable: true),
                    SellingAdlEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingAdlUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingAdlEUR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingAdlGBP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingChdEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingChdUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingChdEUR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    SellingChdGBP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostAdlEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostAdlUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostAdlEUR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostAdlGBP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostChdEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostChdUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostChdEUR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CostChdGBP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NationalFeeAdlEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NationalFeeAdlUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NationalFeeChdEGP = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NationalFeeChdUSD = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    RecordBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecordTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionCostSellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcursionCostSellings_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcursionCostSellings_ExcursionSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "ExcursionSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcursionCostSellings_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcursionCostSellings_HotelDestinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "HotelDestinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcursionCostSellings_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RepVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepId = table.Column<int>(type: "integer", nullable: true),
                    FromNumber = table.Column<int>(type: "integer", nullable: false),
                    ToNumber = table.Column<int>(type: "integer", nullable: false),
                    CountVouchers = table.Column<int>(type: "integer", nullable: false),
                    RecordBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecordTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepVouchers_Reps_RepId",
                        column: x => x.RepId,
                        principalTable: "Reps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCosts_CarTypeId",
                table: "TransportationCosts",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationCosts_DestinationId",
                table: "TransportationCosts",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_NationalityId",
                table: "Agents",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionCostSellings_AgentId",
                table: "ExcursionCostSellings",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionCostSellings_DestinationId",
                table: "ExcursionCostSellings",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionCostSellings_ExcursionId",
                table: "ExcursionCostSellings",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionCostSellings_PriceListId",
                table: "ExcursionCostSellings",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionCostSellings_SupplierId",
                table: "ExcursionCostSellings",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RepVouchers_RepId",
                table: "RepVouchers",
                column: "RepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Nationalities_NationalityId",
                table: "Agents",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationCosts_HotelDestinations_DestinationId",
                table: "TransportationCosts",
                column: "DestinationId",
                principalTable: "HotelDestinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationCosts_TransportationSuppliers_SupplierId",
                table: "TransportationCosts",
                column: "SupplierId",
                principalTable: "TransportationSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationCosts_TransportationTypes_CarTypeId",
                table: "TransportationCosts",
                column: "CarTypeId",
                principalTable: "TransportationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Nationalities_NationalityId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationCosts_HotelDestinations_DestinationId",
                table: "TransportationCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationCosts_TransportationSuppliers_SupplierId",
                table: "TransportationCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationCosts_TransportationTypes_CarTypeId",
                table: "TransportationCosts");

            migrationBuilder.DropTable(
                name: "ExcursionCostSellings");

            migrationBuilder.DropTable(
                name: "RepVouchers");

            migrationBuilder.DropIndex(
                name: "IX_TransportationCosts_CarTypeId",
                table: "TransportationCosts");

            migrationBuilder.DropIndex(
                name: "IX_TransportationCosts_DestinationId",
                table: "TransportationCosts");

            migrationBuilder.DropIndex(
                name: "IX_Agents_NationalityId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "CountVouchers",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "FromNumber",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ToNumber",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TransportationTypes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "FileNo",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "VatNo",
                table: "TransportationSuppliers");

            migrationBuilder.DropColumn(
                name: "CarTypeId",
                table: "TransportationCosts");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "TransportationCosts");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Reps");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reps");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Reps");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PriceLists");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HotelDestinations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "FileNo",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "VatNo",
                table: "ExcursionSuppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "TransportationCosts",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "RoundType",
                table: "TransportationCosts",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CostEGP",
                table: "TransportationCosts",
                newName: "CostValue");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationCosts_SupplierId",
                table: "TransportationCosts",
                newName: "IX_TransportationCosts_TypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Vouchers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Vouchers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "VoucherCount",
                table: "Vouchers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherFrom",
                table: "Vouchers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VoucherTo",
                table: "Vouchers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationTypes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationTypes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationSuppliers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationSuppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "TransportationCosts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "TransportationCosts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "TransportationCosts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "TransportationCosts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Reps",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Reps",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "Rates",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "Rates",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Rates",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "RecordBy",
                table: "Rates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordTime",
                table: "Rates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "PriceLists",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "PriceLists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Nationalities",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Nationalities",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Hotels",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Hotels",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "HotelDestinations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "HotelDestinations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Guides",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Guides",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Guides",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "ExcursionSuppliers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "ExcursionSuppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Excursions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Excursions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Boats",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Boats",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Boats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Boats",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordTime",
                table: "Agents",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "RecordBy",
                table: "Agents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Agents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationCosts_TransportationTypes_TypeId",
                table: "TransportationCosts",
                column: "TypeId",
                principalTable: "TransportationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
