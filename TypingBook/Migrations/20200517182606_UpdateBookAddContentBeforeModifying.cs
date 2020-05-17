using Microsoft.EntityFrameworkCore.Migrations;

namespace TypingBook.Migrations
{
    public partial class UpdateBookAddContentBeforeModifying : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentBeforeModifying",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentBeforeModifying",
                table: "Books");
        }
    }
}
