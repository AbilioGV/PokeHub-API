using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeHub.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTimerFieldsToHunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AccumulatedTime",
                table: "Hunts",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Hunts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                table: "Hunts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Hunts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccumulatedTime",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Hunts");
        }
    }
}
