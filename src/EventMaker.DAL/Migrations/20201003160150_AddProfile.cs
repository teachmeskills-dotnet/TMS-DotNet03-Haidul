using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventMaker.DAL.Migrations
{
    public partial class AddProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 63, nullable: false),
                    Username = table.Column<string>(maxLength: 63, nullable: false),
                    FirstName = table.Column<string>(maxLength: 63, nullable: false),
                    LastName = table.Column<string>(maxLength: 63, nullable: true),
                    Age = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(maxLength: 63, nullable: true),
                    Telegram = table.Column<string>(maxLength: 127, nullable: true),
                    SocialNetwork = table.Column<string>(maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
