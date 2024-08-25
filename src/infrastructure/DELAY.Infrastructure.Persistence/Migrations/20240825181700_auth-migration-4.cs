using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DELAY.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class authmigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "SessionLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "SessionLogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "SessionLogs");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "SessionLogs");
        }
    }
}
