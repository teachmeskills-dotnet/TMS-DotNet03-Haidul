using Microsoft.EntityFrameworkCore.Migrations;

namespace EventMaker.DAL.Migrations
{
    public partial class FixDbProblemsRemoveFirstNameRequiredProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                maxLength: 63,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 63,
                oldNullable: true);
        }
    }
}
