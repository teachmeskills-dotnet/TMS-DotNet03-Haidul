using Microsoft.EntityFrameworkCore.Migrations;

namespace EventMaker.DAL.Migrations
{
    public partial class ReworkFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Format",
                table: "Events",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Format",
                table: "Events",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
