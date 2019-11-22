using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Migrations
{
    public partial class removeSeedBecouseOfDateTimeNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Authors", "Content", "Genre", "Rate", "ReleaseDate", "Title" },
                values: new object[] { 1, "testAuthors", "testContent", 16, 5, new DateTime(2019, 11, 22, 8, 34, 59, 405, DateTimeKind.Local).AddTicks(6885), "testTitle" });
        }
    }
}
