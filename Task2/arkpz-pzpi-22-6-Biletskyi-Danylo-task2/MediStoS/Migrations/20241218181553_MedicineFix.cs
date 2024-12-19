using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediStoS.Migrations
{
    /// <inheritdoc />
    public partial class MedicineFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "medicines",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "medicines");
        }
    }
}
