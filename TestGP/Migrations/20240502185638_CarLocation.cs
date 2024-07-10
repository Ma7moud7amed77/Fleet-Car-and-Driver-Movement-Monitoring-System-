using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class CarLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TestLocation_CarId",
                table: "TestLocation",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                principalColumn: "Id",
                name: "FK_TestLocation_Cars_CarId",
                table: "TestLocation",
                column: "CarId",
                principalTable: "Cars",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestLocation_Cars_CarId",
                table: "TestLocation");

            migrationBuilder.DropIndex(
                name: "IX_TestLocation_CarId",
                table: "TestLocation");
        }
    }
}
