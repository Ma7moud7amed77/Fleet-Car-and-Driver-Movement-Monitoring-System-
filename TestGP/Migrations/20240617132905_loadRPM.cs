using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class loadRPM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Load",
                table: "Violations",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RPM",
                table: "Violations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Load",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "RPM",
                table: "Violations");
        }
    }
}
