using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class changeTimeToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time",
                table: "TestLocation",
                newName: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "TestLocation",
                newName: "time");
        }
    }
}
