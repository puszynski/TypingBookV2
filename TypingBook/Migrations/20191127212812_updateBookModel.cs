using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Migrations
{
    public partial class updateBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "License",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");
        }
    }
}
