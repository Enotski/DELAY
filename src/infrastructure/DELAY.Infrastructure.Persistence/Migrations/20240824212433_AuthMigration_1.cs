using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DELAY.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuthMigration_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Rooms_RoomId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Boards",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "SessionLogs",
                columns: table => new
                {
                    SessionLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthProvider = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionLogs", x => x.SessionLogId);
                    table.ForeignKey(
                        name: "FK_SessionLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionLogs_UserId",
                table: "SessionLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Rooms_RoomId",
                table: "Boards",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets",
                column: "ChangedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Rooms_RoomId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SessionLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Boards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Rooms_RoomId",
                table: "Boards",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets",
                column: "ChangedById",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
