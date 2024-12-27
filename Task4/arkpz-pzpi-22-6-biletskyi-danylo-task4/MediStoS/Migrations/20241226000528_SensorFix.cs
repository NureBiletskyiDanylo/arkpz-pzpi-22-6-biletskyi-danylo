using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediStoS.Migrations
{
    /// <inheritdoc />
    public partial class SensorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "value",
                table: "sensors",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "sensors");
        }
    }
}
