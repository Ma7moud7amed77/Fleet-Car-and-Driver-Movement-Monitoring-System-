using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class addDateRemoveLatLon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "Violations");

            migrationBuilder.AddColumn<DateTime>(
                name: "time",
                table: "TestLocation",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "TestLocation");

            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lon",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
