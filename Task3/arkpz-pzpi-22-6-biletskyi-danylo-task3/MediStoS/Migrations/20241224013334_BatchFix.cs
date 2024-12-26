using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediStoS.Migrations
{
    /// <inheritdoc />
    public partial class BatchFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_batches_warehouses_ware_house_id",
                table: "batches");

            migrationBuilder.RenameColumn(
                name: "ware_house_id",
                table: "batches",
                newName: "warehouse_id");

            migrationBuilder.RenameIndex(
                name: "IX_batches_ware_house_id",
                table: "batches",
                newName: "IX_batches_warehouse_id");

            migrationBuilder.AddForeignKey(
                name: "fk_batches_warehouses_warehouse_id",
                table: "batches",
                column: "warehouse_id",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_batches_warehouses_warehouse_id",
                table: "batches");

            migrationBuilder.RenameColumn(
                name: "warehouse_id",
                table: "batches",
                newName: "ware_house_id");

            migrationBuilder.RenameIndex(
                name: "IX_batches_warehouse_id",
                table: "batches",
                newName: "IX_batches_ware_house_id");

            migrationBuilder.AddForeignKey(
                name: "fk_batches_warehouses_ware_house_id",
                table: "batches",
                column: "ware_house_id",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
