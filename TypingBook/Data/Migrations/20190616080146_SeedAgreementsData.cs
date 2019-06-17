using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Data.Migrations
{
    public partial class SeedAgreementsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "ID", "Description", "From", "SignedDate", "TerminationDate", "TerminationType", "To", "UserId" },
                values: new object[,]
                {
                    { 1, "Testowa umowa PRZEDŁUŻONA dwumiesięczna: styczeń-luty, wygenerowana jako metoda SeedData", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 2, "Testowa umowa dwumiesięczna, NIEPRZEDŁUŻONA: luty-marzec, wygenerowana jako metoda SeedData", new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 3, "Testowa umowa dwumiesięczna, RÓWNOLEGŁA - PRZEDŁUŻONA KARNETEM RÓWNOLEGŁYM: maj-czerwiec, wygenerowana jako metoda SeedData", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" },
                    { 4, "Testowa umowa RÓWNOLEGŁA: kwiecień-lipiec, NIEPRZEDŁUŻONA, wygenerowana jako metoda SeedData", new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "34f8fa11-fa68-4c9c-ab2a-0424b0d9319d" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agreements",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Agreements",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Agreements",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Agreements",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
