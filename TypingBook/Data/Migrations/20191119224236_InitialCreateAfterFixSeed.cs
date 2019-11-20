using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Data.Migrations
{
    public partial class InitialCreateAfterFixSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Authors", "Content", "Genre", "Rate", "ReleaseDate", "Title" },
                values: new object[] { 1, "testAuthors", "testContent", 16, 5, new DateTime(2019, 11, 19, 23, 42, 35, 23, DateTimeKind.Local).AddTicks(1206), "testTitle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    SignedDate = table.Column<DateTime>(nullable: false),
                    TerminationDate = table.Column<DateTime>(nullable: true),
                    TerminationType = table.Column<int>(nullable: true),
                    To = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "Id", "Description", "From", "SignedDate", "TerminationDate", "TerminationType", "To", "UserId" },
                values: new object[,]
                {
                    { 1, "Testowa umowa PRZEDŁUŻONA dwumiesięczna: styczeń-luty, wygenerowana jako metoda SeedData", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 2, "Testowa umowa dwumiesięczna, NIEPRZEDŁUŻONA: luty-marzec, wygenerowana jako metoda SeedData", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 3, "Testowa umowa dwumiesięczna, RÓWNOLEGŁA - PRZEDŁUŻONA KARNETEM RÓWNOLEGŁYM: maj-czerwiec, wygenerowana jako metoda SeedData", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 4, "Testowa umowa RÓWNOLEGŁA: kwiecień-lipiec, NIEPRZEDŁUŻONA, wygenerowana jako metoda SeedData", new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_UserId",
                table: "Agreements",
                column: "UserId");
        }
    }
}
