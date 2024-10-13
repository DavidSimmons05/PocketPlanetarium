using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketPlanetarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlanetsDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscoveryDate",
                table: "Planet",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DiscoveryDate",
                table: "Planet",
                type: "date",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
