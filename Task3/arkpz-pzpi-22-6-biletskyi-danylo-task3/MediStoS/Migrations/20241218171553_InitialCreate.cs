using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediStoS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manufacturer = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    max_temperature = table.Column<float>(type: "real", nullable: false),
                    min_temperature = table.Column<float>(type: "real", nullable: false),
                    max_humidity = table.Column<float>(type: "real", nullable: false),
                    min_humidity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medicines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    max_temperature = table.Column<float>(type: "real", nullable: false),
                    min_temperature = table.Column<float>(type: "real", nullable: false),
                    max_humidity = table.Column<float>(type: "real", nullable: false),
                    min_humidity = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "batches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    batch_number = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    manufacture_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ware_house_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    medicine_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_batches", x => x.id);
                    table.ForeignKey(
                        name: "fk_batches_medicines_medicine_id",
                        column: x => x.medicine_id,
                        principalTable: "medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_batches_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_batches_warehouses_ware_house_id",
                        column: x => x.ware_house_id,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sensors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: false),
                    warehouse_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sensors", x => x.id);
                    table.ForeignKey(
                        name: "fk_sensors_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storage_violations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    humidity = table.Column<float>(type: "real", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    warehouse_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storage_violations", x => x.id);
                    table.ForeignKey(
                        name: "fk_storage_violations_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batches_medicine_id",
                table: "batches",
                column: "medicine_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_user_id",
                table: "batches",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_batches_ware_house_id",
                table: "batches",
                column: "ware_house_id");

            migrationBuilder.CreateIndex(
                name: "IX_sensors_warehouse_id",
                table: "sensors",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_storage_violations_warehouse_id",
                table: "storage_violations",
                column: "warehouse_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "batches");

            migrationBuilder.DropTable(
                name: "sensors");

            migrationBuilder.DropTable(
                name: "storage_violations");

            migrationBuilder.DropTable(
                name: "medicines");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "warehouses");
        }
    }
}
