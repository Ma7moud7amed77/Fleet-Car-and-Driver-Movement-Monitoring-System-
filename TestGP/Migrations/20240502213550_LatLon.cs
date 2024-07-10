using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class LatLon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lan",
                table: "TestLocation",
                newName: "lat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lat",
                table: "TestLocation",
                newName: "lan");
        }
    }
}
