using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBookBlazorApp.Migrations
{
    public partial class InitMigatinToExistingDB : Migration
    {
        //migration not started - beware! it whan drop Books/UserData table
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Books",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(maxLength: 100, nullable: false),
            //        Content = table.Column<string>(nullable: false),
            //        ContentBeforeModifying = table.Column<string>(nullable: true),
            //        Authors = table.Column<string>(maxLength: 100, nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        Rate = table.Column<int>(nullable: true),
            //        Genre = table.Column<int>(nullable: true),
            //        ReleaseDate = table.Column<DateTime>(nullable: true),
            //        AddDate = table.Column<DateTime>(nullable: false),
            //        UserId = table.Column<string>(nullable: true),
            //        License = table.Column<string>(nullable: true),
            //        IsVerified = table.Column<bool>(nullable: false),
            //        IsPrivate = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Books", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Books_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserData",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(nullable: true),
            //        BookProgress = table.Column<string>(nullable: true),
            //        Statistics = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserData", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserData_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Books_UserId",
            //    table: "Books",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserData_UserId",
            //    table: "UserData",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Books");

            //migrationBuilder.DropTable(
            //    name: "UserData");
        }
    }
}
