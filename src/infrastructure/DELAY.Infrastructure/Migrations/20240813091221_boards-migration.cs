using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DELAY.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class boardsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChangedById",
                table: "Tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "Boards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ChangedBy",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Boards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Boards",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_RoomId",
                table: "Boards",
                column: "RoomId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Rooms_RoomId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Boards_RoomId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Boards");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChangedById",
                table: "Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ChangedById",
                table: "Tickets",
                column: "ChangedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
