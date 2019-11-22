using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Migrations
{
    public partial class checkIfAreChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2019, 11, 22, 8, 34, 59, 405, DateTimeKind.Local).AddTicks(6885));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2019, 11, 21, 21, 41, 8, 871, DateTimeKind.Local).AddTicks(7867));
        }
    }
}
