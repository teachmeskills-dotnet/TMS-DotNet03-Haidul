using Microsoft.EntityFrameworkCore.Migrations;

namespace EventMaker.DAL.Migrations
{
    public partial class AddAuthorNameToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Events");
        }
    }
}