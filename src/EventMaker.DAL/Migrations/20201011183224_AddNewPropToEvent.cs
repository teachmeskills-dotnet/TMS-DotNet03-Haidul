using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EventMaker.DAL.Migrations
{
    public partial class AddNewPropToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Closed",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Started",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Closed",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Started",
                table: "Events");
        }
    }
}