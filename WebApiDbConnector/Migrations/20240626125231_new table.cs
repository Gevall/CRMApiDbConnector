using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiDbConnector.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_name = table.Column<string>(type: "character(100)", fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("company_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    lastname = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    patronymic = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    telegram_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    lastname = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true),
                    patronymic = table.Column<string>(type: "character(30)", fixedLength: true, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("managers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "triptype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typecontract = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("triptype_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manager_id = table.Column<int>(type: "integer", nullable: true),
                    employe_id = table.Column<int>(type: "integer", nullable: true),
                    trip_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    contract_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deadline_contract = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    company_id = table.Column<int>(type: "integer", nullable: true),
                    customer = table.Column<string>(type: "character(200)", fixedLength: true, maxLength: 200, nullable: true),
                    customer_address = table.Column<string>(type: "character(200)", fixedLength: true, maxLength: 200, nullable: true),
                    trip_type_id = table.Column<int>(type: "integer", nullable: true),
                    caption = table.Column<string>(type: "character(500)", fixedLength: true, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("trips_pkey", x => x.id);
                    table.ForeignKey(
                        name: "trips_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "trips_employe_id_fkey",
                        column: x => x.employe_id,
                        principalTable: "employes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "trips_manager_id_fkey",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "trips_trip_type_id_fkey",
                        column: x => x.trip_type_id,
                        principalTable: "triptype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trips_company_id",
                table: "trips",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_employe_id",
                table: "trips",
                column: "employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_manager_id",
                table: "trips",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_trip_type_id",
                table: "trips",
                column: "trip_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "employes");

            migrationBuilder.DropTable(
                name: "managers");

            migrationBuilder.DropTable(
                name: "triptype");
        }
    }
}
