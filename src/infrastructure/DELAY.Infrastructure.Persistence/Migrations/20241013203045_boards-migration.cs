using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DELAY.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class boardsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeadlineDate",
                table: "Tickets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Tickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Boards",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Boards");
        }
    }
}
